using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

[RequireComponent(typeof(NavMeshAgent))]
public class AIMoveBehaviour : MonoBehaviour
{
    public Transform currentTarget;
    public bool xRayVision;
    public float visionDistance = 16f, patrolWaitTime;
    public Vector3 visionOffset = Vector3.up;

    public List<Transform> patrolPoints;
    public UnityEvent patrolBehaviour;

    private NavMeshAgent agent;
    private bool targetVisible;
    private int currentPatrolPointIndex;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (currentTarget == null) targetVisible = false;

        if(currentTarget == null && !targetVisible)
        {
            agent.SetDestination(patrolPoints[currentPatrolPointIndex].position);
        }
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

    public void NextPatrolPoint()
    {
        currentPatrolPointIndex = (currentPatrolPointIndex + 1) % patrolPoints.Capacity;
    }

    public void RandomPatrolPoint()
    {
        currentPatrolPointIndex = Random.Range(0, patrolPoints.Capacity);
    }

    private IEnumerator Patrol()
    {
        yield return new WaitUntil(PatrolPointReached);

        yield return new WaitForSeconds(patrolWaitTime);

        patrolBehaviour.Invoke();

        StartCoroutine(Patrol());
    }

    private bool PatrolPointReached()
    {
        return Vector3.Distance(transform.position, patrolPoints[currentPatrolPointIndex].position) < agent.stoppingDistance;
    }

    private void OnDrawGizmosSelected()
    {
        if (xRayVision) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + visionOffset, .1f);
        Gizmos.DrawWireSphere(transform.position + visionOffset, visionDistance);
    }
}
