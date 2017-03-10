using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;
using Menu;

public class ShipDock : MonoBehaviour {

	public float rotationSpeed = 5;
	public ShipType activeShip { get; private set; }

	RectTransform rect;

	void Awake() {
		rect = GetComponent<RectTransform>();
		ShipType playerShip = ProgressManager.Instance.PlayerShip();
		if (playerShip == ShipType.SHIP_NONE)
			SelectShip(ShipType.SHIP_01);
		else
			SelectShip(playerShip);
	}

	public void SelectShip(ShipType ship) {
		activeShip = ship;
		//ProgressManager.Instance.SetPlayerShip(activeShip);
		PageManager.Instance.LoadPage(PageType.SHIP_STORE);
	}
}
