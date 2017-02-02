using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class EnviroFader : MonoBehaviour {

	public SpriteRenderer background;

	SpriteRenderer sprite;
	ShipController ship;

	void Start() {
		sprite = GetComponent<SpriteRenderer>();
		sprite.color = background.color;
		ship = GameObject.FindObjectOfType<ShipController>();
		ship.GetComponent<SpriteRenderer>().sortingOrder = 0;
		StartCoroutine("FadeOut");
	}

	IEnumerator FadeOut() {
		float target = 0;
		Color curr = sprite.color;

		while (curr.a > 0.015f) {
			curr.a = Mathf.Lerp(curr.a, target, 1 * Time.deltaTime);
			sprite.color = curr;
			yield return null;
		}

		curr.a = target;
		sprite.color = curr;

		ship.GetComponent<SpriteRenderer>().sortingOrder = 10;
		ship.StartCoroutine("WaitToLaunch");
	}
}
