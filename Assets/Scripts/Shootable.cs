using System;
using UnityEngine;

public class Shootable : MonoBehaviour 
{
    [SerializeField] private float speed;
    private Animator animator;
    private Rigidbody2D rb;
    private void Awake()
    {
        
        
    }

    public void Shoot(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        //animator.GetComponent<Animator>();
        rb.velocity = direction * speed;
    }
    

    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm nếu cần
        if(!other.CompareTag("Player"))
        {
            //Destroy(gameObject);
            //animator.GetComponent<Animator>();
            //animator.SetTrigger("hit");
            //gameObject.SetActive(false);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Invoke("DestroyObject",0.3f);
        }
        
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}
/*
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
    private Shootable shootable;
    [SerializeField] private GameObject bulletPrefabs;
    [SerializeField] private Transform arrowSpawnPoint;
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
            if (gameObject.name == "LeafPlayer")
            {
                Invoke("ShootArrow2",0.5f);
            }
        }
        Debug.Log(2);
    }
    public void OnAttack3(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove)

        {
            animator.SetTrigger("attack3");
            //isAttacking = true;
            if (gameObject.name == "LeafPlayer")
            {
                Invoke("ShootArrow3",0.5f);
            }
        }
        Debug.Log(3);
    }

    private void ShootArrow3()
    {
        
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

    void ShootArrow2()
    {
        
        Vector2 direction = transform.localScale.x >= 0 ? Vector2.right : Vector2.left;
        GameObject bullet = Instantiate(bulletPrefabs, arrowSpawnPoint.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(transform.localScale.x,bullet.transform.localScale.y,bullet.transform.localScale.z);
        bullet.GetComponent<Shootable>().Shoot(direction); // Sử dụng component Shootable để bắn mũi tên
    }
        
    
    
}*/