using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {

	public Transform target;

	NavMeshAgent agent;

	void Start () {
		agent = GetComponent<NavMeshAgent>();
	}
	
	void FixedUpdate () {
		agent.destination = target.position;
	}
}
