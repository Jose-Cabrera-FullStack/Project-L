using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalMove;
    public float verticalMove;
    public CharacterController player;
    public Vector3 playerInput;
    public float playerSpeed;

    void Start()
    {
        player = GetComponent<CharacterController>();
        playerSpeed = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        playerInput = Vector3.ClampMagnitude(new Vector3(horizontalMove, 0, verticalMove), 1);

        player.Move(playerInput * playerSpeed * Time.deltaTime);
    }
}
