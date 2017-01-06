using UnityEngine;
using System.Collections;

public class WeaponPickup : MonoBehaviour {
	public GameObject weapon;
	public bool active = false;

	public float lifeTime = 40f;

	float timer;
	Material mat;
	float transitionSpeed = 0.1f;

	private void Awake()
	{
		mat = GetComponentInChildren<Renderer>().material;

		transform.localScale = new Vector3(0, 0, 0);
		timer = lifeTime;
		active = false;
	}

	private void Update()
	{
		timer -= Time.deltaTime;

		if (timer <= lifeTime - 1f) active = true;

		if (timer <= 0)
		{
			Destroy(gameObject);
		}

		if (timer <= 10)
		{
			mat.color = Color.Lerp(mat.color, new Color(1, 0, 0), Time.deltaTime * transitionSpeed);
			mat.SetColor("_EmissionColor", Color.Lerp(mat.GetColor("_EmissionColor"), new Color(0.5f,0,0), Time.deltaTime * transitionSpeed));
		}
		transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (active && other.CompareTag("Player"))
		{
			other.GetComponent<PlayerController>().ChangeGun(weapon);
			Destroy(gameObject);
		}
	}
}
