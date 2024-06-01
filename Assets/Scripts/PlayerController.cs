using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    protected TouchingDirections touchingDirections;
    protected Vector2 moveInput;
    protected Damageable damageable;
    protected bool _IsMoving = false;
    [SerializeField] protected GameObject bulletPrefabs;
    [SerializeField] protected Transform arrowSpawnPoint;
    protected Rigidbody2D rb;
    protected Animator animator;

    [SerializeField] protected float walkSpeed = 5f;
    [SerializeField] protected float jumpImpulse = 10f;

    public bool isMoving
    {
        get
        {
            return _IsMoving;
        }
        protected set
        {
            _IsMoving = value;
            animator.SetBool("isMoving", value);
        }
    }

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    protected virtual void Update()
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
        if (rb.velocity.y < -21)
        {
            Destroy(gameObject);
            UIManager.Instance.PauseGame();
        }
    }

    protected float tempWalkSpeed
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
        if (moveInput.x < 0 && IsAlive && CanMove)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = -1 * Mathf.Abs(newScale.x);
            transform.localScale = newScale;
        }
        else if (moveInput.x > 0 && IsAlive && CanMove)
        {
            Vector3 newScale = transform.localScale;
            newScale.x = Mathf.Abs(newScale.x);
            transform.localScale = newScale;
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
        }
    }

    public virtual void OnAttack2(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("attack2");
        }
    }

    public virtual void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("attack3");
        }
    }

    public void OnAttack4(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger("attack4");
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}
