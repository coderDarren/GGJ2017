using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu {

	[RequireComponent(typeof(Text))]
	public class SlideshowText : MonoBehaviour {

		Text t;
		SlideshowPageController slideShow;

		void Start() {
			t = GetComponent<Text>();
			slideShow = GameObject.FindObjectOfType<SlideshowPageController>();
			t.text = slideShow.activeMessage;
		}
	}
}