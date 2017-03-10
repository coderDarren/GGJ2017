using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

[RequireComponent(typeof(Image))]
public class ShipIcon : MonoBehaviour {

	void OnEnable() {
		ProgressManager.Instance.UpdateShipUI += OnShipChanged;
	}

	void OnDisable() {
		ProgressManager.Instance.UpdateShipUI -= OnShipChanged;
	}

	void OnShipChanged(ShipType ship) {
		GetComponent<Image>().sprite = ShipFinder.GetShip(ship).sprite;
	}
}
