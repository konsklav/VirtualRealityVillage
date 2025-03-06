using UnityEngine;
using UnityEngine.AI;

public class AnimalMovement : MonoBehaviour
{
    public float moveRadius = 10f;  // Radius in which the animal can move
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        InvokeRepeating(nameof(MoveToRandomPosition), 0f, 5f);  // Move every 5 seconds
    }

    void MoveToRandomPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * moveRadius;
        randomDirection += transform.position;

        if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, moveRadius, 1))
        {
            agent.SetDestination(hit.position);
        }
    }
}
