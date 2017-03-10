using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Globalization;
using System;

[RequireComponent(typeof(Text))]
public class StatText : MonoBehaviour {

	public enum StatType {
		RESOURCES,
		STARS
	}
	public StatType stat;
	public bool updateDiscrete = true;
	public bool useFormatting = true;
	public string prefix = string.Empty;
	public string suffix = string.Empty;

	Text t;
	int current;

	void Start() {
		t = GetComponent<Text>();
		current = stat == StatType.RESOURCES ? ProgressManager.Instance.GetTotalResources() : ProgressManager.Instance.GetStars();
		SetText();
	}

	void OnEnable() {
		if (stat == StatType.RESOURCES) {
			ProgressManager.Instance.UpdateResourcesText += UpdateStat;
		}

		if (stat == StatType.STARS) {
			ProgressManager.Instance.UpdateStarsText += UpdateStat;
		}
	}

	void OnDisable() {
		if (stat == StatType.RESOURCES) {
			ProgressManager.Instance.UpdateResourcesText -= UpdateStat;
		}

		if (stat == StatType.STARS) {
			ProgressManager.Instance.UpdateStarsText -= UpdateStat;
		}
	}

	void UpdateStat(int amount) {
		if (updateDiscrete) {
			current = amount;
			SetText();
			return;
		}
		StopCoroutine("UpdateStatOvertime");
		StartCoroutine("UpdateStatOvertime", amount);
	}

	IEnumerator UpdateStatOvertime(int target) {
		float fTarget = (float)target;
		float vel = 0;
		while (Mathf.Abs(current - target) > 10) {
			current = (int)Mathf.SmoothDamp((float)current, fTarget, ref vel, 0.5f);
			SetText();
			yield return null;
		}

		current = target;
		SetText();
	}

	void SetText() {

		if (!t) {
			t = GetComponent<Text>();
		}

		if (!useFormatting) {
			t.text = prefix + string.Format("{0:n0}", current) + suffix;
			return;
		}

		int mag = (int)(Mathf.Floor(Mathf.Log10(current))/3); 
		double divisor = Mathf.Pow(10, mag*3);

		double shortNumber = current / divisor;

		string numSuffix = string.Empty;
		switch(mag)
		{
		    case 1:
		        numSuffix = "K";
		        break;
		    case 2:
		        numSuffix = "M";
		        break;
		    case 3:
		        numSuffix = "B";
		        break;
		}
		string decimals = current >= 1000 ? "N1" : "N0";
		string result = shortNumber.ToString(decimals) + numSuffix; 
		if (result.ToLower() == "nan") { result = "0"; }
		t.text = prefix + result + suffix;
	}
}
