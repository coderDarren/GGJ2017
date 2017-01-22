using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text livesText;
	public Text countdownText;

	void OnEnable()
	{
		countdownText.color = Color.white;
		ShipController.OnLivesChanged += OnLivesChanged;
		ShipController.OnCountdown += OnCountdown;
	}

	void OnDisable()
	{
		ShipController.OnLivesChanged -= OnLivesChanged;
		ShipController.OnCountdown -= OnCountdown;
	}

	void OnLivesChanged(int lives)
	{
		livesText.text = lives.ToString();
	}

	void OnCountdown(int time)
	{
		countdownText.text = "Launch in " +time.ToString();
		if (time == 1) {
			StopCoroutine("FadeText");
			StartCoroutine("FadeText");
		}
	}

	IEnumerator FadeText()
	{
		Color color = countdownText.color;

		while (color.a > 0.01f) {
			color.a = Mathf.Lerp(color.a, 0, 2.5f * Time.deltaTime);
			countdownText.color = color;
			yield return null;
		}

		color.a = 0;
		countdownText.color = color;
	}
}
