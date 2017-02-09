using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Menu;

public class AchievementProgressPage : MonoBehaviour {

	public Image img;

	public void Configure(int curr, int target, int max) {
		img.fillAmount = (float)curr / (float)max;
		StartCoroutine(MoveFill(curr,target,max));
	}

	IEnumerator MoveFill(int curr, int target, int max) {
		yield return new WaitForSeconds(3);

		float currFill = (float)curr / (float)max;
		float targetFill = (float)target / (float)max;

		while (Mathf.Abs(currFill - targetFill) > 0.05f) {
			currFill += 0.1f * Time.deltaTime;
			img.fillAmount = currFill;
			yield return null;
		}

		img.fillAmount = targetFill;
		yield return new WaitForSeconds(1);
		PageManager.Instance.TurnOffPage(PageType.ACHIEVEMENT_PROGRESS, PageType.NONE);
	}
}
