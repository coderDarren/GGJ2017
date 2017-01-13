using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorOvertime : MonoBehaviour {

	// ---------------------- PRIVATE MEMBERS ----------------------
	Image  _image;
	Text   _text;
	int    _targetColorIndex;
	Color  _currentColor;
	Color  _targetColor;

	///Velocities for smoothing the color properties
	float  _rVel;
	float  _gVel;
	float  _bVel;
	float  _aVel;

	// ---------------------- PUBLIC MEMBERS ----------------------
	public enum UIType 
	{
		IMAGE,
		TEXT
	}
	public UIType  uiType;
	public Color[] colors;
	public float   colorSmooth;
	public bool    smoothDamp;

	// ---------------------- UNITY EVENT FUNCTIONS ----------------------
	void Start()
	{
		_currentColor = GetNextColor();
		_targetColor = GetNextColor();
		Configure(uiType);
		StartCoroutine("TransitionToNextColor");
	}

	// ---------------------- PRIVATE FUNCTIONS ----------------------

	void Configure(UIType _type)
	{
		if (_type == UIType.IMAGE)
		{
			_image = GetComponent<Image>();
			_image.color = _currentColor;
		}
		else {
			_text = GetComponent<Text>();
			_text.color = _currentColor;
		}
	}

	void SetColor(UIType _type)
	{
		if (_type == UIType.IMAGE)
		{
			_image.color = _currentColor;
		}
		else {
			_text.color = _currentColor;
		}
	}

	IEnumerator TransitionToNextColor()
	{
		while (true)
		{
			if (!smoothDamp)
			{
				_currentColor = Color.Lerp(_currentColor, _targetColor, colorSmooth * Time.deltaTime);
			}
			else 
			{
				_currentColor.r = Mathf.SmoothDamp(_currentColor.r, _targetColor.r, ref _rVel, colorSmooth);
				_currentColor.g = Mathf.SmoothDamp(_currentColor.g, _targetColor.g, ref _gVel, colorSmooth);
				_currentColor.b = Mathf.SmoothDamp(_currentColor.b, _targetColor.b, ref _bVel, colorSmooth);
				_currentColor.a = Mathf.SmoothDamp(_currentColor.a, _targetColor.a, ref _aVel, colorSmooth);
			}
			SetColor(uiType);
			CheckColorMatch(_currentColor, _targetColor);

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
			_targetColor = GetNextColor();
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
