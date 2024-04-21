using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentTester : MonoBehaviour
{
    public Transform target; 
    private NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component not found on " + gameObject.name);
        }
        else
        {
            // Disable the NavMeshAgent's control over the object's position
            agent.updatePosition = false;
        }
    }

    void Update()
    {
        if (target != null && agent != null)
        {
            agent.SetDestination(target.position); 

            // Apply the NavMeshAgent's calculated movement manually
            transform.position += agent.desiredVelocity * Time.deltaTime;
        }
    }
}