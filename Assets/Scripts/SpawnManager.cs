using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour {

	public static SpawnManager instance;

	public GameObject player;
	public float spawnRadius = 8f;

	public GameObject[] enemies;
	public SpawnGroup[] spawnGroups;

	int maxEnemiesAlive = 60;

	public int startingEnemiesPerLevel = 20;
	int enemiesPerLevel = 20;
	public int enemiesAlive = 0;
	public int enemiesSpawned = 0;
	public float spawnRate = 1f;
	float defaultDelay = 1f;
	public float increasePerLevel = 0.2f;

	float timer = 0f;
	bool spawning = true;

	private void Start()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		player = GameManager.instance.player;

		enemiesPerLevel = startingEnemiesPerLevel;
	}

	private void Update()
	{
		if (spawning)
		{
			timer -= Time.deltaTime;

			if (timer <= 0 && enemiesAlive < maxEnemiesAlive)
			{
				SpawnGroup();
				timer = defaultDelay * (1 / spawnRate);
			}

			if (enemiesSpawned >= enemiesPerLevel)
			{
				spawning = false;
			}
		}
		else
		{
			if (enemiesAlive == 0)
			{
				GameManager.instance.NextLevel();
				enemiesSpawned = 0;
				enemiesPerLevel += (int)(enemiesPerLevel * increasePerLevel * GameManager.instance.difficultyModifier);
				spawnRate += spawnRate * increasePerLevel * GameManager.instance.difficultyModifier;
				spawning = true;
			}
		}
	}

	public void SpawnGroup()
	{
		Vector2 randOnCircle = Random.insideUnitCircle * spawnRadius;
		Vector3 randPos = new Vector3(randOnCircle.x, 0, randOnCircle.y);
		randPos += player.transform.position;

		Instantiate(spawnGroups[Random.Range(0, spawnGroups.Length)], randPos, Quaternion.identity);
	}

	public GameObject GetEnemyToSpawn()
	{
		return enemies[Random.Range(0, enemies.Length)];
	}
}
