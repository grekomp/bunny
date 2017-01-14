using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public float damage = 100f;
	public float radius = 2f;
	public float delay = 3f;
	public GameObject explosionEffect;
	public bool auto = true;
	public float knockback = 10f;

	float timer;
	int layerShootable;
	bool isExploding;

	protected void Awake()
	{
		layerShootable = LayerMask.NameToLayer("Shootable");

		if (auto)
		{
			timer = delay;
		}
	}

	protected void Update()
	{
		if (auto)
		{
			timer -= Time.deltaTime;
			if (timer <= 0 && !isExploding)
			{
				Explode();
			}
		}
	}

	public void Explode()
	{
		GameObject effect = Instantiate(explosionEffect, transform.position, transform.rotation) as GameObject;
		effect.transform.Rotate(new Vector3(1, 0, 0), -90f);
		effect.transform.Translate(Vector3.up * 0.3f, Space.World);

		SphereCollider explosionCollider = transform.GetComponent<SphereCollider>();
		explosionCollider.radius = radius;
		explosionCollider.enabled = true;

		isExploding = true;

		Destroy(gameObject, 0.1f);
		Destroy(effect, 1f);
	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Entered" + other.name);
		if (isExploding && other.gameObject.layer == layerShootable)
		{
			if (other.CompareTag("Enemy") && !other.isTrigger)
			{
				EnemyHealth enemy = other.GetComponent<EnemyHealth>();
				EnemyMovement enemyMove = other.GetComponent<EnemyMovement>();
				enemyMove.knockback(transform.position, radius * 2f);
				enemy.GetHit(damage);
			}
		}
	}
}
