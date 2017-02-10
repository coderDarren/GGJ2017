using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Util;
using PoolingServices;

public class RippleNotification : MonoBehaviour {

	public GameObject prefab;
	public RectTransform parent;
	public float rippleLength;		//how long between each ripple
	public float rippleDensity;		//how many ripples
	public float rippleFrequency; 	//how long between each ripple sequence
	public float expandRate;
	public float fadeSpeed;

	public int maxParticles;
	public float defaultSize = 0.1f;

	Pool pool;
	Dictionary<Transform, int> idLookupTable;

	void Start() {
		pool = new Pool();
		pool.ConfigurePool(parent.transform, prefab, maxParticles);
		idLookupTable = new Dictionary<Transform, int>();
		StartCoroutine("RunRippleEffect");
	}

	IEnumerator RunRippleEffect()
	{
		while (true)
		{
			StopCoroutine("Ripple");
			StartCoroutine("Ripple");
			yield return new WaitForSeconds(rippleFrequency);
		}
	}

	IEnumerator Ripple()
	{
		int i = 0;
		while (i < rippleDensity) {
			//GameObject obj = (GameObject)Instantiate(prefab);
			int objId = pool.GetObject();
			if (objId != -1) {
				Transform t = pool.ObjectTransform(objId);
				RectTransform rect = t.gameObject.GetComponent<RectTransform>();
				rect.SetParent(parent);
				rect.localPosition = Vector3.zero;
				idLookupTable.Add(t, objId);
				StartCoroutine("UpdateRipple", rect);
			}
			i++;
			yield return new WaitForSeconds(rippleLength);
		}
	}

	IEnumerator UpdateRipple(RectTransform rect)
	{
		ResetRect(ref rect);
		Image img = rect.GetComponent<Image>();
		Color c = img.color;
		float size = rect.sizeDelta.x;

		while (c.a > 0.01f) {
			size += expandRate * Time.deltaTime;
			c.a = Mathf.Lerp(c.a, 0, fadeSpeed * Time.deltaTime);
			img.color = c;
			Utility.SetRectSize(ref rect, Vector2.one * size);
			yield return null;
		}

		int objId = 0;
		if (idLookupTable.TryGetValue(rect.gameObject.transform, out objId)) {
			pool.DiscardObject(objId);
			idLookupTable.Remove(rect.gameObject.transform);
		}
		//Destroy(rect.gameObject);
	}

	void ResetRect(ref RectTransform rect) {
		Image img = rect.GetComponent<Image>();
		Color c = img.color;
		c.a = 1;
		img.color = c;
		Utility.SetRectSize(ref rect, Vector2.one * defaultSize);
	}
}
