using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LasergunController : GunController {

	public float range = 10f;
	public float effectDisplayTime = 0.2f;

	int layer;

	LineRenderer line;

	override protected void Start()
	{
		base.Start();
		layer = LayerMask.GetMask("Shootable");
		line = GetComponentInChildren<LineRenderer>();
		line.enabled = false;
	}

	override protected void Update()
	{
		base.Update();

		if (timer <= (1 / fireRate) - effectDisplayTime)
		{
			line.enabled = false;
		}
		else
		{
			line.SetPosition(0, emmiter.transform.position);
		}
	}

	override public bool Shoot()
	{
		if (timer <= 0 && ammo > 0)
		{
			ammo--;

			Ray ray;
			RaycastHit rayHit;
			line.SetPosition(0, emmiter.transform.position);


			ray = new Ray(emmiter.transform.position, transform.forward * -1);
			if (Physics.Raycast(ray, out rayHit, range, layer))
			{
				line.SetPosition(1, rayHit.point);
				EnemyHealth enemy = rayHit.collider.GetComponent<EnemyHealth>();
				if (enemy != null) enemy.GetHit(dmgPerShot);
			}
			else
			{
				line.SetPosition(1, ray.origin + ray.direction * range);
			}

			line.enabled = true;

			UpdateAmmoCounter();

			timer = 1f / fireRate;

			return true;
		}

		return false;
	}
}
