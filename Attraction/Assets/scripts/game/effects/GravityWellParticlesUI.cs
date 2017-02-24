using UnityEngine;
using System.Collections;

public class GravityWellParticlesUI : SimpleParticleSystemUI {

	[System.Serializable]
	public class GravitySystem
	{
		public float radius;
	}

	public GravitySystem gravitySettings = new GravitySystem();

	void Start()
	{
		Init();
		StartCoroutine("SpawnParticles");
	}

	IEnumerator SpawnParticles()
	{
		while (true)
		{
			float randomAngle = Random.Range(0, 359);
			Vector3 newPos = gravitySettings.radius * transform.right;
			newPos = transform.position + Quaternion.Euler(transform.forward * randomAngle) * newPos;
			Vector3 newRot = new Vector3(0, 0, randomAngle);
			Vector3 dir = transform.position - newPos;
			SpawnParticle(newPos, newRot, dir);
			yield return new WaitForSeconds(spawnRate);
		}
	}

	void Update()
	{
		UpdateParticles();
	}
}
