using UnityEngine;
using System.Collections;

public class GravityWellParticles : SimpleParticleSystem {

	[System.Serializable]
	public class GravitySystem
	{
		public float radius;
	}

	public GravitySystem gravitySettings = new GravitySystem();

	void Start()
	{
		StartCoroutine("SpawnParticles");
	}

	IEnumerator SpawnParticles()
	{
		while (true)
		{

			yield return new WaitForSeconds(spawnRate);
		}
	}
}
