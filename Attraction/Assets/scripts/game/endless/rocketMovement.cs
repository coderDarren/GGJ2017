using UnityEngine;
using System.Collections;

public class rocketMovement : MonoBehaviour {

	public float movementSpeed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * Time.deltaTime * movementSpeed;
	}
}
