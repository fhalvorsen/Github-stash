using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CharacterMotor : MonoBehaviour {

    NavMeshAgent agent;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator> ();
        agent = GetComponent<NavMeshAgent>();
	}

	void Update()
	{
		if(anim)
		anim.SetFloat ("Speed", agent.velocity.magnitude);
	}
	
	public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }
}
