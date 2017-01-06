using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform target;
	NavMeshAgent agent;

	void Start () {
		target = GameObject.FindGameObjectWithTag("Player").transform;

		agent = GetComponent<NavMeshAgent>();
	}
	
	void FixedUpdate () {
		agent.destination = target.position;
	}
}
