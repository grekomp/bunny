using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {

	public GameObject[] pickups;
	public float delay = 0f;
	public float spawnTime = 10f;
	public float range = 6.0f;

	float timer;

	private void Awake()
	{
		timer = delay;
	}

	void Update()
	{
		if (!GameManager.paused)
		{
			timer -= Time.deltaTime;

			if (timer <= 0)
			{
				Vector3 point;
				if (GameManager.RandomOnNavmesh(GameManager.instance.player.transform.position, range, out point))
				{
					point.y = 0.6f;

					int i = (int)Random.Range(0, pickups.Length);
					GameObject pickup = pickups[i];

					Spawn(point, pickup);

					timer = spawnTime;
				}
			}
		}
	}

	public void Spawn(Vector3 location, GameObject pickup)
	{
		Instantiate(pickup, location, Quaternion.identity);
	}
}
