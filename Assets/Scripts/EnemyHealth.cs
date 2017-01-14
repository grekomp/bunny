using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;

	public int score = 10;
	public Color flashColor = new Color(0.3f,0,0);
	Color defaultEmissionColor;

	MeshRenderer rend;

	private void Awake()
	{
		health = maxHealth;

		rend = transform.GetComponentInChildren<MeshRenderer>();
		defaultEmissionColor = rend.material.GetColor("_EmissionColor");
	}

	public void GetHit(float bulletDamage)
	{
		health -= bulletDamage;

		flashStart();

		Invoke("flashEnd", 0.1f);

		if(health <= 0)
		{
			Die();
		}
	}

	public void flashStart()
	{
		Debug.Log("Flashing");
		rend.material.SetColor("_EmissionColor", flashColor);
	}

	public void flashEnd()
	{
		rend.material.SetColor("_EmissionColor", defaultEmissionColor);
	}

	public void Die()
	{
		GameManager.instance.AddScore(score);
		Destroy(gameObject);
	}
}
