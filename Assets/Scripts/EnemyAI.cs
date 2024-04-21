using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform target; 
    private NavMeshAgent agent; 

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
    PlayerScript playerScript = gameObject.GetComponent<PlayerScript>();
    GameObject closestGoldObject = playerScript.closestGoldObject;
    agent.SetDestination(closestGoldObject.transform.position); 
    transform.position += agent.desiredVelocity * Time.deltaTime;

    }
}