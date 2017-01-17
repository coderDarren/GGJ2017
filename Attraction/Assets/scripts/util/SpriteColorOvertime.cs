using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteColorOvertime : MonoBehaviour {

	// ---------------------- PRIVATE MEMBERS ----------------------
	SpriteRenderer  _sprite;
	int    _targetColorIndex;
	[HideInInspector] public Color currentColor;
	[HideInInspector] public Color targetColor;

	///Velocities for smoothing the color properties
	float  _rVel;
	float  _gVel;
	float  _bVel;
	float  _aVel;

	// ---------------------- PUBLIC MEMBERS ----------------------
	public Color[] colors;
	public float   colorSmooth;
	public bool    smoothDamp;
	public bool    pause;
	// ---------------------- UNITY EVENT FUNCTIONS ----------------------
	void Start()
	{
		_sprite = GetComponent<SpriteRenderer>();
		currentColor = GetNextColor();
		targetColor = GetNextColor();
		StartCoroutine("TransitionToNextColor");
	}

	// ---------------------- PRIVATE FUNCTIONS ----------------------

	void SetColor()
	{
		_sprite.color = currentColor;
	}

	IEnumerator TransitionToNextColor()
	{
		while (true)
		{
			if (pause)
				yield return null;
			else {
				if (!smoothDamp)
				{
					currentColor = Color.Lerp(currentColor, targetColor, colorSmooth * Time.deltaTime);
				}
				else 
				{
					currentColor.r = Mathf.SmoothDamp(currentColor.r, targetColor.r, ref _rVel, colorSmooth);
					currentColor.g = Mathf.SmoothDamp(currentColor.g, targetColor.g, ref _gVel, colorSmooth);
					currentColor.b = Mathf.SmoothDamp(currentColor.b, targetColor.b, ref _bVel, colorSmooth);
					currentColor.a = Mathf.SmoothDamp(currentColor.a, targetColor.a, ref _aVel, colorSmooth);
				}
				SetColor();
				CheckColorMatch(currentColor, targetColor);
			}
			yield return null;
		}
	}

	/// <summary>
	/// Assigns a new target color if the current and target are close enough in value
	/// </summary>
	void CheckColorMatch(Color _c1, Color _c2)
	{
		if (Mathf.Abs(_c1.r - _c2.r) < 0.1f &&
			Mathf.Abs(_c1.g - _c2.g) < 0.1f &&
			Mathf.Abs(_c1.b - _c2.b) < 0.1f &&
			Mathf.Abs(_c1.a - _c2.a) < 0.1f) 
		{
			targetColor = GetNextColor();
		}
	}

	/// <summary>
	/// Randomly chooses the next color to transition to in the colors array
	/// </summary>
	Color GetNextColor()
	{
		_targetColorIndex = Random.Range(0, colors.Length);
		return colors[_targetColorIndex];
	}

}
