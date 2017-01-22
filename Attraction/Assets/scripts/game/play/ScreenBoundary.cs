using UnityEngine;
using System.Collections;

public class ScreenBoundary : MonoBehaviour {

	public ExplosionParticles explosion;

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals("Player")) {
			explosion.gameObject.transform.position = col.gameObject.transform.position;
			explosion.SpawnParticles();
		}
	}
}
