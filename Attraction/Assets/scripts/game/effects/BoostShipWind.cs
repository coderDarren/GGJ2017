using UnityEngine;
using System.Collections;

public class BoostShipWind : MonoBehaviour {

	public float fadeSpeed = 2;

	SpriteRenderer sprite;

	void OnEnable() {
		sprite = GetComponent<SpriteRenderer>();
		ShipController.OnThrustersEngage += OnThrustersEngage;
		ShipController.OnThrustersDisengage += OnThrustersDisengage;
	}

	void OnDisable() {
		ShipController.OnThrustersEngage -= OnThrustersEngage;
		ShipController.OnThrustersDisengage -= OnThrustersDisengage;
	}

	void OnThrustersEngage() {
		StopAllCoroutines();
		StartCoroutine("FadeWindTo", 1);
	}

	void OnThrustersDisengage() {
		StopAllCoroutines();
		StartCoroutine("FadeWindTo", 0);
	}

	IEnumerator FadeWindTo(float alpha) {
		Color curr = sprite.color;

		while (Mathf.Abs(alpha - curr.a) > 0.025f) {
			curr.a = Mathf.Lerp(curr.a, alpha, fadeSpeed * Time.deltaTime);
			sprite.color = curr;
			yield return null;
		}
		curr.a = alpha;
		sprite.color = curr;
	}

}
