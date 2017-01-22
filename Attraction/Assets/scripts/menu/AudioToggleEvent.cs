using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Types;
using Util;

namespace Menu {

	public class AudioToggleEvent : ButtonEvent {

		public Types.AudioType audioType;
		public Sprite offImage;
		public Sprite onImage;
		public Color offColor;
		public Color onColor;

		Image img;

		void Start()
		{
			InitButton();
			img = GetComponent<Image>();
			ConfigureImage();
		}

		void ConfigureImage()
		{
			int status = 0;
			switch (audioType)
			{
				case Types.AudioType.MUSIC:
					status = AudioManager.Instance.GetStatus(Types.AudioType.MUSIC);
					break;
				case Types.AudioType.SFX:
					status = AudioManager.Instance.GetStatus(Types.AudioType.SFX);
					break;
			}

			if (status != 1) {
				img.sprite = offImage;
				img.color = offColor;
			} else {
				img.sprite = onImage;
				img.color = onColor;
			}
		}

		public override void OnItemUp()
		{
			base.OnItemUp();
			if (audioType == Types.AudioType.MUSIC)
				AudioManager.Instance.ToggleMusic();
			else 
				AudioManager.Instance.ToggleSFX();
			ConfigureImage();
		}
	}
}