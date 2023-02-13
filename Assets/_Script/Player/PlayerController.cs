using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float walkingSpeed = 1f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float pushForce = 2f;

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
        ClimbLedder();
    }

    private void HandleMovement()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        if (hInput == 0 && vInput == 0)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkingSpeed * 2 : walkingSpeed;
        animator.SetBool("IsWalking", true);

        Vector3 cameraInputHorizontal = hInput * cameraRight;
        Vector3 cameraInputVertical = vInput * cameraForward;
        Vector3 cameraInput = speed * (cameraInputHorizontal + cameraInputVertical);

        moveVelocity.x = cameraInput.x;
        moveVelocity.z = cameraInput.z;

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
    }


    private void GetCameraDirections()
    {
        if (CameraManager.selectedCamera == null)
            return;

        cameraRight = CameraManager.selectedCamera.transform.right;
        cameraForward = CameraManager.selectedCamera.transform.forward;
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

    void ClimbLedder()
    {
        // TODO: Y is rotationPlayer
        Vector3 targetDirection = Quaternion.Euler(0, 0, 0) * Vector3.forward;
        float avoidFloorDistance = 0.1f;
        float ladderGrabDistance = 0.4f;
        if (Physics.Raycast(
            transform.position + Vector3.up * avoidFloorDistance,
            targetDirection,
            out RaycastHit raycastHit,
            ladderGrabDistance))
        {
            Debug.Log($"{raycastHit.transform}");
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.forward * 1.2f);
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