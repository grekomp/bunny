using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour {

	public GameObject[] pickups;
	public float delay = 0f;
	public float spawnTime = 10f;
	public float range = 10.0f;

	float timer;

	private void Awake()
	{
		timer = delay;
	}

	void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			Vector3 point;
			while (!RandomPoint(transform.position, range, out point)) { }
			point.y = 0.6f;

			int i = (int)Random.Range(0, pickups.Length);
			GameObject pickup = pickups[i];

			Spawn(point, pickup);

			timer = spawnTime;
		}
	}

	bool RandomPoint(Vector3 center, float range, out Vector3 result)
	{
		for (int i = 0; i < 30; i++)
		{
			Vector3 randomPoint = center + Random.insideUnitSphere * range;
			NavMeshHit hit;
			if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
			{
				result = hit.position;
				return true;
			}
		}
		result = Vector3.zero;
		return false;
	}

	public void Spawn(Vector3 location, GameObject pickup)
	{
		Instantiate(pickup, location, Quaternion.identity);
	}
}
