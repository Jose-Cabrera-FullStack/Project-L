using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 1f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float pushForce = 2f;
    [SerializeField] float rotationSpeed = 2.0f;

    Rigidbody attachedRigidbody;
    CharacterController characterController;
    Animator animator;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    float walkingSpeed = 1f;
    float runningSpeed => walkingSpeed * 2;
    Vector3 cameraRight;
    Vector3 cameraForward;
    [SerializeField]
    Vector3 initialPosition;

    void Awake()
    {
        initialPosition = this.transform.position;
        GameManager.OnResetGame += handleResetGame;
    }

    private void handleResetGame()
    {
        transform.position = Vector3.zero;
    }


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // GetCameraDirections();
        HandleMovement();
        HandlePushing();
        SwitchCamera();
    }

    private void HandleMovement()
    {
        float playerHorizontalInput = Input.GetAxis("Horizontal");
        float playerVerticalInput = Input.GetAxis("Vertical");

        if (playerHorizontalInput == 0 && playerVerticalInput == 0)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkingSpeed * 2 : walkingSpeed;
        animator.SetBool("IsWalking", true);

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * playerVerticalInput;
            turnVelocity = transform.up * rotationSpeed * playerHorizontalInput;
            moveVelocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                //TODO: Fix the jump 
                animator.SetTrigger("Jump");
                moveVelocity.y = jumpSpeed;
            }
        }
        else
        {
            moveVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }




    private void SwitchCamera()
    {
        if (!Input.GetKeyDown(KeyCode.Tab))
            return;

        CameraManager.NextCamera();
    }

    void HandlePushing()
    {
        if (attachedRigidbody == null)
            return;

        Vector3 forceDirection = transform.position - attachedRigidbody.transform.position;
        forceDirection.y = 0;
        forceDirection.Normalize();

        attachedRigidbody.velocity = forceDirection * pushForce;
    }


    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.attachedRigidbody == null)
            return;

        if (!Input.GetKeyDown(KeyCode.E))
            return;

        if (attachedRigidbody == null)
        {
            attachedRigidbody = hit.collider.attachedRigidbody;
            Vector3 forceDirection = attachedRigidbody.transform.position - transform.position;
            attachedRigidbody.AddForce(forceDirection * pushForce, ForceMode.Force);
        }
        else
        {
            attachedRigidbody = null;
        }
    }
}
