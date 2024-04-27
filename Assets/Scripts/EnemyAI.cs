// This script is what controls the enemy boats AI. 
// First, the enemies will start walking ranomly in a range of 200 units.
// Then, after a random time between 1 and 5 minutes, they will start searching for the closest gold (given by the PlayerScript).
// After the gold has been mined, they will go back to walking randomly etc...

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
public Transform target; 
private NavMeshAgent agent; 
public string phase = "walk";

void Start()
{
    agent = GetComponent<NavMeshAgent>();
    StartCoroutine(SwitchToSearchGold());
}

IEnumerator SwitchToSearchGold()
{
    Debug.Log("Wait time");
    yield return new WaitForSeconds(Random.Range(60, 300)); // wait for a random time between 1 and 5 minutes
    Debug.Log("Wait time over");
    phase = "searchGold"; // and then start searching gold
}

void Update()
{
    PlayerScript playerScript = gameObject.GetComponent<PlayerScript>();
    GameObject closestGoldObject = playerScript.closestGoldObject;

    NavMeshHit hit;
    float maxDistance = 100f;
    int mask = NavMesh.AllAreas; // To search gold even if it's not exactly on the navmesh

    if (phase == "searchGold")
    {
        if (closestGoldObject == null) // gold has been mined
        {
            phase = "walk";
            StartCoroutine(SwitchToSearchGold());
        }
        else if (NavMesh.SamplePosition(closestGoldObject.transform.position, out hit, maxDistance, mask))
        {
            agent.SetDestination(hit.position);
        }
    }
    else if (phase == "walk")
    {
        // If the agent has reached its destination or doesn't have a path yet
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 randomDirection = Random.insideUnitSphere * 200;
            Debug.Log("New direction : " + randomDirection);
            randomDirection += transform.position;
            NavMesh.SamplePosition(randomDirection, out hit, 200, NavMesh.AllAreas);
            agent.SetDestination(hit.position);
            Debug.Log("New destination : " + hit.position);
        }
    }

    transform.position += agent.desiredVelocity * Time.deltaTime;
}
}