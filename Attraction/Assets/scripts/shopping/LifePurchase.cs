using UnityEngine;
using System.Collections;
using Types;

public class LifePurchase : PurchaseItem {

	public delegate void PurchaseConstraintsDelegate();
	public static event PurchaseConstraintsDelegate UpdatePurchaseConstraints;

	const int LIFE_COST = 25;

	Ship ship;
	ShipType shipType;
	ShipDock dock;
	int cost;


	void OnEnable() {
		InitButton();

		dock = GameObject.FindObjectOfType<ShipDock>();
		shipType = dock.activeShip;
		ship = ShipFinder.GetShip(shipType);

		ConfigurePurchaseConstraints();

		LifePurchase.UpdatePurchaseConstraints += ConfigurePurchaseConstraints;
	}

	void OnDisable() {
		LifePurchase.UpdatePurchaseConstraints -= ConfigurePurchaseConstraints;
	}

	void ConfigurePurchaseConstraints() {

		if (ship == null)
			Destroy(gameObject);

		if (!ship.purchased || 
			shipType != ProgressManager.Instance.PlayerShip()) {
			interactable = false;
			//gameObject.SetActive(false);
			return;
		}

		switch (purchaseType) {
			case PurchaseType.SINGLE_LIFE:

				cost = LIFE_COST;

				if (ship.lives == ship.armor) {
					interactable = false;
				}

				break;
			case PurchaseType.FULL_LIFE_REFILL:

				cost = (ship.armor - ship.lives) * LIFE_COST;

				if (ship.armor - ship.lives <= 1) {
					interactable = false;
				}

				break;
		}
		
		SetCostText(cost);

		int playerResources = ProgressManager.Instance.GetTotalResources();
		if (playerResources < cost) {
			interactable = false;
		}

		ProgressManager.Instance.UpdateGlobalResourceNotification(playerResources);
	}

	public override void BuySingleLife() {
		ProgressManager.Instance.AddResourcesSpent(cost);
		ProgressManager.Instance.SetShipLives(shipType, ship.lives + 1);
		UpdatePurchaseConstraints();
	}

	public override void BuyFullRefill() {
		ProgressManager.Instance.AddResourcesSpent(cost);
		ProgressManager.Instance.SetShipLives(shipType, ship.armor);
		UpdatePurchaseConstraints();
	}
}
