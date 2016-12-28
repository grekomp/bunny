using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

	public float attackDmg = 10f;
	public float attackRate = 1f;

	public GameObject player;

	float timer;
	bool playerInRange = false;

	private void FixedUpdate()
	{
		timer += Time.deltaTime;

		if (playerInRange)
		{
			if (timer >= 1 / attackRate)
			{
				timer = 0;

				//Debug.Log("Attack player");

				PlayerController playerController = player.GetComponent<PlayerController>();
				playerController.GetHit(attackDmg, gameObject);
			}
		}

	}

	private void OnTriggerEnter(Collider other)
	{
		//Debug.Log("Triggered");

		if (other.CompareTag("Player"))
		{
			//Debug.Log("Player in range");
			playerInRange = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//Debug.Log("Player out of range");
			playerInRange = false;
		}
	}
}
