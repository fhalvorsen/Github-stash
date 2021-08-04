using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MovingAndInteracting : MonoBehaviour
{
    public Interactable interactTarget;
    [HideInInspector]
    public Camera warehouseCamera;
    [HideInInspector]
    public Character character;
    [HideInInspector]
    public NavMeshAgent agent;
    [HideInInspector]
	public Animator animator;

	void Awake () 
    {
        warehouseCamera = Camera.main;
        animator = GetComponentInChildren<Animator> ();
        agent = GetComponent<NavMeshAgent>();
	}
	
	void Update () 
    {
        if(animator)
		animator.SetFloat ("Speed", agent.velocity.magnitude);
       
    }

    public void SetInteractionTarget(Interactable newInterract)
    {
        if (newInterract != interactTarget)
        {
            if (interactTarget != null)
                interactTarget.OnDefocused(character);

            interactTarget = newInterract;
            interactTarget.OnDefocused(character);
            MoveToPoint(newInterract.interactionTransform.position);
        }

        newInterract.OnFocused(character);
    }

    public void RemoveInteractionTarget()
    {
        if (interactTarget != null)
            interactTarget.OnDefocused(character);

        interactTarget = null;
    }
  
    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }
}
