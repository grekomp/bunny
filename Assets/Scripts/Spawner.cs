using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject entity;
	public GameObject spawnParticles;

	public GameObject prespawnEffect;

	float delay = 2f;

	public void Spawn()
	{
		PreSpawn();
		Invoke("PostSpawn", delay);
	}

	protected void PreSpawn()
	{
		GameObject effect = Instantiate(prespawnEffect, transform.position, transform.rotation) as GameObject;
		Destroy(effect, delay);
	}

	protected void PostSpawn()
	{
		GameObject particles = Instantiate(spawnParticles, transform.position, transform.rotation) as GameObject;
		particles.transform.Rotate(Vector3.right, -90f);
		Destroy(particles, 2f);
		Instantiate(entity, transform.position, transform.rotation);
	}
}
