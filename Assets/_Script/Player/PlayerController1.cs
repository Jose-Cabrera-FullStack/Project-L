using UnityEngine;

public class PlayerController1 : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotationSpeed;
    [SerializeField] int jumpSpeed;
    [SerializeField] float gravity = -20f;

    CharacterController characterController;
    Vector3 moveVelocity;
    Vector3 turnVelocity;

    [SerializeField]
    float forceMagnitude;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = 15f;
        rotationSpeed = 600f;
        jumpSpeed = 5;
    }

    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        if (characterController.isGrounded)
        {
            moveVelocity = transform.forward * speed * vInput;
            turnVelocity = transform.up * rotationSpeed * hInput;

            if (Input.GetButtonDown("Jump"))
            {
                moveVelocity.y = jumpSpeed;
            }
        }
        // switchCamera();

        moveVelocity.y += gravity * Time.deltaTime;
        characterController.Move(moveVelocity * Time.deltaTime);
        transform.Rotate(turnVelocity * Time.deltaTime);
    }

    // void switchCamera()
    // {
    //     if (Input.GetKeyDown(KeyCode.Q))
    //     {
    //         CameraManager.NextCamera();
    //     }

    //     if (Input.GetKeyDown(KeyCode.E))
    //     {
    //         CameraManager.PrevCamera();
    //     }
    // }

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