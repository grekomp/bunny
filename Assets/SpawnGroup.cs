using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGroup : MonoBehaviour {
	public GameObject spawnPointPrefab;

	public Vector3[] spawnPoints;

	private void Start()
	{
		foreach(Vector3 spawnPoint in spawnPoints)
		{
			Instantiate(spawnPointPrefab, spawnPoint + transform.position, Quaternion.identity);
		}

		Destroy(gameObject);
	}
}
