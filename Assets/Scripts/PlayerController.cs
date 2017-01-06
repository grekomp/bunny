using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;
	public GameObject gun;
	public GunController gunController;
	public Slider healthSlider;
	public Text healthText;
	public Image damageOverlay;
	public Color damageFlashColor;
	public float flashTime = 2f;
	public float healthTransitionTime = 0.1f;

	bool damaged = false;
	float damagedTime = -10;
	float previousHealth;

	private void Awake()
	{
		gunController = GetComponentInChildren<GunController>();

		health = maxHealth;
		previousHealth = maxHealth;

		healthText.text = health.ToString();
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
			healthSlider.value = Mathf.Lerp(previousHealth / maxHealth, health / maxHealth, (Time.time - damagedTime) / healthTransitionTime);
			damageOverlay.color = Color.Lerp(damageFlashColor, Color.clear, (Time.time - damagedTime) / flashTime);
		}

		if (Input.GetButton("Fire1"))
		{
			gunController.Shoot();
		}
	}

	public void ChangeGun(GameObject gunNew)
	{
		Destroy(gun);
		gun = Instantiate(gunNew, new Vector3(), new Quaternion(), gameObject.transform) as GameObject;
		gunController = gun.GetComponent<GunController>();

		gun.transform.localPosition = gunController.gunPosition;
	}

	public void GetHit(float damage, GameObject attacker)
	{
		previousHealth = health;
		health -= damage;
		healthText.text = health.ToString();

		damaged = true;

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
