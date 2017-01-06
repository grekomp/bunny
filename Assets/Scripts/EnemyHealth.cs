using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;

	public int score = 10;

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
		GameManager.instance.AddScore(score);
		Destroy(gameObject);
	}
}
