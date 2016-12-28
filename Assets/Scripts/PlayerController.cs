using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;
	public GunController gun;
	public Slider healthSlider;
	public Image damageOverlay;
	public Color damageFlashColor;
	public float flashTime = 2f;

	bool damaged;
	bool isTransitioningHealth;
	float damagedTime;

	private void Awake()
	{
		gun = GetComponentInChildren<GunController>();

		health = maxHealth;

		healthSlider.value = health / maxHealth;
	}

	private void Update()
	{
		if (damaged)
		{
			damagedTime = Time.time;
			damageOverlay.color = damageFlashColor;
			damaged = false;
		} else
		{
			damageOverlay.color = Color.Lerp(damageFlashColor, Color.clear, (Time.time - damagedTime) / flashTime);
		}
	}

	private void FixedUpdate () {
		if (Input.GetMouseButton(0))
		{
			gun.Shoot();
		}
	}

	public void GetHit(float damage, GameObject attacker)
	{
		health -= damage;
		healthSlider.value = health / maxHealth;

		damaged = true;

		//Debug.Log("Hit");
		if (health <= 0)
		{
			Die();
		}
	}

	public void Die()
	{
		Debug.Log("Player dead");
	}
}
