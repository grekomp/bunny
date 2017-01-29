using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class GunController : MonoBehaviour {

	public float dmgPerShot = 20f;
	public float fireRate = 1f;

	public int ammo;
	public int maxAmmo;

	public float knockBack = 2f;

	public GameObject emmiter;
	public Text ammoText;
	public Slider ammoSlider;
	public Vector3 gunPosition;
	Animator ammoLowAnimator;

	protected float timer;

	virtual protected void Start()
	{
		ammoText = GameManager.ui.ammoText;
		ammoSlider = GameManager.ui.ammoSlider;
		ammoLowAnimator = GameManager.ui.ammoLowText.gameObject.GetComponent<Animator>();

		timer = 0;
		UpdateAmmoCounter();
	}

	virtual protected void Update()
	{
		timer -= Time.deltaTime;
	}

	abstract public bool Shoot();

	public int AddAmmo(int count)
	{
		ammo += count;
		if (ammo > maxAmmo)
		{
			count = ammo - maxAmmo;
			ammo = maxAmmo;
		}

		UpdateAmmoCounter();
		return 0;
	}

	public void UpdateAmmoCounter()
	{
		ammoText.text = ammo.ToString();
		ammoSlider.value = (float)ammo / (float)maxAmmo;
		if((float)ammo / (float)maxAmmo < 0.3f)
		{
			ammoLowAnimator.SetBool("AmmoLow", true);
		} else
		{
			ammoLowAnimator.SetBool("AmmoLow", false);
		}

		if(ammo == 0)
		{
			GameManager.instance.player.GetComponent<PlayerController>().NoAmmo();
		}
	}

}
