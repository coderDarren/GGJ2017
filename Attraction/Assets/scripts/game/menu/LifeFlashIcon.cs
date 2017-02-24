using UnityEngine;
using System.Collections;
using Menu;

public class LifeFlashIcon : FlashNotification {

	void OnEnable() {
		Init();
		ShipController.OnFlashLifeIcon += StartFlash;
	}

	void OnDisable() {
		ShipController.OnFlashLifeIcon -= StartFlash;
	}
}
