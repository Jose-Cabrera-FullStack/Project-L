using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 direction;

    public float speed = 8;
    public float jumpForce = 10;
    public float gravity = -20;

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        direction.x = hInput * speed;

        direction.y += gravity * Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            direction.y = jumpForce;
        }
        controller.Move(direction * Time.deltaTime);
    }
}
