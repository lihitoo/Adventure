using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    TouchingDirections touchingDirections;
    Vector2 moveInput;
    Damageable damageable;
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
        damageable = GetComponent<Damageable>();
    }
    void Update()
    {
        if (!CanMove) rb.velocity = Vector2.zero;
        if (!damageable.IsHit && CanMove)
            rb.velocity = new Vector2(moveInput.x * tempWalkSpeed, rb.velocity.y);
        animator.SetFloat("yVelocity", rb.velocity.y);
        if (Input.GetKey(KeyCode.A) && CanMove)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -1 * Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
        if (Input.GetKey(KeyCode.D) && CanMove)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
        if(rb.velocity.y < -21)
        {
            Destroy(gameObject);
            UIManager.Instance.PauseGame();
        }
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
        //if(context.started)
        {
            moveInput = context.ReadValue<Vector2>();

            isMoving = moveInput != Vector2.zero;
            if (moveInput.x < 0 && IsAlive && CanMove)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = -1 * Mathf.Abs(newScale.x);
                transform.localScale = newScale;
            //    Debug.Log("left");
              //  animator.SetBool("canMove", true);
            }
            else if (moveInput.x > 0 && IsAlive && CanMove)
            {
                Vector3 newScale = transform.localScale;
                newScale.x = Mathf.Abs(newScale.x);
                transform.localScale = newScale;
             //   Debug.Log("right");
             //   animator.SetBool("canMove", false);
            }
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
    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger("attack1");
            //isAttacking = true;
        }
        Debug.Log(1);
    }
    public void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger("attack2");
            //isAttacking = true;
        }
        Debug.Log(2);
    }
    public void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger("attack3");
            //isAttacking = true;
        }
        Debug.Log(3);
    }
    public void OnAttack4(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger("attack4");
            //isAttacking = true;
        }
        Debug.Log(4);
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}
