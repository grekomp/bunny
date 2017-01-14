using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	public float speed = 10f;
	Rigidbody rb;

	void Start() {
		target = GameManager.instance.player.transform;

		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		moveTowards(target.position);
	}

	public void knockback(Vector3 sourcePosition, float distance, bool fromSource = true)
	{
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

		rb.AddForce(direction * strength);
	}

	public void moveTowards(Vector3 target)
	{
		Vector3 delta = (target - transform.position).normalized;
		rb.MovePosition(transform.position + delta * speed * Time.fixedDeltaTime);
	}
}
