using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    Transform player;

    Vector3 playerPosition;
    public float scalarProjection;
    public float radius = 10f;

    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        playerPosition = player.transform.position;

    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 1);

        float distance = Vector3.Distance(playerPosition, transform.position);

        Gizmos.color = distance > radius ? Color.red : Color.blue;
        Gizmos.DrawWireSphere(transform.position, radius);

    }
}
