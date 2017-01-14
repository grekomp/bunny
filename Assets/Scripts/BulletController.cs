using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {

	public float bulletDamage;
	public GameObject particles;
	public GameObject enemyHitParticles;
	public Vector3 shotFrom;

	int layerShootable;
	string enemyTag = "Enemy";

	private void Awake()
	{
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
					enemy.GetHit(bulletDamage);
					EnemyMovement enemyMove = other.GetComponent<EnemyMovement>();
					enemyMove.knockback(shotFrom, 1f, false);

					GameObject tmpParticleSystem = Instantiate(enemyHitParticles, transform.position, transform.rotation) as GameObject;
					ParticleSystem tmpPS = tmpParticleSystem.GetComponent<ParticleSystem>();
					tmpPS.Play();
					Destroy(tmpParticleSystem, 0.5f);

					Destroy(gameObject);
				}
			}
			else
			{
				GameObject tmpParticleSystem = Instantiate(particles, transform.position, transform.rotation) as GameObject;
				ParticleSystem tmpPS = tmpParticleSystem.GetComponent<ParticleSystem>();
				tmpPS.Play();
				Destroy(tmpParticleSystem, 0.5f);

				Destroy(gameObject);
			}
		}
	}
}
