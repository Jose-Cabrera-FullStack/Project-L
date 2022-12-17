using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float deathDistance = 5f;
    [SerializeField]
    float distanceAway;
    // public Transform thisObject;
    Transform target;
    NavMeshAgent navComponent;

    // TODO: Add corner trigger to follow the player when it's inside the polygon / Create a Path routine to enemy instance.

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navComponent = transform.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = Vector3.Distance(target.position, transform.position);

        if (target)
        {
            navComponent.SetDestination(target.position);
        }
        else
        {
            if (target == null)
            {
                target = transform.gameObject.GetComponent<Transform>();
            }
            else
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
            }
        }

        // Logic to kill o subtract life
        if (dist <= deathDistance)
        {
            navComponent.isStopped = true;
        }
        else
        {
            navComponent.isStopped = false;
        }
    }
}
