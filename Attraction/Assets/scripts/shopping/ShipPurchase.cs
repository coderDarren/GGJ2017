using UnityEngine;
using System.Collections;
using Types;
using Menu;

public class ShipPurchase : PurchaseItem {

	Ship ship;
	ShipType shipType;
	ShipDock dock;
	int cost;

	void Start() {
		InitButton();

		dock = GameObject.FindObjectOfType<ShipDock>();
		shipType = dock.activeShip;
		ship = ShipFinder.GetShip(shipType);
		cost = ship.cost;

		if (ship.purchased) {
			interactable = false;
			canvas.alpha = 0;
			//gameObject.SetActive(false);
			return;
		}

		int playerResources = ProgressManager.Instance.GetTotalResources();
		if (playerResources < cost) {
			interactable = false;
			canvas.alpha = disabledAlpha;
		}

		SetCostText(cost);
	}

	public override void BuyShip() {
		ProgressManager.Instance.MarkShipPurchased(shipType);
		ProgressManager.Instance.SetPlayerShip(shipType);
		ProgressManager.Instance.AddResourcesSpent(cost);
		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		PageManager.Instance.LoadPage(PageType.SHIP_STORE);
	}
}
