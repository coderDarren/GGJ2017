using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

public class ShipStore : MonoBehaviour {

	public Image shipIcon;
	public Text shipName;

	ShipType activeShip;
	ShipDock dock;

	void Start() {
		dock = GameObject.FindObjectOfType<ShipDock>();
		activeShip = dock.activeShip;
		ConfigurePage();
	}

	void ConfigurePage() {
		Ship ship = ShipFinder.GetShip(activeShip);
		Ship playerShip = ShipFinder.GetShip(ProgressManager.Instance.PlayerShip());

		shipName.text = ship.name;
		shipIcon.sprite = ship.sprite;
	}
}
