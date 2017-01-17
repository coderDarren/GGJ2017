using UnityEngine;
using System.Collections;
using Types;

public class EnvironmentColorModule : MonoBehaviour {

	[System.Serializable]
	public class EnvironmentColors
	{
		public Color backgroundColor1;
		public Color backgroundColor2;
		public Color particlesStartColor;
		public Gradient particlesColorOvertime;
	}

	public static EnvironmentColorModule Instance;

	public SpriteRenderer background;
	public ParticleSystem clouds;
	public GalaxyColor awakeColor;
	public EnvironmentColors blueEnvironment;
	public EnvironmentColors purpleEnvironment;
	public EnvironmentColors redEnvironment;
	public EnvironmentColors pinkEnvironment;
	public EnvironmentColors orangeEnvironment;
	public EnvironmentColors blackEnvironment;
	public EnvironmentColors greenEnvironment;

	EnvironmentColors currentColor;
	GalaxyColor currentColorType;
	Color targetBackgroundColor;
	Gradient targetParticlesGradient;

	void Awake()
	{
		Instance = this;
		SetEnvironmentColor(awakeColor);
	}

	void ApplyColorSettings()
	{
		var c = clouds.colorOverLifetime;
		c.color = currentColor.particlesColorOvertime;
		clouds.startColor = currentColor.particlesStartColor;
		targetParticlesGradient = currentColor.particlesColorOvertime;
		targetBackgroundColor = currentColor.backgroundColor1;
		StopCoroutine("FadeBackground");
		StartCoroutine("FadeBackground");
		//StopCoroutine("FadeClouds");
		//StartCoroutine("FadeClouds");
	}

	IEnumerator FadeClouds()
	{
		var c = clouds.colorOverLifetime;
		Gradient currGradient = c.color.gradient;

		while (!ColorsMatch(clouds.startColor, currentColor.particlesStartColor)) {
			clouds.startColor = Color.Lerp(clouds.startColor, currentColor.particlesStartColor, 5f * Time.deltaTime);
			for (int i = 0; i < targetParticlesGradient.colorKeys.Length; i++) {
				currGradient.colorKeys[i].color.r = Mathf.Lerp(currGradient.colorKeys[i].color.r, targetParticlesGradient.colorKeys[i].color.r, 10 * Time.deltaTime);
			}

			c.color = currGradient;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		clouds.startColor = currentColor.particlesStartColor;
		c.color = targetParticlesGradient;
	}

	IEnumerator FadeBackground()
	{
		SpriteColorOvertime colorOvertime = background.GetComponent<SpriteColorOvertime>();
		colorOvertime.pause = true;
		Color col = background.color;
		float  _rVel = 0;
		float  _gVel = 0;
		float  _bVel = 0;
		float  _aVel = 0;

		while (!ColorsMatch(col, targetBackgroundColor)) {
			col.r = Mathf.SmoothDamp(col.r, targetBackgroundColor.r, ref _rVel, 5);
			col.g = Mathf.SmoothDamp(col.g, targetBackgroundColor.g, ref _gVel, 5);
			col.b = Mathf.SmoothDamp(col.b, targetBackgroundColor.b, ref _bVel, 5);
			col.a = Mathf.SmoothDamp(col.a, targetBackgroundColor.a, ref _aVel, 5);
			background.color = col;
			yield return null;
		}

		background.color = targetBackgroundColor;
		colorOvertime.colors[0] = currentColor.backgroundColor1;
		colorOvertime.colors[1] = currentColor.backgroundColor2;
		colorOvertime.currentColor = currentColor.backgroundColor1;
		colorOvertime.targetColor = currentColor.backgroundColor2;
		colorOvertime.pause = false;
	}

	bool ColorsMatch(Color _c1, Color _c2)
	{
		if (Mathf.Abs(_c1.r - _c2.r) < 0.07f &&
			Mathf.Abs(_c1.g - _c2.g) < 0.07f &&
			Mathf.Abs(_c1.b - _c2.b) < 0.07f &&
			Mathf.Abs(_c1.a - _c2.a) < 0.07f) 
		{
			return true;
		}
		return false;
	}

	public void SetEnvironmentColor(GalaxyColor colorType)
	{
		if (currentColorType == colorType)
			return;

		switch (colorType)
		{
			case GalaxyColor.BLUE:	  currentColor = blueEnvironment;	break;
			case GalaxyColor.PURPLE:  currentColor = purpleEnvironment;	break;
			case GalaxyColor.RED:	  currentColor = redEnvironment;	break;
			case GalaxyColor.PINK:	  currentColor = pinkEnvironment;	break;
			case GalaxyColor.ORANGE:  currentColor = orangeEnvironment;	break;
			case GalaxyColor.BLACK:	  currentColor = blackEnvironment;	break;
			case GalaxyColor.GREEN:	  currentColor = greenEnvironment;	break;
		}
		currentColorType = colorType;
		ApplyColorSettings();
	}
}
