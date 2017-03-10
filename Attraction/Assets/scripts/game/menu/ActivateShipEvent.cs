using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Menu;
using Types;

public class ActivateShipEvent : ButtonEvent {

	public Text activeText;

	ShipType shipType;
	ShipDock dock;
	Ship ship;

	void Start() {
		InitButton();

		dock = GameObject.FindObjectOfType<ShipDock>();
		shipType = dock.activeShip;
		ship = ShipFinder.GetShip(shipType);

		if (!ship.purchased ||
			shipType == ProgressManager.Instance.PlayerShip() ||
			ProgressManager.Instance.PlayerShip() == ShipType.SHIP_NONE) {
			interactable = false;
			gameObject.SetActive(false);
		}

		if (shipType == ProgressManager.Instance.PlayerShip()) {
			activeText.gameObject.SetActive(true);
		}

		if (!ship.purchased) {
			activeText.gameObject.SetActive(false);
		}
	}

	public override void OnItemUp() {
		ProgressManager.Instance.SetPlayerShip(shipType);
		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		PageManager.Instance.LoadPage(PageType.SHIP_STORE);
	}
}
