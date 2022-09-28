using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    Vector3 playerVelocity;
    public float speed = 6f;
    public float JumpForce = 100.0f;
    public float gravity = 9.8f;

    public Camera mainCamera;
    Vector3 camForward;
    Vector3 camRight;

    void Update()
    {
        movement();
    }

    void movement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        direction.y += -gravity * Time.deltaTime;

        if(Input.GetKeyDown("space")){
            // TODO: Needs to be improved
            direction.y += Mathf.Sqrt(Math.Abs(JumpForce * -3.0f * gravity * Time.deltaTime));
        }

        camDirection();

        if (direction.magnitude >= 0.1f)
        {
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
