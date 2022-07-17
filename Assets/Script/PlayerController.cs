/* using System.Collections; */
/* using System.Collections.Generic; */
/* using System; */
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public RouteCreator creator;
    [SerializeField]
    public float gravity = -10;
    public float speed;
    private Vector3 direction;

    [SerializeField]
    public CharacterController controller;
    [SerializeField]
    public float t = 0;

    private Vector3 velocity;
    public float jumpForce = 10;

    void Start()
    {
        transform.position = creator.route[0];
    }

    // Update is called once per frame
    void Update()
    {
      HadlerMovement();
    }

    private void HadlerMovement()
    {
          var input = Input.GetAxis("Horizontal") * speed;
        t = Mathf.Clamp(t + input, 0, 1);

        direction = creator.route.getTanget(t) * Mathf.RoundToInt(Input.GetAxis("Horizontal"));
        /* direction.Normalize(); */
        transform.forward = direction.normalized;

        var position = creator.route.getPosition(t);

        Debug.DrawLine(position, position + direction, Color.blue);
        Debug.DrawLine(position, position + Vector3.Cross(-direction, Vector3.up).normalized, Color.green);

        if (input != 0)
        {
            Debug.Log("Move");
            /* controller.Move(new Vector3(direction.x, transform.position.y, direction.z) * Time.deltaTime); */
            /* controller.Move(position + new Vector3(direction.x, transform.position.y, direction.z)); */
            /* transform.position = Vector3.MoveTowards(transform.position, new Vector3(position.x, transform.position.y, position.z), 0.1f); */
        }

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            Debug.Log("Jump");
            velocity.y = jumpForce;
        }

        velocity.y += 3 * gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        /* controller.Move(position + new Vector3(direction.x, transform.position.y, direction.z)); */
        transform.position = new Vector3(position.x, transform.position.y, position.z);
    }
}
