using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;

public class ShipDock : MonoBehaviour {

	public float rotationSpeed = 5;

	RectTransform rect;

	void Start() {
		rect = GetComponent<RectTransform>();
	}

	IEnumerator RotateShipDock(float deg) {
		float dist = Mathf.Abs(rect.eulerAngles.z - deg);
		while (dist > 0.1f) {
			rect.rotation = Quaternion.Lerp(rect.rotation, Quaternion.Euler(0,0,deg), rotationSpeed * Time.deltaTime);
			dist = Mathf.Abs(rect.eulerAngles.z - deg);
			yield return null;
		}
		rect.rotation = Quaternion.Euler(0,0,deg);
	}

	public void SelectShip(ShipType ship) {
		StopCoroutine("RotateShipDock");
		switch (ship) {
			case ShipType.SHIP_01: StartCoroutine("RotateShipDock", 0); break;
			case ShipType.SHIP_02: StartCoroutine("RotateShipDock", 45); break;
			case ShipType.SHIP_03: StartCoroutine("RotateShipDock", 90); break;
			case ShipType.SHIP_04: StartCoroutine("RotateShipDock", 135); break;
			case ShipType.SHIP_05: StartCoroutine("RotateShipDock", 180); break;
			case ShipType.SHIP_06: StartCoroutine("RotateShipDock", -135); break;
			case ShipType.SHIP_07: StartCoroutine("RotateShipDock", -90); break;
			case ShipType.SHIP_08: StartCoroutine("RotateShipDock", -45); break;
		}
	}
}
