using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;

	private void Start()
	{
		health = maxHealth;
	}

	public void GetHit(float bulletDamage)
	{
		health -= bulletDamage;

		//Debug.Log("Got hit - hp:" + health);

		if(health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Destroy(gameObject);
	}
}
