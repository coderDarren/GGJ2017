using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu {
	[RequireComponent(typeof(Image))]
	public class FlashNotification : MonoBehaviour {

		public Color restColor;
		public Color flashColor;
		public float interval;
		public float duration;

		public Image[] images;

		protected void Init() {
			
		}

		protected void StartFlash() {
			StopFlash();
			StartCoroutine("Flash");
		}

		void StopFlash() {
			StopCoroutine("Flash");
			SwapColors(false); //sets image to rest
		}

		void SwapColors(bool flash) {
			if (flash) {
				foreach (Image img in images) img.color = flashColor;
			}
			else {
				foreach (Image img in images) img.color = restColor;
			}
		}

		IEnumerator Flash() {
			float flashTime = 0;
			float flashInterval = 0;
			bool flash = false;

			while (flashTime < duration) {
				flashTime += Time.deltaTime;
				flashInterval += Time.deltaTime;

				if (flashInterval > interval) {
					flash = !flash;
					SwapColors(flash);
					flashInterval = 0;
				}

				yield return null;
			}

			SwapColors(false); //sets image to rest
		}
	}
}