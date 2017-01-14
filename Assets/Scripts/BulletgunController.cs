using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BulletgunController : GunController {

	public float bulletSpeed = 10f;
	public GameObject bullet;

	override protected void Start()
	{
		base.Start();
	}

	override protected void Update()
	{
		base.Update();
	}

	override public bool Shoot()
	{
		if (timer <= 0 && ammo > 0)
		{
			ammo--;
			GameObject tmpBullet = Instantiate(bullet, emmiter.transform.position, emmiter.transform.rotation) as GameObject;
			tmpBullet.transform.Rotate(Vector3.up * -90f);

			Rigidbody tmpRb = tmpBullet.GetComponent<Rigidbody>();
			tmpRb.AddForce(transform.forward * bulletSpeed * -1);

			BulletController tmpBulletController = tmpBullet.GetComponent<BulletController>();
			tmpBulletController.bulletDamage = dmgPerShot;
			tmpBulletController.shotFrom = emmiter.transform.position;
			UpdateAmmoCounter();

			Destroy(tmpBullet, 2.5f);
			timer = 1f / fireRate;

			return true;
		}

		return false;
	}
}
