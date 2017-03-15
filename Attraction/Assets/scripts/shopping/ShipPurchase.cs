using UnityEngine;
using System.Collections;
using Types;
using Menu;

public class ShipPurchase : PurchaseItem {

	Ship ship;
	ShipType shipType;
	ShipDock dock;
	int cost;

	ProgressManager progress;

	void Start() {
		InitButton();
		progress = ProgressManager.Instance;

		dock = GameObject.FindObjectOfType<ShipDock>();
		shipType = dock.activeShip;
		ship = ShipFinder.GetShip(shipType);
		cost = ship.cost;

		if (ship.purchased) {
			interactable = false;
			canvas.alpha = 0;
			gameObject.SetActive(false);
			return;
		}

		int playerResources = progress.GetTotalResources();
		if (playerResources < cost) {
			interactable = false;
			canvas.alpha = disabledAlpha;
		}

		SetCostText(cost);
	}

	public override void BuyShip() {
		progress.MarkShipPurchased(shipType);
		progress.AddResourcesSpent(cost);
		progress.MarkShipPurchaseAchievement();
		//progress.SaveShipArmorTimestamp(shipType);
		//progress.SetShipLives(shipType, ship.armor / 4);

		if (progress.PlayerShip() == ShipType.SHIP_NONE) {
			progress.SetPlayerShip(shipType);
			//TutorialManager.Instance.MarkTutorialComplete(TutorialType.BUY_SHIP);
		}

		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		PageManager.Instance.LoadPage(PageType.SHIP_STORE);
	}
}
