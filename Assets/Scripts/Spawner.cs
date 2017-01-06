using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject entity;
	public GameObject spawnParticles;

	public void Spawn(float delay)
	{
		Invoke("DoSpawn", delay);
	}

	protected void DoSpawn()
	{
		GameObject particles = Instantiate(spawnParticles, transform.position, transform.rotation) as GameObject;
		Instantiate(entity, transform.position, transform.rotation);

		Destroy(particles, 2f);
	}
}
