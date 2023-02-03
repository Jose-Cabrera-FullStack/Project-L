using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 600f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    // [SerializeField] float forceMagnitude;
    float pushForce = 2f;
    Rigidbody attachedRigidbody;


    CharacterController characterController;
    Animator animator;
    Vector3 moveVelocity;
    float walkingSpeed = 1f;
    float runningSpeed => walkingSpeed * 2;
    Vector3 cameraRight;
    Vector3 cameraForward;

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

        if (!(CameraManager.selectedCamera is null))
        {
            cameraRight = CameraManager.selectedCamera.transform.right;
            cameraForward = CameraManager.selectedCamera.transform.forward;
        }

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
            moveVelocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                moveVelocity.y = jumpSpeed;
            }
        }
        switchCamera();

        moveVelocity.y += gravity * Time.deltaTime;

        Vector3 cameraInputHorizontal = hInput * cameraRight;
        Vector3 cameraInputVertical = vInput * cameraForward;
        Vector3 cameraInput = speed * (cameraInputHorizontal + cameraInputVertical);

        moveVelocity.x = cameraInput.x;
        moveVelocity.z = cameraInput.z;

        characterController.Move(moveVelocity * Time.deltaTime);
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