using UnityEngine;
using System.Collections;

public class AntiGravityParticles : SimpleParticleSystem {

	void Start()
	{
		Init();
		StartCoroutine("SpawnParticles");
	}

	IEnumerator SpawnParticles()
	{
		while (true)
		{
			SpawnParticle(transform.position, Vector3.zero, Vector3.zero);
			yield return new WaitForSeconds(spawnRate);
		}
	}

	void Update()
	{
		UpdateParticles();
	}
}
