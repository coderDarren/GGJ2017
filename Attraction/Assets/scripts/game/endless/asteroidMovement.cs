using UnityEngine;
using System.Collections;

public class asteroidMovement : MonoBehaviour {

	float movementSpeed;
	Rigidbody2D rb;
	
	void Start () {
		movementSpeed = Random.Range(10f,100f);
		rb = GetComponent<Rigidbody2D>();
		rb.AddForce(-transform.up * movementSpeed);
	}

	void FixedUpdate() {

	}

	void OnTriggerEnter(Collider coll) {
		if(coll.gameObject.tag == "AsteroidDestroyer") {
			Destroy(this.gameObject);
		}
	}
}