using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Types;

[RequireComponent(typeof(Text))]
public class StoryElement : MonoBehaviour {

	public GalaxyType forGalaxy;
	public int level;
	
	Text text;

	void Start()
	{	
		//text = GetComponent<Text>();
		//int status = ProgressManager.Instance.GetStatus(forGalaxy, level);
		//if (status == -1 || status == 0) {
		//	if (level == 0) {
		//		text.text = "You have no knowledge of this galaxy.";
		//	}
		//	return;
		//}
		//string story = StoryManager.Instance.GetStory(forGalaxy, level);
		//if (story != null) {
		//	text.text = story;
		//}
	}
}
