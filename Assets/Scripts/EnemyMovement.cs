using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;
	Rigidbody rb;
	float timer;

	void Start() {
		target = GameObject.FindGameObjectWithTag("Player").transform;
		timer = 0f;

		rb = GetComponent<Rigidbody>();
		agent = GetComponent<NavMeshAgent>();
	}
	
	void FixedUpdate() {
		timer -= Time.deltaTime;

		if (timer <= 0)
		{
			stopKnockback();
			agent.destination = target.position;
		}
	}

	public void knockback(Vector3 sourcePosition, float distance, bool fromSource = true)
	{
		agent.enabled = false;
		rb.isKinematic = false;

		float strength;
		Vector3 direction = (transform.position - sourcePosition);
		direction.y = 0;
		direction.Normalize();

		if (fromSource)
		{
			strength = distance - Vector3.Distance(transform.position, sourcePosition);
			strength = strength >= 0f ? strength : 0f;
		} else
		{
			strength = distance;
		}
		timer = strength / 2f;

		strength = strength * 17f;

		rb.AddForce(direction * strength, ForceMode.VelocityChange);
	}

	public void stopKnockback()
	{
		rb.isKinematic = true;
		agent.enabled = true;
	}
}
