using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public float speed = 3f;

	public GameObject modelLegs;
	public GameObject modelTop;

	Vector3 movement;
	Rigidbody rb;
	float camRayLength = 100f;
	int aimTargetMask;
	Quaternion lastLegsRotation;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		aimTargetMask = LayerMask.GetMask("AimTarget");
	}
	
	void FixedUpdate () {
		if (!GameManager.paused && GameManager.instance.playerAlive)
		{
			float h = Input.GetAxisRaw("Horizontal");
			float v = Input.GetAxisRaw("Vertical");

			Move(h, v);
			Rotate();
		}
	}

	void Move(float h, float v)
	{
		movement.Set(h, 0f, v);
		if (movement != Vector3.zero)
		{
			modelLegs.transform.rotation = Quaternion.Lerp(modelLegs.transform.rotation, Quaternion.LookRotation(-movement), Time.deltaTime * 10);
			lastLegsRotation = modelLegs.transform.rotation;
		}
		else
		{
			modelLegs.transform.rotation = Quaternion.Lerp(lastLegsRotation, Quaternion.identity, Time.deltaTime * 10);
		}
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
