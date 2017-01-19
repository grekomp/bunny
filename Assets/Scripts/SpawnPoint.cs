using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpawnPoint : MonoBehaviour {

	public GameObject spawnEffect;
	public GameObject prespawnEffect;

	float delay = 2f;

	private void Start()
	{
		NavMeshHit hit;
		if (NavMesh.SamplePosition(transform.position, out hit, 0.5f, NavMesh.AllAreas))
		{
			transform.position = hit.position;
			Spawn();
		}
	}

	public void Spawn()
	{
		SpawnManager.instance.enemiesAlive++;
		SpawnManager.instance.enemiesSpawned++;
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
		GameObject entity = SpawnManager.instance.GetEnemyToSpawn();
		GameObject particles = Instantiate(spawnEffect, transform.position, transform.rotation) as GameObject;
		particles.transform.Rotate(Vector3.right, -90f);
		Destroy(particles, 2f);
		GameObject enemy = Instantiate(entity, transform.position, transform.rotation);
		Animator anim = enemy.GetComponentInChildren<Animator>();
		if (anim != null)
		{
			anim.Play("Walking", -1, Random.Range(0, 1f));
		}
	}
}
