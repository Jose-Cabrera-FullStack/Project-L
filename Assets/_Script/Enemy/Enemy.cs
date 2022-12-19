using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    ShapeAreaTrigger trigger;
    Transform target;
    NavMeshAgent navComponent;

    // TODO: Create a Path routine to enemy instance.

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        navComponent = transform.gameObject.GetComponent<NavMeshAgent>();
        trigger = FindObjectOfType<ShapeAreaTrigger>();
    }

    void Update()
    {
        if (trigger.IsContained(target.position))
        {
            navComponent.SetDestination(target.position);

            navComponent.isStopped = false;
        }
        else
        {
            navComponent.isStopped = true;
        }
    }
}
