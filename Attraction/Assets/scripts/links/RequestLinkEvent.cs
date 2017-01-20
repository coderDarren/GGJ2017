using UnityEngine;
using System.Collections;
using Menu;

public class RequestLinkEvent : ButtonEvent {

	public string userMessage;
	public string url;

	public override void OnItemUp()
	{
		base.OnItemUp();
		PageManager.Instance.LoadPage(PageType.LINK);
		ConfirmLinkPage linkPage = GameObject.FindObjectOfType<ConfirmLinkPage>();
		linkPage.ConfigureLinkPage(userMessage, url);
	}
}
