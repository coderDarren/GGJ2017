using UnityEngine;
using System.Collections;

public class BoostCameraShake : MonoBehaviour {

	public float damp = 2;
	public float smooth = 5;
	public float strength = 20;
	public float interval = 0.5f;

	void OnEnable() {
		ShipController.OnThrustersEngage += OnThrustersEngage;
		ShipController.OnThrustersDisengage += OnThrustersDisengage;
	}

	void OnDisable() {
		ShipController.OnThrustersEngage -= OnThrustersEngage;
		ShipController.OnThrustersDisengage -= OnThrustersDisengage;
	}

	void OnThrustersEngage() {
		StopAllCoroutines();
		StartCoroutine("ShakeCamera");
	}

	void OnThrustersDisengage() {
		StopAllCoroutines();
		StartCoroutine("ResetCamera");
	}

	IEnumerator ShakeCamera() {
		float timer = 0;
		float target = -1;
		while (Lightning.Instance.Active == false) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, target * strength), smooth * Time.deltaTime);
			timer += Time.deltaTime;
			if (timer > interval) {
				target = target == 1 ? -1 : 1;
				timer = 0;
			}
			yield return null;
		}
	}

	IEnumerator ResetCamera() {
		float timer = 0;
		float str = strength;
		float target = -str;
		while (str > 0.02f) {
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, target), smooth * Time.deltaTime);
			timer += Time.deltaTime;
			if (timer > interval) {
				str = Mathf.Lerp(str, 0, damp * Time.deltaTime);
				target = target > 0 ? -str : str;
				timer = 0;
			}
			yield return null;
		}

		transform.rotation = Quaternion.Euler(0,0,0);
	}
}
