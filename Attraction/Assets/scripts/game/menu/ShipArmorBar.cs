using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

public class ShipArmorBar : MonoBehaviour {

	public Image bar;
	public Text rechargeText;
	public Text percentageText;
	public ShipType shipType;
	public bool forActiveShip;
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

		while (true) {
			bar.fillAmount = ship.lives / (float)armor;
			if (bar.fillAmount == 0) bar.fillAmount = 0.05f;
			SetBarColor();

			if (rechargeText) {
				int minutes = progress.MinutesLeftToNextRecharge(shipType);
				int seconds = progress.SecondsLeftToNextRecharge(shipType);
				if (seconds >= 60)
					rechargeText.text = minutes.ToString() + "m";
				else if (seconds >= 10)
					rechargeText.text = "0:"+seconds.ToString();
				else 
					rechargeText.text = "0:0"+seconds.ToString();
			}
			if (percentageText) {
				percentageText.text = ((ship.lives / (float)armor) * 100).ToString() + "%";
			}

			yield return new WaitForSeconds(1);
		}
	}
}
