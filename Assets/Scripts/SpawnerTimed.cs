using UnityEngine;
using System.Collections;

public class SpawnerTimed : Spawner {

	public float delay = 0f;
	public float spawnTime = 0f;
	public int quantity = 1;

	private void Start()
	{
		if(quantity > 0 && spawnTime > 0)
		{
			InvokeRepeating("Spawn", delay, spawnTime);
		}
		else if(quantity == 0 && spawnTime > 0)
		{
			InvokeRepeating("DoSpawn", delay, spawnTime);
		}
	}

	public void Spawn()
	{
		if (quantity > 0)
		{
			quantity--;
			DoSpawn();
		}
	}
}
