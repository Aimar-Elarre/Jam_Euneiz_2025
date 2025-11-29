using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    

    [Header("Movement")]
    public float groundSpeed = 1.5f;
    public float airSpeed = 4f;

    [Header("Jump")]
    public float jumpVelocity = 7.7f;
    private bool jumpPressed;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.1f;
    public LayerMask groundLayer;

    [Header("Cameras")]
    public GameObject camera1;
    public GameObject camera2;

    private Rigidbody2D rb;

    // Input System
    private InputSystem_Actions inputActions;
    private Vector2 moveInput;

    private bool isGrounded;
    private bool cameraSwitch = false;

    [SerializeField]
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        inputActions = new InputSystem_Actions();

        // Conectar eventos
        
        inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;

        inputActions.Player.Jump.performed += ctx => jumpPressed = true;
        inputActions.Player.Jump.canceled += ctx => jumpPressed = false;

        inputActions.Player.Sprint.performed += ctx => cameraSwitch = true;
        inputActions.Player.Sprint.canceled += ctx => cameraSwitch = false;
    }

    void OnEnable() => inputActions.Player.Enable();
    void OnDisable() => inputActions.Player.Disable();

    void Update()
    {
        // Ground check usando Raycast
        float rayDistance = groundCheckRadius;
        RaycastHit2D hit = Physics2D.Raycast(
            groundCheck.position,
            Vector2.down,
            rayDistance,
            groundLayer
        );

        isGrounded = hit.collider != null;
    }

    void FixedUpdate()
    {
        // Mobimiento
        float speed = isGrounded ? groundSpeed : airSpeed;

        rb.linearVelocity = new Vector2(moveInput.x * speed, rb.linearVelocity.y);
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
            if (rb.linearVelocityY < 0)
            {
                animator.SetBool("isFalling", true);
            }
            else
            {
                animator.SetBool("isFalling", false);
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.SetBool("isJumping", true);
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
            }
        }

        // Salto
        if (jumpPressed && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpVelocity);
            jumpPressed = false;
        }

        //Cambio De Camara
        camera1.SetActive(!cameraSwitch);
        camera2.SetActive(cameraSwitch);

        //Rotacion de el player
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -1 * 180, 0));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0,0,0));
        }
        
    }

    void OnDrawGizmos()
    {
        if (groundCheck == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(groundCheck.position,
                        groundCheck.position + Vector3.down * groundCheckRadius);
    }
}


