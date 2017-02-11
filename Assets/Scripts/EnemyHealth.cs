using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;
	public bool isAlive = true;

	public int score = 10;
	Color flashColor = new Color(0.6f,0,0);
	Color defaultEmissionColor;

	Renderer rend;
	AudioSource audioSrc;

	public GameObject bloodDecal;
	public AudioClip[] deathSounds;

	private void Awake()
	{
		health = maxHealth;

		audioSrc = gameObject.GetComponent<AudioSource>();

		rend = transform.GetComponentInChildren<SkinnedMeshRenderer>();
		if(rend == null) rend = transform.GetComponentInChildren<MeshRenderer>();
		defaultEmissionColor = rend.material.GetColor("_EmissionColor");
	}

	public void GetHit(float bulletDamage)
	{
		health -= bulletDamage;

		flashStart();

		Invoke("flashEnd", 0.1f);

		if(health <= 0 && isAlive)
		{
			Die();
		}
	}

	public void flashStart()
	{
		rend.material.SetColor("_EmissionColor", flashColor);
	}

	public void flashEnd()
	{
		rend.material.SetColor("_EmissionColor", defaultEmissionColor);
	}

	public void Die()
	{
		audioSrc.clip = deathSounds[Random.Range(0, deathSounds.Length)];
		audioSrc.Play();

		GameObject blood = Instantiate(bloodDecal, transform.position, transform.rotation) as GameObject;
		Destroy(blood, 120f);

		isAlive = false;
		gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
		gameObject.layer = 0;
		GameManager.instance.AddScore(score);
		SpawnManager.instance.enemiesAlive--;
		EnemyMovement move = gameObject.GetComponent<EnemyMovement>();
		gameObject.GetComponent<EnemyAttack>().isAlive = false;
		move.Die();
	}
}
