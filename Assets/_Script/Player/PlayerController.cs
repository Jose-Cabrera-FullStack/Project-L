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
    float runningSpeed => walkingSpeed * 2;
    [SerializeField]
    Vector3 initialPosition;
    float targetAngle;
    float angle;

    void Awake()
    {
        initialPosition = this.transform.position;
        GameManager.OnResetGame += handleResetGame;
    }

    void OnDestroy()
    {
        GameManager.OnResetGame -= handleResetGame;
    }

    private void handleResetGame()
    {
        characterController.Move(initialPosition - this.transform.position);
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

        Vector3 forward = CameraManager.selectedCamera.transform.forward * playerVerticalInput;
        Vector3 right = CameraManager.selectedCamera.transform.right * playerHorizontalInput;

        Vector3 movement = forward + right;

        if (playerHorizontalInput == 0 && playerVerticalInput == 0)
        {
            animator.SetBool("IsWalking", false);
            return;
        }

        float speed = Input.GetKey(KeyCode.LeftShift) ? walkingSpeed * 2 : walkingSpeed;
        animator.SetBool("IsWalking", true);

        if (characterController.isGrounded)
        {
            moveVelocity.y = 0;
            /* moveVelocity = transform.forward * speed * playerVerticalInput; */
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

        targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
        angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 1f, rotationSpeed);
        movement.y += moveVelocity.y;

        characterController.Move(movement * speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);
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
