using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 1f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float pushForce = 2f;
    [SerializeField] bool isClimbing = false;

    Rigidbody attachedRigidbody;
    CharacterController characterController;
    Animator animator;
    Vector3 moveVelocity;
    Vector3 cameraRight;
    Vector3 cameraForward;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GetCameraDirections();
        HandleMovement();
        HandlePushing();
        SwitchCamera();
    }

    private void HandleMovement()
    {
        // Get Player Input (Original Input System)
        float playerHorizontalInput = Input.GetAxis("Horizontal");
        float playerVerticalInput = Input.GetAxis("Vertical");

        if (playerHorizontalInput == 0 && playerVerticalInput == 0)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkingSpeed * 2 : walkingSpeed;
        animator.SetBool("IsWalking", true);

        Vector3 forwardRelativeHorizontalInput = playerHorizontalInput * cameraRight;
        Vector3 rightRelativeVertticalInput = playerVerticalInput * cameraForward;


        Vector3 cameraRelativeMovement = speed * (forwardRelativeHorizontalInput + rightRelativeVertticalInput);
        Quaternion targetRotation = Quaternion.LookRotation(cameraRelativeMovement);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * speed);

        moveVelocity.x = cameraRelativeMovement.x;
        moveVelocity.z = cameraRelativeMovement.z;

        if (characterController.isGrounded)
        {
            moveVelocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
                moveVelocity.y = jumpSpeed;
            }
        }
        else
        {
            moveVelocity.y += gravity * Time.deltaTime;
        }

        characterController.Move(moveVelocity * Time.deltaTime);
        // transform.Translate(moveVelocity, Space.World);
    }


    private void GetCameraDirections()
    {
        if (CameraManager.selectedCamera == null)
            return;

        cameraRight = CameraManager.selectedCamera.transform.right;
        cameraForward = CameraManager.selectedCamera.transform.forward;

        cameraRight.y = 0;
        cameraForward.y = 0;

        cameraRight = cameraRight.normalized;
        cameraForward = cameraForward.normalized;
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