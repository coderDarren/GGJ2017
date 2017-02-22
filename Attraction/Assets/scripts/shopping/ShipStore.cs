using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

public class ShipStore : MonoBehaviour {

	public Image shipIcon;
	public Image coastVelocityFill;
	public Image activeCoastVelocity;
	public Image maxVelocityFill;
	public Image activeMaxVelocity;
	public Image thrustAccelFill;
	public Image activeThrustAccel;
	public Image durabilityFill;
	public Image activeDurability;
	public Text shipName;
	public ShipArmorBar armorBar;

	public Color selectedColor, gainColor, lossColor, disabledColor;

	ShipType activeShip;
	ShipDock dock;


	void Awake() {
		dock = GameObject.FindObjectOfType<ShipDock>();
		activeShip = dock.activeShip;
		armorBar.shipType = activeShip;
		ConfigurePage();
	}

	void ConfigurePage() {
		Ship ship = ShipFinder.GetShip(activeShip);
		Ship playerShip = ShipFinder.GetShip(ProgressManager.Instance.PlayerShip());

		shipName.text = ship.name;
		shipIcon.sprite = ship.sprite;
		float selectedCoastVelocity = BarValue(0.1f, 5f, ship.coast);
		float selectedMaxVelocity = BarValue(1, 6f, ship.thrustPower + ship.coast);
		float selectedThrustAccel = BarValue(0.0005f, 0.005f, ship.thrustAccel);
		float selectedDurability = BarValue(2, 30, ship.armor);

		float playerCoastVelocity = BarValue(0.1f, 5f, playerShip.coast);
		float playerMaxVelocity = BarValue(1, 6f, playerShip.thrustPower + playerShip.coast);
		float playerThrustAccel = BarValue(0.0005f, 0.005f, playerShip.thrustAccel);
		float playerDurability = BarValue(2, 30, playerShip.armor);

		//COAST VELOCITY BARS

		if (selectedCoastVelocity < playerCoastVelocity) {

			coastVelocityFill.fillAmount = selectedCoastVelocity;
			coastVelocityFill.color = selectedColor;
			activeCoastVelocity.fillAmount = playerCoastVelocity;
			activeCoastVelocity.color = lossColor;

		} else if (selectedCoastVelocity > playerCoastVelocity) {

			coastVelocityFill.fillAmount = playerCoastVelocity;
			coastVelocityFill.color = selectedColor;
			activeCoastVelocity.fillAmount = selectedCoastVelocity;
			activeCoastVelocity.color = gainColor;

		} else {

			coastVelocityFill.fillAmount = selectedCoastVelocity;
			coastVelocityFill.color = selectedColor;
			activeCoastVelocity.fillAmount = 0;

		}

		//MAX VELOCITY BARS

		if (selectedMaxVelocity < playerMaxVelocity) {

			maxVelocityFill.fillAmount = selectedMaxVelocity;
			maxVelocityFill.color = selectedColor;
			activeMaxVelocity.fillAmount = playerMaxVelocity;
			activeMaxVelocity.color = lossColor;

		} else if (selectedMaxVelocity > playerMaxVelocity) {

			maxVelocityFill.fillAmount = playerMaxVelocity;
			maxVelocityFill.color = selectedColor;
			activeMaxVelocity.fillAmount = selectedMaxVelocity;
			activeMaxVelocity.color = gainColor;

		} else {

			maxVelocityFill.fillAmount = selectedMaxVelocity;
			maxVelocityFill.color = selectedColor;
			activeMaxVelocity.fillAmount = 0;

		}

		//THRUST BARS

		if (selectedThrustAccel < playerThrustAccel) {

			thrustAccelFill.fillAmount = selectedThrustAccel;
			thrustAccelFill.color = selectedColor;
			activeThrustAccel.fillAmount = playerThrustAccel;
			activeThrustAccel.color = lossColor;

		} else if (selectedThrustAccel > playerThrustAccel) {

			thrustAccelFill.fillAmount = playerThrustAccel;
			thrustAccelFill.color = selectedColor;
			activeThrustAccel.fillAmount = selectedThrustAccel;
			activeThrustAccel.color = gainColor;

		} else {

			thrustAccelFill.fillAmount = selectedThrustAccel;
			thrustAccelFill.color = selectedColor;
			activeThrustAccel.fillAmount = 0;

		}

		//DURABILITY BARS

		if (selectedDurability < playerDurability) {

			durabilityFill.fillAmount = selectedDurability;
			durabilityFill.color = selectedColor;
			activeDurability.fillAmount = playerDurability;
			activeDurability.color = lossColor;

		} else if (selectedDurability > playerDurability) {

			durabilityFill.fillAmount = playerDurability;
			durabilityFill.color = selectedColor;
			activeDurability.fillAmount = selectedDurability;
			activeDurability.color = gainColor;

		} else {

			durabilityFill.fillAmount = selectedDurability;
			durabilityFill.color = selectedColor;
			activeDurability.fillAmount = 0;

		}

	}

	float BarValue(float min, float max, float actual) {
		float ret = 0;
		ret = (actual - min) / (max - min);
		return ret;
	}
}
