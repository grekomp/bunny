using UnityEngine;
using System.Collections;

public class GunController : MonoBehaviour {

	public float dmgPerShot = 20f;
	public float fireRate = 1f;
	public float reloadTime = 3f;

	public GameObject emmiter;
	public GameObject bullet;
	public float bulletSpeed = 10f;

	float timer;

	private void Start()
	{
		timer = 0f;
	}

	private void FixedUpdate()
	{
		timer += Time.deltaTime;
	}

	public bool Shoot()
	{
		if(timer >= 1f / fireRate)
		{
			GameObject tmpBullet = Instantiate(bullet, emmiter.transform.position, emmiter.transform.rotation) as GameObject;
			tmpBullet.transform.Rotate(Vector3.up * -90f);
			
			Rigidbody tmpRb = tmpBullet.GetComponent<Rigidbody>();
			tmpRb.AddForce(transform.forward * bulletSpeed * -1);

			tmpBullet.GetComponent<BulletController>().bulletDamage = dmgPerShot;

			Destroy(tmpBullet, 2.5f);

			timer = 0f;
			return true;
		}

		return false;
	}

}
