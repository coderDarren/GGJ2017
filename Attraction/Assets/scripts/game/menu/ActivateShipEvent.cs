using UnityEngine;
using System.Collections;
using Menu;
using Types;

public class ActivateShipEvent : ButtonEvent {

	ShipType shipType;
	ShipDock dock;
	Ship ship;
	CanvasGroup canvas;

	void Start() {
		InitButton();

		dock = GameObject.FindObjectOfType<ShipDock>();
		shipType = dock.activeShip;
		ship = ShipFinder.GetShip(shipType);
		canvas = GetComponent<CanvasGroup>();

		if (!ship.purchased ||
			shipType == ProgressManager.Instance.PlayerShip() ||
			ProgressManager.Instance.PlayerShip() == ShipType.SHIP_NONE) {
			interactable = false;
			canvas.alpha = 0; 
			gameObject.SetActive(false);
		}
	}

	public override void OnItemUp() {
		ProgressManager.Instance.SetPlayerShip(shipType);
		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		PageManager.Instance.LoadPage(PageType.SHIP_STORE);
	}
}
