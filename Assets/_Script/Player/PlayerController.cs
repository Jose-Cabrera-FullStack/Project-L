using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed = 600f;
    [SerializeField] int jumpSpeed = 5;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float forceMagnitude;

    CharacterController characterController;
    Animator animator;
    Vector3 moveVelocity;
    float walkingSpeed = 1f;
    float runningSpeed => walkingSpeed * 2;
    Vector3 cameraRight;
    Vector3 cameraForward;

    [SerializeField]
    float forceMagnitude;
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
        
        if (!(CameraManager.selectedCamera is null)){
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

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        forceMagnitude = 10f;
        if (rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}