using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMoveBehaviour : MonoBehaviour
{
    public Transform currentTarget;
    public bool xRayVision;
    public float visionDistance = 16f;
    public Vector3 visionOffset = Vector3.up;

    private NavMeshAgent agent;
    private bool targetVisible;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (!xRayVision)
        {
            if (currentTarget == null) return;
            var direction = currentTarget.position - transform.position;
            if (Physics.Raycast(transform.position + visionOffset, direction, out var hit, visionDistance))
            {
                Debug.Log(hit.transform.name);
                if (hit.transform == currentTarget.transform)
                {
                    targetVisible = true;
                    Debug.Log(targetVisible);
                    return;
                }
            }
            targetVisible = false;
            Debug.Log(targetVisible);
            return;
        }
    }

    public void SetTarget(GameObject target)
    {
        currentTarget = target.transform;
    }

    public void Untarget()
    {
        currentTarget = null;
    }

    public void MoveTowards()
    {
        if (currentTarget == null) return;
        if (!targetVisible) return;

        agent.SetDestination(currentTarget.position);
    }

    public void MoveTowardsDumb()
    {
        if (currentTarget == null) return;
        if (!targetVisible) return;

        if (agent.hasPath) agent.ResetPath();

        if (Vector3.Distance(transform.position, currentTarget.position) < agent.stoppingDistance) return;

        var direction = currentTarget.position - transform.position;
        direction = direction.normalized * agent.speed;

        agent.Move(direction * Time.deltaTime);
    }

    public void MoveAwayFrom()
    {
        if (currentTarget == null) return;
        if (!targetVisible) return;
        if (agent.hasPath) agent.ResetPath();

        var direction = transform.position - currentTarget.transform.position;
        direction = direction.normalized * agent.speed;

        agent.Move(direction * Time.deltaTime);
    }

    public void FollowPath(List<Transform> targets)
    {

    }

    public void FollowPath(NavMeshPath path)
    {

    }

    public void RandomPatrol(List<Transform> targets)
    {

    }

    private void OnDrawGizmosSelected()
    {
        if (xRayVision) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + visionOffset, .1f);
        Gizmos.DrawWireSphere(transform.position + visionOffset, visionDistance);
    }
}
