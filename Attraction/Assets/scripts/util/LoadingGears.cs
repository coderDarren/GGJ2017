using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace Util {

	public class LoadingGears : MonoBehaviour {

		[System.Serializable]
		public class GearOption
		{
			public Image img;
			public float turnDirection;
			public float fillSpeed;
			public float rotSpeed;
		}

		public GearOption gear1, gear2, gear3;

		bool _loading = true;

		void Start()
		{
			StartCoroutine("TurnGear", gear1);
			StartCoroutine("TurnGear", gear2);
			StartCoroutine("TurnGear", gear3);
		}

		IEnumerator TurnGear(GearOption _gear)
		{
			Image img = _gear.img;
			RectTransform rect = img.GetComponent<RectTransform>();
			float startDir = _gear.turnDirection;
			float alternateDir = startDir == 1 ? -1 : 1;
			float currentFill = alternateDir;
			float speed = _gear.fillSpeed;
			float rot = _gear.rotSpeed;
			float targetFill = 1;

			Vector3 scale = rect.localScale;
			scale.x = startDir;
			rect.localScale = scale;

			float vel = 0;
			float distTraveled = 0;

			while (true)
			{
				if (_loading)
				{
					distTraveled = targetFill == 1 ? 1 - img.fillAmount : img.fillAmount;
					distTraveled += 0.1f;
					img.fillAmount = Mathf.SmoothDamp(img.fillAmount, targetFill, ref vel, speed * distTraveled);
					rect.rotation *= Quaternion.Euler(0, 0, rot * _gear.turnDirection * Time.deltaTime);

					if (Mathf.Abs(img.fillAmount - targetFill) < 0.01f)
					{
						img.fillAmount = targetFill;
						scale.x = alternateDir;
						rect.localScale = scale;

						float temp = startDir;
						startDir = alternateDir;
						alternateDir = temp;

						if (targetFill == 1) targetFill = 0; else targetFill = 1;
					}
				}

				yield return new WaitForSeconds(Time.deltaTime);
			}
		}

		public void StartLoading()
		{
			_loading = true;
		}

		public void StopLoading()
		{
			_loading = false;
		}
	}
}