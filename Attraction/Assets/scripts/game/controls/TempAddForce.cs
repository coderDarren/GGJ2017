using UnityEngine;
using System.Collections;

public class TempAddForce : MonoBehaviour {

	public ForceMode2D forceMode;
	public GameObject well;
	public float speed = 5f;
	public float angle;
	public float forceAmount = 10f;
	Vector3 relativeDirection;
	public float G = 1;

	void FixedUpdate () {				
		Vector2 dist = new Vector2 (well.transform.position.x - transform.position.x, well.transform.position.y - transform.position.y);
		float r = Vector2.SqrMagnitude(dist);
		Vector2 tdist = new Vector2 (dist.y, -dist.x).normalized; // 2D vector prependicular to the dist vector .
		float force = Mathf.Sqrt (G * ((this.GetComponent<Rigidbody2D>().mass + well.GetComponent<Rigidbody2D>().mass) / r)); // Calculate the velocity .
		this.GetComponent<Rigidbody2D>().velocity =  tdist * force;
	}
}
