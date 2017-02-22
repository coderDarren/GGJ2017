using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

public class ShipArmorBar : MonoBehaviour {

	public Image bar;
	public Image icon;
	public Text rechargeText;
	public Text percentageText;
	public ShipType shipType;
	public bool forActiveShip;
	public bool useShipIcon = true;
	public Color lowArmor, mediumArmor, healthyArmor;

	ProgressManager progress;

	void Start() {
		progress = ProgressManager.Instance;
		StartCoroutine("UpdateBar");
	}

	void SetBarColor() {
		if (bar.fillAmount <= 0.25f) {
			bar.color = lowArmor;
		}
		else if (bar.fillAmount <= 0.7f) {
			bar.color = mediumArmor;
		}
		else {
			bar.color = healthyArmor;
		}
	}

	IEnumerator UpdateBar() {
		if (forActiveShip)
			shipType = progress.PlayerShip();
		Ship ship = ShipFinder.GetShip(shipType);
		int lives = ship.lives;
		int armor = ship.armor;

		if (useShipIcon) {
			icon.sprite = ship.sprite;
		}

		while (true) {
			bar.fillAmount = ship.lives / (float)armor;
			if (bar.fillAmount == 0) bar.fillAmount = 0.05f;
			SetBarColor();

			if (ship.purchased) {

				if (ship.lives == armor) {
					rechargeText.text = "FULL";
				}

				if (rechargeText && ship.lives < armor) {
					int minutes = progress.MinutesLeftToNextRecharge(shipType);
					int seconds = progress.SecondsLeftToNextRecharge(shipType);

					seconds = Mathf.Clamp(seconds, 0, 59);
					minutes = Mathf.Clamp(minutes, 0, 59);

					if (seconds >= 10)
						rechargeText.text = minutes.ToString()+":"+seconds.ToString();
					else 
						rechargeText.text = minutes.ToString()+":0"+seconds.ToString();
				}
				if (percentageText) {
					int percentage = (int)(((ship.lives / (float)armor) * 100));
					percentageText.text = percentage.ToString() + "%";
				}
			}
			
			yield return new WaitForSeconds(1);
		}
	}
}
