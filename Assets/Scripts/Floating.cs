using UnityEngine;
using System.Collections;

public class Floating : MonoBehaviour {

	public float floatSpeed;
	public float amplitude;

	public float rotationSpeed;

	float y0;

	private void Start()
	{
		y0 = transform.position.y;
	}

	void Update () {
		Vector3 newPosition = new Vector3(transform.position.x, y0 + amplitude * Mathf.Sin(Time.time * floatSpeed), transform.position.z);
		transform.position = newPosition;

		transform.Rotate(Vector3.up * rotationSpeed);
	}
}
