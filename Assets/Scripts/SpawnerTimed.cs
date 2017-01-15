using UnityEngine;
using System.Collections;

public class SpawnerTimed : Spawner {

	public float delayFirst = 0f;
	public float spawnTime = 0f;

	private void Start()
	{
		InvokeRepeating("Spawn", delayFirst, spawnTime);
	}
}
