using UnityEngine;
using System.Collections;

public class asteroidMovement : MonoBehaviour {

	float movementSpeed;
	Rigidbody2D rb;
	
	void Start () {
		movementSpeed = Random.Range(50f,200f);
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(-transform.up * movementSpeed);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if(coll.gameObject.tag == "AsteroidDestroyer") {
			Destroy(this.gameObject);
		}
	}
}