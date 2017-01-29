using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeaponController : GunController {

	public GameObject attackObject;

	public float attackWaveSpeed = 1f;
	public float lifeTime = 1f;

	public override bool Shoot()
	{
		if (timer <= 0)
		{
			GameObject attack = Instantiate(attackObject, emmiter.transform.position, emmiter.transform.rotation) as GameObject;
			attack.transform.Rotate(Vector3.right * -90f);

			Rigidbody tmpRb = attack.GetComponent<Rigidbody>();
			tmpRb.AddForce(transform.forward * attackWaveSpeed * -1);

			ShockWaveController tmpBulletController = attack.GetComponent<ShockWaveController>();
			tmpBulletController.damage = dmgPerShot;
			tmpBulletController.shotFrom = emmiter.transform.position;
			tmpBulletController.knockBack = knockBack;

			Destroy(attack, lifeTime);
			timer = 1f / fireRate;

			return true;
		}

		return false;
	}
}
