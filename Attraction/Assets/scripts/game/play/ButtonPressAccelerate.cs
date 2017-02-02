using UnityEngine;
using System.Collections;
using Menu;

public class ButtonPressAccelerate : ButtonEvent {

	public bool buttonPressed = false;

	public override void OnItemDown() {
		base.OnItemDown();
		buttonPressed = true;
	}

	public override void OnItemUp() 
	{
		base.OnItemUp();
		buttonPressed = false;
	}
}