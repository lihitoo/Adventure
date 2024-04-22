using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    TouchingDirections touchingDirections;
    // Start is called before the first frame update
    Vector2 moveInput;
    private bool _IsMoving = false;
    public bool isMoving
    {
        get
        {
            return _IsMoving;
        }
        private set
        {
            _IsMoving = value;
            animator.SetBool("isMoving", value);

        }
    }


    Rigidbody2D rb;
    Animator animator;
    public float walkSpeed = 5f;
    public float jumpImpulse = 10f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * tempWalkSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }
    public float tempWalkSpeed
    {
        get
        {
            if(CanMove)
            {
                if (!touchingDirections.IsOnWall && isMoving)
                {
                    //Debug.Log("OK");
                    return walkSpeed;
                    
                }
                else return 0f;
            }
            else return 0f;
        }
    }
    public bool CanMove { 
        get 
        {
            return animator.GetBool("canMove");
        } 
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isMoving = moveInput != Vector2.zero;
        if (moveInput.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("jump");
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger("attack");
        }
    }
}
