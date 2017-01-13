using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Menu {

	[RequireComponent(typeof(InputField))]
	public class InputFieldItem : MonoBehaviour {

		public CanvasGroup cross; //bad input notifier
		public int minCharacters = 3;
		public int maxCharacters = 18;

		InputField inputField;
		public bool valid { get; private set; }
		public string text { get; private set; }

		void Start()
		{
			inputField = GetComponent<InputField>();
			StartCoroutine(FadeCanvas(cross, true));
		}

		public void OnInputChanged()
		{
			text = inputField.text;
			OnInputChanged(text);
		}

		public virtual void OnInputChanged(string input)
		{
			if (input.Length < minCharacters || input.Length > maxCharacters)
			{
				if (valid) {
					StopAllCoroutines();
					StartCoroutine(FadeCanvas(cross, true));
					valid = false;
				}
			}
			else
			{
				if (!valid) {
					StopAllCoroutines();
					StartCoroutine(FadeCanvas(cross, false));
					valid = true;
				}
			}
		}

		IEnumerator FadeCanvas(CanvasGroup canvas, bool fadeIn)
		{
			float target = fadeIn ? 1 : 0;

			while (Mathf.Abs(canvas.alpha - target) > 0.02f)
			{
				canvas.alpha = Mathf.Lerp(canvas.alpha, target, 20 * Time.deltaTime);
				yield return new WaitForSeconds(Time.deltaTime);
			}

			canvas.alpha = target;
		}
	}
}