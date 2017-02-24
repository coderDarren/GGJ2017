using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu {

	public class SlideshowPageController : MonoBehaviour {

		public ButtonEvent previousSlide;
		public ButtonEvent nextSlide;
		public bool multipleSlides;
		public PageType slideType;
		public PageType[] slideTypes;
		public string[] messages;
		public Sprite[] icons;
		public Image[] slideBubbles;
		public Color bubbleActive, bubbleInactive;

		PageManager pageManager;
		public string activeMessage { get; private set; }
		public Sprite activeIcon { get; private set; }
		int activeSlide;
		int slideCount;

		void Start() {
			pageManager = PageManager.Instance;
			Configure();
		}

		void OnEnable() {
			previousSlide.OnButtonUp += GoPreviousPage;
			nextSlide.OnButtonUp += GoNextPage;
		}

		void OnDisable() {
			previousSlide.OnButtonUp -= GoPreviousPage;
			nextSlide.OnButtonUp -= GoNextPage;
		}

		void Configure() {
			foreach(Image img in slideBubbles) img.color = bubbleInactive;

			if (multipleSlides)
				slideCount = slideTypes.Length;
			else
				slideCount = messages.Length;

			ActivateSlide(0);
		}

		void ActivateSlide(int slide) {
			if (slide < 0 || slide >= slideCount) return;

			previousSlide.GetComponent<CanvasGroup>().alpha = slide == 0 ? 0.25f : 1;
			nextSlide.GetComponent<CanvasGroup>().alpha = slide == slideCount - 1 ? 0.25f : 1;
			previousSlide.interactable = slide == 0 ? false : true;
			nextSlide.interactable = slide == slideCount - 1 ? false : true;

			if (multipleSlides) {
				pageManager.TurnOffPage(slideTypes[activeSlide], PageType.NONE);
			}

			slideBubbles[activeSlide].color = bubbleInactive;
			activeSlide = slide;
			slideBubbles[activeSlide].color = bubbleActive;

			if (multipleSlides) {
				pageManager.LoadPage(slideTypes[activeSlide]);
			}
			else {
				activeMessage = messages[activeSlide];
				activeIcon = icons[activeSlide];
				pageManager.TurnOffPage(slideType, PageType.NONE);
				pageManager.LoadPage(slideType);
			}
			
		}

		void GoNextPage() {
			ActivateSlide(activeSlide + 1);
		}

		void GoPreviousPage() {
			ActivateSlide(activeSlide - 1);
		}
	}
}