using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Menu;
using Types;

public class ShipSelectEvent : PageLoadToggleEvent {

	public ShipType shipType;

	[System.Serializable]
	public struct Icon {
		public Color toggleOnColor, toggleOffColor;
		public Image img;
	}
	public Icon[] icons;

	ShipDock dock;

	void Start() {
		dock = GameObject.FindObjectOfType<ShipDock>();
		if (dock.activeShip == shipType) {
			Toggle(this, toggleGroupId);
		}
	}

	public override void OnToggleThis(ButtonEvent be, int id) {

		if (id != toggleGroupId)
			return; //event not intended for this toggle group

		if (this == be) //this button was activated
		{
			_onItem = true;
			ApplyHoverProperties();
			HandleButtonEffect();
		}
		else { //another button was activated
			_onItem = false;
			ApplyRestProperties();
			StopButtonEffect();
		}
	}

	public override void OnItemUp() {
		Toggle(this, toggleGroupId);
		PageManager.Instance.TurnOffPage(PageType.SHIP_STORE, PageType.NONE);
		dock.SelectShip(shipType);
	}
}
