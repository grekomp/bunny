using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public abstract class GunController : MonoBehaviour {

	public float dmgPerShot = 20f;
	public float fireRate = 1f;

	public int ammo;
	public int maxAmmo;

	public GameObject emmiter;
	public Text ammoText;
	public Slider ammoSlider;
	public Vector3 gunPosition;

	protected float timer;

	virtual protected void Start()
	{
		ammoText = GameObject.FindGameObjectWithTag("AmmoText").GetComponent<Text>();
		ammoSlider = GameObject.FindGameObjectWithTag("AmmoSlider").GetComponent<Slider>();

		timer = 1f / fireRate;
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
	}

}
