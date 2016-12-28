using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform target;
	public float smoothing = 5f;

	Vector3 offset;

	private void Awake()
	{
		offset = transform.position - target.position;
	}

	private void FixedUpdate()
	{
		Vector3 targetCameraPosition = target.position + offset;

		transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing * Time.deltaTime);
	}
}
