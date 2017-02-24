using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu {

	[RequireComponent(typeof(Image))]
	public class SlideshowIcon : MonoBehaviour {

		Image img;
		SlideshowPageController slideShow;

		void Start() {
			img = GetComponent<Image>();
			slideShow = GameObject.FindObjectOfType<SlideshowPageController>();
			img.sprite = slideShow.activeIcon;
		}
	}
}