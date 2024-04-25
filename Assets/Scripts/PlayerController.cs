using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    TouchingDirections touchingDirections;
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

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * tempWalkSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
    }

    public float tempWalkSpeed
    {
        get
        {
            if (CanMove)
            {
                if (!touchingDirections.IsOnWall && isMoving)
                {
                    return walkSpeed;
                }
                else
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    [SerializeField]
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }
    public bool IsAlive
    {
        get
        {
            return animator.GetBool("isAlive");
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        isMoving = moveInput != Vector2.zero;
        if (moveInput.x > 0 && IsAlive)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput.x < 0 && IsAlive)
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
