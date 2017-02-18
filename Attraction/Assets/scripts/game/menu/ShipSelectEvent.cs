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
		dock.SelectShip(shipType);
		
	}
}
