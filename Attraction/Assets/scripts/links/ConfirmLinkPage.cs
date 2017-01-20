using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmLinkPage : MonoBehaviour {

	public Text message;

	string url;
	
	public void ConfigureLinkPage(string msg, string url)
	{
		message.text = msg;
		this.url = url;
	}

	public void OpenURL()
	{
		Application.OpenURL(url);
	}
}
