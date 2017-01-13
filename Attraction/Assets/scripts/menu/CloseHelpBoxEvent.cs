using UnityEngine;
using System.Collections;
using Menu;

public class CloseHelpBoxEvent : ButtonEvent {

	public HelpBox _helpBox;

	public override void OnItemUp()
	{
		if (_helpBox)
		{
			_helpBox.CloseHelpBox();
		}
	}
}
