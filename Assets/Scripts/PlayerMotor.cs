using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMotor : MonoBehaviour
{
    Transform targer;
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();   
    }
    private void Update()
    {
        if(targer != null)
        {
            agent.SetDestination(targer.position);
            FaceTarger();
        }
    }

    public void MoveToPoint (Vector3 point)
    {
        agent.SetDestination(point);
    }

    // Move Character follow object when object move
    public void FollowTarger (Interactable newTarger)
    {
        // avoid Character move into object
        agent.stoppingDistance = newTarger.radius * .8f;
        // avoid Player not change rotation inside object
        agent.updateRotation = false;

        targer = newTarger.interactionTransform;
    }

    public void StopFollowTarger()
    {
        agent.stoppingDistance = 0f;

        agent.updateRotation = true;

        targer = null;
    }

    private void FaceTarger()
    {
        Vector3 direction = (targer.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation , Time.deltaTime * 5f);   
    }
}
