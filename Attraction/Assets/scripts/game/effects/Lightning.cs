using UnityEngine;
using System.Collections;
using UnityStandardAssets.ImageEffects;

public class Lightning : MonoBehaviour {

	public static Lightning Instance;

	public float flashIntensity;
	public float flashSpeed;
	public float reduceSpeed;

	BloomOptimized bloom;

	void Start()
	{
		Instance = this;
		bloom = GetComponent<BloomOptimized>();
	}

	public void Flash()
	{
		StopCoroutine("FlashIntensity");
		StopCoroutine("ReduceIntensity");
		StartCoroutine("FlashIntensity");
	}

	IEnumerator FlashIntensity()
	{
		while (Mathf.Abs(bloom.intensity - flashIntensity) > 0.01f) {
			bloom.intensity = Mathf.Lerp(bloom.intensity, flashIntensity, flashSpeed * Time.deltaTime);
			yield return null;
		}

		bloom.intensity = flashIntensity;
		StartCoroutine("ReduceIntensity");
	}

	IEnumerator ReduceIntensity()
	{
		while (bloom.intensity> 0.01f) {
			bloom.intensity = Mathf.Lerp(bloom.intensity, 0, reduceSpeed * Time.deltaTime);
			yield return null;
		}

		bloom.intensity = 0;
	}
}
