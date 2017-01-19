using UnityEngine;
using System.Collections;

public class shipRotator : MonoBehaviour {

	public float rotationAmount = 0.4f;
	private Vector3 mouseRef;
	private Vector3 mouseOffset;
	private Vector3 rotation;
	private bool rotating = false;

	// Use this for initialization
	void Start () {
		rotation = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(rotating) {
			mouseOffset = (Input.mousePosition - mouseRef);
			rotation.z = -(mouseOffset.x + mouseOffset.y) * rotationAmount;

			transform.Rotate(rotation);

			mouseRef = Input.mousePosition;
		}
	}

	void OnMouseDown()
	{
		Debug.Log("Mouse Down");
		rotating = true;
		mouseRef = Input.mousePosition;
	}

	void OnMouseUp()
	{
		Debug.Log("Mouse Up");
		rotating = false;
	}
}
