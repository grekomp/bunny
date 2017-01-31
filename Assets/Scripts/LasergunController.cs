using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LasergunController : GunController {

	public float range = 10f;
	public float effectDisplayTime = 0.2f;

	public GameObject laserHit;
	public LayerMask layerMask;

	float ammoDeficit = 0f;

	bool isShooting = false;
	LineRenderer line;
	GameObject laserHitInstance;

	override protected void Start()
	{
		base.Start();
		line = GetComponentInChildren<LineRenderer>();
		line.enabled = false;
	}

	override protected void Update()
	{
		base.Update();

		if (GameManager.instance.playerAlive && !GameManager.paused)
		{
			if (Input.GetButton("Fire1") && ammo > 0)
			{
				if (isShooting == false)
				{
					laserHitInstance = Instantiate(laserHit);
				}
				isShooting = true;
			}
			else
			{
				if (isShooting)
				{
					Destroy(laserHitInstance);
				}
				isShooting = false;
			}

			transform.rotation = Quaternion.LookRotation(transform.position - GameManager.instance.cursorLocation);
			if (isShooting)
			{
				line.enabled = true;
				Ray ray;
				RaycastHit rayHit;
				line.SetPosition(0, emmiter.transform.position);
				ammoDeficit += Time.deltaTime * fireRate;
				if (ammoDeficit >= 1)
				{
					ammo--;
					ammoDeficit -= 1;
				}
				UpdateAmmoCounter();

				ray = new Ray(emmiter.transform.position, transform.forward * -1);
				if (Physics.Raycast(ray, out rayHit, range, layerMask))
				{
					line.SetPosition(1, rayHit.point);
					laserHitInstance.transform.position = rayHit.point;

					EnemyHealth enemy = rayHit.collider.GetComponent<EnemyHealth>();
					if (enemy != null)
					{
						enemy.GetHit(dmgPerShot * fireRate * Time.deltaTime);
						EnemyMovement enemyMove = rayHit.collider.GetComponent<EnemyMovement>();
						enemyMove.knockback(emmiter.transform.position, knockBack * Time.deltaTime * fireRate, false);
					}
				}
				else
				{
					line.SetPosition(1, ray.origin + ray.direction * range);
					laserHitInstance.transform.position = ray.origin + ray.direction * range;
				}

				laserHitInstance.transform.rotation = Quaternion.LookRotation(emmiter.transform.position - laserHitInstance.transform.position);
				/*
				if (timer <= (1 / fireRate) - effectDisplayTime)
				{
					line.enabled = false;
				}*/
			}
			else
			{
				line.enabled = false;
			}
		}
	}

	override public bool Shoot()
	{
		/*if (timer <= 0 && ammo > 0)
		{
			ammo--;

			

			line.enabled = true;

			UpdateAmmoCounter();

			timer = 1f / fireRate;

			return true;
		}
		*/
		return false;
	}

	private void OnDestroy()
	{
		if (laserHitInstance != null) Destroy(laserHitInstance);
	}
}
