using UnityEngine;
using System.Collections;

public class rocketMovement : MonoBehaviour {

	public float movementSpeed;
	public GameObject explosion;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.up * Time.deltaTime * movementSpeed;
	}

	void OnColliderEnter2D(Collision2D coll) {
		if(coll.gameObject.tag == "Asteroid") {
			Instantiate(explosion,transform.position,transform.rotation);
			Destroy(coll.gameObject);
			Destroy(this.gameObject);
		}
	}
}