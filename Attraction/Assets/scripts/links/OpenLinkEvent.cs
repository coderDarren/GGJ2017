using UnityEngine;
using System.Collections;
using Menu;

public class OpenLinkEvent : ButtonEvent {

	public override void OnItemUp()
	{
		base.OnItemUp();
		ConfirmLinkPage linkPage = GameObject.FindObjectOfType<ConfirmLinkPage>();
		linkPage.OpenURL();
		PageManager.Instance.TurnOffPage(PageType.LINK, PageType.NONE);
	}
}
