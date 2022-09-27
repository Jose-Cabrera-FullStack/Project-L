using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 6f;

    public Camera mainCamera;
    Vector3 camForward;
    Vector3 camRight;
    public float gravity = 9.8f;

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        direction.y = -gravity * Time.deltaTime;

        camDirection();

        if (direction.magnitude >= 0.1f)
        {
            Debug.Log(camRight);
            Vector3 moveController = direction.x * camRight + direction.z * camForward;
            controller.transform.LookAt(controller.transform.position + moveController);
            controller.Move(direction * speed * Time.deltaTime);
        }
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

}
