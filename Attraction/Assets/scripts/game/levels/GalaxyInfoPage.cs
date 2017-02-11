using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Menu;
using Types;

public class GalaxyInfoPage : MonoBehaviour {

	public Text infoText;
	public PageLoadEvent pageLoadEvent;

	public void ConfigureInfo(GalaxyType galaxy, PageType pageToLoad)
	{
		pageLoadEvent.pageToLoad = pageToLoad;
		infoText.text = StoryManager.Instance.GetStory(galaxy, 0);
	}

}
