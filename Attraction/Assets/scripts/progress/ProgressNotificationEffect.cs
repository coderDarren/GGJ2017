using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;
using Util;

public class ProgressNotificationEffect : MonoBehaviour {

	public GameObject prefab;
	public RectTransform parent;
	public float rippleLength;		//how long between each ripple
	public float rippleDensity;		//how many ripples
	public float rippleFrequency; 	//how long between each ripple sequence
	public float expandRate;
	public float fadeSpeed;

	public GalaxyType galaxy;
	public int level;
	public bool checkProgress = true;
	int galaxyStatus;
	bool on = true;

	public bool On 
	{
		get { return on; }
		set { on = value; }
	}

	void OnEnable()
	{
		if (checkProgress) {
			galaxyStatus = ProgressManager.Instance.GetStatus(galaxy, level);
			if ((level == 0 && galaxyStatus == 0) ||
				(level > 0 && galaxyStatus == 0))
				StartCoroutine("RunRippleEffect");
		}
		else {
			StartCoroutine("RunRippleEffect");
		}
	}

	void OnDisable()
	{
		StopCoroutine("RunRippleEffect");
	}

	IEnumerator RunRippleEffect()
	{
		while (true)
		{
			if (on) {
				StopCoroutine("Ripple");
				StartCoroutine("Ripple");
				yield return new WaitForSeconds(rippleFrequency);
			} else {
				yield return null;
			}
		}
	}

	IEnumerator Ripple()
	{
		int i = 0;
		while (i < rippleDensity) {
			GameObject obj = (GameObject)Instantiate(prefab);
			RectTransform rect = obj.GetComponent<RectTransform>();
			rect.SetParent(parent);
			rect.localPosition = Vector3.zero;
			StartCoroutine("UpdateRipple", rect);
			i++;
			yield return new WaitForSeconds(rippleLength);
		}
	}

	IEnumerator UpdateRipple(RectTransform rect)
	{
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

		Destroy(rect.gameObject);
	}
}
