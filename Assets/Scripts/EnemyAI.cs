using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target; 
    private NavMeshAgent agent; 
    public string phase = "searchGold";

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
void Update()
{
    PlayerScript playerScript = gameObject.GetComponent<PlayerScript>();
    GameObject closestGoldObject = playerScript.closestGoldObject;

    NavMeshHit hit;
    float maxDistance = 100f;
    int mask = NavMesh.AllAreas;

    if (phase == "searchGold")
    {
        if (NavMesh.SamplePosition(closestGoldObject.transform.position, out hit, maxDistance, mask))
        {
            agent.SetDestination(hit.position);
        }
    }
    else if (phase == "walk")
    {
        // If the agent has reached its destination or doesn't have a path yet
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 1000;
            Debug.Log("New direction : " + randomDirection);
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, 1000, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
            Debug.Log("New destination : " + hit.position);
        }
    }

    transform.position += agent.desiredVelocity * Time.deltaTime;
}
}