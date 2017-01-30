using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;
using DebugServices;

public class BoostCameraBloom : MonoBehaviour {

	public float boostIntensity = 1.24f;
	public float boostSpeed = 10;
	public float reduceSpeed = 2;

	BloomOptimized bloom;

	void OnEnable() {
		bloom = GetComponent<BloomOptimized>();
		ShipController.OnThrustersEngage += OnThrustersEngage;
		ShipController.OnThrustersDisengage += OnThrustersDisengage;
	}

	void OnDisable() {
		ShipController.OnThrustersEngage -= OnThrustersEngage;
		ShipController.OnThrustersDisengage -= OnThrustersDisengage;
	}

	void OnThrustersEngage() {
		StopAllCoroutines();
		StartCoroutine("IncreaseBloom");
	}

	void OnThrustersDisengage() {
		StopAllCoroutines();
		StartCoroutine("DecreaseBloom");
	}

	IEnumerator IncreaseBloom() {
		while (Mathf.Abs(bloom.intensity - boostIntensity) > 0.1f &&
			   Lightning.Instance.Active == false) {
			bloom.intensity = Mathf.Lerp(bloom.intensity, boostIntensity, boostSpeed * Time.deltaTime);
			yield return null;
		}
		bloom.intensity = boostIntensity;
	}

	IEnumerator DecreaseBloom() {
		while (bloom.intensity > 0.1f &&
			   Lightning.Instance.Active == false) {
			bloom.intensity = Mathf.Lerp(bloom.intensity, 0, reduceSpeed * Time.deltaTime);
			yield return null;
		}
		bloom.intensity = 0;
	}

}
