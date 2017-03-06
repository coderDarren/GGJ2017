using UnityEngine;
using System.Collections;

public class asteroidMovement : MonoBehaviour {

	float movementSpeed;
	
	void Start () {
		movementSpeed = Random.Range(1f,10f);
	}

	// Update is called once per frame
	void Update () {
		transform.position -= transform.up * Time.deltaTime * movementSpeed;
	}
}