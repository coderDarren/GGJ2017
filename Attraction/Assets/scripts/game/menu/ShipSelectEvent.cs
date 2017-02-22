using UnityEngine;
using System.Collections;
using Menu;
using Types;

public class ShipSelectEvent : ButtonEvent {

	public ShipType shipType;

	ShipDock dock;

	void Start() {
		dock = GameObject.FindObjectOfType<ShipDock>();
	}

	public override void OnItemUp() {
		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		dock.SelectShip(shipType);
	}
}
