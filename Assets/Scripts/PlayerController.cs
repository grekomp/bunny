using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;
	public GameObject gun;
	public GameObject defaultGun;
	public GunController gunController;
	public Color damageFlashColor;
	public float flashTime = 2f;
	public float healthTransitionTime = 0.1f;

	public float bombDamage = 100f;
	public float bombRadius = 3f;
	public int bombs;
	public int maxBombs;
	public float bombDelay = 0.5f;
	public GameObject bomb;
	public float bombThrowRange = 2f;

	public Text healthText;
	public Slider healthSlider;
	public Image damageOverlay;

	Text bombText;
	Text bombMaxText;

	bool damaged = false;
	float damagedTime = -10;
	float previousHealth;

	float bombUsedTime;

	private void Start()
	{
		GameManager.instance.player = gameObject;

		gunController = GetComponentInChildren<GunController>();
		gun = gunController.gameObject;
		bombText = GameManager.ui.bombsText;
		bombMaxText = GameManager.ui.maxBombsText;

		health = maxHealth;
		previousHealth = maxHealth;

		healthText.text = health.ToString();
		healthSlider.value = health / maxHealth;

		UpdateBombCounter();
	}

	private void Update()
	{
		if (GameManager.instance.playerAlive && !GameManager.paused)
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

			if (Input.GetButton("Jump"))
			{
				UseBomb();
			}
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

	public void UseBomb()
	{
		if (bombs > 0 && Time.time - bombUsedTime >= bombDelay)
		{
			bombs--;
			bombUsedTime = Time.time;
			UpdateBombCounter();

			GameObject newBomb = Instantiate(bomb, transform.position, transform.rotation) as GameObject;
			newBomb.GetComponent<Rigidbody>().AddForce(transform.forward * bombThrowRange * -1);
			newBomb.GetComponent<Rigidbody>().AddForce(transform.up * 3f);
			Bomb newBombController = newBomb.GetComponent<Bomb>();
			newBombController.damage = bombDamage;
			newBombController.radius = bombRadius;
		}
	} 

	public void UpdateBombCounter()
	{
		bombText.text = bombs.ToString();
		bombMaxText.text = maxBombs.ToString();
	}

	public int AddBombs(int bombs)
	{
		this.bombs += bombs;
		if(this.bombs > maxBombs)
		{
			bombs = this.bombs - maxBombs;
			this.bombs = maxBombs;
			UpdateBombCounter();
			return bombs;
		}

		UpdateBombCounter();
		return 0;
	}

	public float AddHealth(float health)
	{
		previousHealth = this.health;
		this.health += health;
		if (this.health > maxHealth)
		{
			health = this.health - maxHealth;
			this.health = maxHealth;

			healthText.text = this.health.ToString();
			return health;
		}

		healthText.text = this.health.ToString();
		return 0;
	}

	public void NoAmmo()
	{
		GameObject tmpWeapon = Instantiate(defaultGun, transform.position, transform.rotation, transform) as GameObject;
		tmpWeapon.transform.localPosition = tmpWeapon.GetComponent<GunController>().gunPosition;

		ChangeGun(tmpWeapon);
	}

	public void Die()
	{
		GameManager.instance.PlayerDead();
	}
}
