using UnityEngine;

public class RadialTrigger : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float radius = 10f;
    [SerializeField] Color insideRadiusColor = Color.blue;
    [SerializeField] Color outsideRadiusColor = Color.red;
    Vector3 playerPosition;

    void Start()
    {
        if (!player)
            player = GameObject.Find("Player").transform;
    }

    void Update()
    {
        playerPosition = player.transform.position;
    }

    void OnDrawGizmosSelected()
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        Gizmos.color = distance > radius ? outsideRadiusColor : insideRadiusColor;

        Gizmos.DrawWireSphere(transform.position, radius);
        Gizmos.DrawSphere(transform.position, 0.1f);
    }
}
