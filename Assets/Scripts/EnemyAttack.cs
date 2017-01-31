using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float attackDmg = 10f;
	public float attackRate = 1f;

	public GameObject player;

	float timer;
	bool playerInRange = false;
	public bool isAlive = true;

	private void Start()
	{
		player = GameManager.instance.player;
	}

	private void FixedUpdate()
	{
		timer += Time.deltaTime;

		if (playerInRange && isAlive)
		{
			if (timer >= 1 / attackRate)
			{
				timer = 0;

				PlayerController playerController = player.GetComponent<PlayerController>();
				playerController.GetHit(attackDmg, gameObject);
			}
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInRange = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerInRange = false;
		}
	}
}
