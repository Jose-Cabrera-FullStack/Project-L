/* using System.Collections; */
/* using System.Collections.Generic; */
/* using System; */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Refs
    [SerializeField]
    public RouteCreator creator;

    [SerializeField]
    public CharacterController controller;

    [SerializeField]
    public float speed;
    public float t = 0;

    private Vector3 direction;
    private Vector3 velocity;
    Vector3 position => creator.route.GetPosition(t);

    void Start()
    {
        transform.position = creator.route[0];
    }

    // Update is called once per frame
    void Update()
    {
        MovementHandler();
    }

    private void MovementHandler()
    {
        var input = Input.GetAxis("Horizontal") * speed;
        t = Mathf.Clamp(t + input, 0, 1);

        var sign = Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        direction = creator.route.GetTanget(t) * sign;

        direction.Normalize();

        Debug.DrawLine(position, position + direction, Color.blue);
        Debug.DrawLine(
            position,
            position + Vector3.Cross(-direction, Vector3.up * sign),
            Color.green
        );

        if (input != 0)
        {
            Debug.Log("Move");
            /* controller.Move(position + new Vector3(direction.x, 0f, direction.z) *
             */
            /*                                speed * Time.deltaTime); */
        }
    }
}
