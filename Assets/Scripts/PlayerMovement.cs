using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 3f;

	Vector3 movement;
	Rigidbody rb;
	float camRayLength = 100f;
	int aimTargetMask;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		aimTargetMask = LayerMask.GetMask("AimTarget");
	}
	
	void FixedUpdate () {
		float h = Input.GetAxisRaw("Horizontal");
		float v = Input.GetAxisRaw("Vertical");

		Move(h, v);
		Rotate();
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		movement = movement.normalized * speed * Time.deltaTime;

		rb.MovePosition(transform.position + movement);
	}

	void Rotate()
	{
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit rayHit;

		if(Physics.Raycast(camRay, out rayHit, camRayLength, aimTargetMask))
		{
			Vector3 offset = transform.position - rayHit.point;
			offset.y = 0f;
			Quaternion newRotation = Quaternion.LookRotation(offset);
			rb.MoveRotation(newRotation);
		}

		GameManager.instance.cursorLocation = rayHit.point;
	}
}
