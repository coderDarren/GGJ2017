using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Text countdownText;
	public Image thrustFillBar;
	public Image lifeFillBar;

	void OnEnable()
	{
		lifeFillBar.fillAmount = GameObject.FindObjectOfType<ShipController>().lives / 5.0f;
		countdownText.color = Color.white;
		ShipController.OnLivesChanged += OnLivesChanged;
		ShipController.OnCountdown += OnCountdown;
		ShipController.UpdateThrusterHUD += UpdateThrusterHUD;
	}

	void OnDisable()
	{
		ShipController.OnLivesChanged -= OnLivesChanged;
		ShipController.OnCountdown -= OnCountdown;
		ShipController.UpdateThrusterHUD -= UpdateThrusterHUD;
	}

	void UpdateThrusterHUD(float curr, float max) {
		thrustFillBar.fillAmount = (curr / max) / 2.0f;
	}

	void OnLivesChanged(int lives)
	{
		StopCoroutine("SetLifeFill");
		StartCoroutine("SetLifeFill", (lives / 5.0f));
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

	IEnumerator SetLifeFill(float amount) {
		float vel = 0;
		while (Mathf.Abs(amount - lifeFillBar.fillAmount) > 0.01f) {
			lifeFillBar.fillAmount = Mathf.SmoothDamp(lifeFillBar.fillAmount, amount, ref vel, 0.1f);
			yield return null;
		}
	}

	IEnumerator SetThrustFill(float amount) {
		float vel = 0;
		while (Mathf.Abs(amount - thrustFillBar.fillAmount) > 0.01f) {
			thrustFillBar.fillAmount = Mathf.SmoothDamp(thrustFillBar.fillAmount, amount, ref vel, 0.1f);
			yield return null;
		}
	}
}
