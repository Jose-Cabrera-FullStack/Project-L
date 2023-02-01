using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 600f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    // [SerializeField] float forceMagnitude;
    float pushForce = 2f;
    bool isPushing = false;
    Rigidbody attachedRigidbody;


    CharacterController characterController;
    Animator animator;
    Vector3 moveVelocity;
    Vector3 turnVelocity;
    float walkingSpeed = 1f;
    float runningSpeed => walkingSpeed * 2;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        speed = walkingSpeed;
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        if (hInput != 0 || vInput != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = runningSpeed;
            }
            else
            {
                speed = walkingSpeed;
            }

            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        HandlePushing();

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * vInput;
            turnVelocity = transform.up * rotationSpeed * hInput;

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                moveVelocity.y = jumpSpeed;
            }
        }
        switchCamera();

        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }

    void switchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            CameraManager.NextCamera();
        }
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
        if (hit.collider.attachedRigidbody != null && Input.GetKeyDown(KeyCode.E))
        {
            if (!isPushing)
            {
                Debug.Log($"hit.collider.attachedRigidbody:{hit.collider.attachedRigidbody}");
                // Start pushing or pulling
                attachedRigidbody = hit.collider.attachedRigidbody;
                Vector3 forceDirection = attachedRigidbody.transform.position - transform.position;
                attachedRigidbody.AddForce(forceDirection * pushForce, ForceMode.Force);
                isPushing = true;
            }
            else
            {
                // Stop pushing or pulling
                attachedRigidbody = null;
                isPushing = false;
            }
        }
    }
}