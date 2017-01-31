using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	bool isAlive = true;
	bool isSinking = false;
	public Transform target;
	public float speed = 10f;
	Rigidbody rb;
	Animator anim;

	void Start() {
		target = GameManager.instance.player.transform;
		anim = transform.GetComponentInChildren<Animator>();
		anim.speed = speed;

		rb = GetComponent<Rigidbody>();
	}
	
	void FixedUpdate() {
		if (isAlive)
		{
			moveTowards(target.position);

		} else
		{
			if(isSinking)
			{
				rb.MovePosition(transform.position + transform.up * Time.deltaTime * -1f);
			}
		}
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
		delta.y = 0;

		rb.MoveRotation(Quaternion.LookRotation(delta));
	}

	public void Die()
	{
		isAlive = false;

		rb.isKinematic = true;
		anim.SetTrigger("Death");
		anim.speed = 2f;

		Invoke("StartSinking", 1f);
	}

	public void StartSinking()
	{
		rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
		isSinking = true;
		Destroy(gameObject, 2f);
	}
}
