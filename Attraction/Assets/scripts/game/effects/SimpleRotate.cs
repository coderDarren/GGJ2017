using UnityEngine;
using System.Collections;

public class SimpleRotate : MonoBehaviour {

	public float rotateSpeed;

	void Update()
	{
		transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);
	}
}
