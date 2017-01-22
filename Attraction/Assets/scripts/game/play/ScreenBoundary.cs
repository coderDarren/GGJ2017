using UnityEngine;
using System.Collections;

public class ScreenBoundary : MonoBehaviour {

	public ExplosionParticles explosion;
	public GameObject playerRef;

	void Start()
	{
		playerRef = GameObject.FindGameObjectWithTag("Player");
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Player")) {
			explosion.gameObject.transform.position = col.gameObject.transform.position;
			explosion.SpawnParticles();
			playerRef.GetComponent<ShipController>().ReduceLives();
		}
	}
}
