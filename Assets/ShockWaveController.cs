using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockWaveController : MonoBehaviour {

	public float damage = 20f;
	public Vector3 shotFrom;
	public GameObject enemyHitParticles;
	public float knockBack = 3f;

	int layerShootable;
	string enemyTag = "Enemy";

	void Start () {
		layerShootable = LayerMask.NameToLayer("Shootable");
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == layerShootable)
		{
			if (other.CompareTag(enemyTag))
			{
				if (!other.isTrigger)
				{
					EnemyHealth enemy = other.GetComponent<EnemyHealth>();
					enemy.GetHit(damage);
					EnemyMovement enemyMove = other.GetComponent<EnemyMovement>();
					enemyMove.knockback(shotFrom, knockBack, false);

					GameObject tmpParticleSystem = Instantiate(enemyHitParticles, transform.position, transform.rotation) as GameObject;
					ParticleSystem tmpPS = tmpParticleSystem.GetComponent<ParticleSystem>();
					tmpPS.Play();
					Destroy(tmpParticleSystem, 0.5f);
				}
			}
		}
	}
}
