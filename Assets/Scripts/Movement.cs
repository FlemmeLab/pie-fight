
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed              = 10.0f;
    private float movementMultiplier    = 10.0f;
    public float groundMultiplier       = 10.0f;
    public float airMultiplierUp        = 01.0f;
    public float airMultiplierDown      = 00.5f;

    [Header("Drag")]
    public float rbDrag             = 06.0f;
    public float groundDrag         = 06.0f;
    public float airDrag            = 10.0f;

    [Header("Keybinds")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    float horizontalMovement;
    float verticalMovement;

    [Header("Jumping")]
    public float jumpForce          = 08.0f;

    float playerHeight = 2.0f;
    private bool isGrounded         = false;

    Vector3 moveDirection;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight / 2 + 0.1f);
        MyInput();
        ControlDrag();

        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }
    }

    void MyInput()
    {
        horizontalMovement  = Input.GetAxisRaw("Horizontal");
        verticalMovement    = Input.GetAxisRaw("Vertical");

        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }

    void ControlDrag()
    {
        
        if (isGrounded)
        {
            rbDrag = groundDrag;
            movementMultiplier = groundMultiplier;
        }
        else
        {
            rbDrag = airDrag;

            if (rb.velocity.y < 0)
            {
                movementMultiplier = airMultiplierUp;
                Physics.gravity = new Vector3(0, -20.0F, 0);
            }
            else
            {
                movementMultiplier = airMultiplierDown;
                Physics.gravity = new Vector3(0, -09.8F, 0);
            }
            
        }

        rb.drag = rbDrag;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        rb.AddForce(moveDirection.normalized * moveSpeed * movementMultiplier, ForceMode.Acceleration);
    }
    void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

}