using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    // Start is called before the first frame update
    public DetectionZone attackZone;
    Animator animator;
    TouchingDirections TouchingDirections;
    Rigidbody2D rb;
    Damageable damageable;
    public DetectionZone cliffDetectionZone;
    public float walkSpeed = 5f;
    Vector2 walkDirectionVector = Vector2.right;
    public enum WalkableDirection { Right, Left};
    WalkableDirection _walkDirection;
    public WalkableDirection WalkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if(_walkDirection != value)
            {
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);
                if(value == WalkableDirection.Right)
                {
                    walkDirectionVector = Vector2.right;
                }
                else if(value == WalkableDirection.Left)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }
    public bool _hasTarget = false; 
    public float walkStopRate = 0.2f;

    public bool HasTarget { 
        get 
        { 
            return _hasTarget; 
        }
        private set 
        { 
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool("canMove");
        }
    }
    void Start()
    {
        
    }
    private void Update()
    {
        HasTarget =  attackZone.detectedColliders.FindIndex(item => item.tag == "Player") != -1 ;
    }
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        TouchingDirections = GetComponent<TouchingDirections>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
    }
    void FixedUpdate()
    {
        
        if ((TouchingDirections.IsOnWall  || cliffDetectionZone.detectedColliders.Count == 0) && TouchingDirections.IsGrounded)
        {
            FlipDirection();          
        }
        if(!damageable.IsHit)
        {
            if (CanMove) rb.velocity = new Vector2(walkDirectionVector.x * walkSpeed, rb.velocity.y);
            else rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y);
        }

    }
    private void FlipDirection()
    {

        if (WalkDirection == WalkableDirection.Right)
        {
            WalkDirection = WalkableDirection.Left;
        }
        else if(WalkDirection == WalkableDirection.Left)
        {
            WalkDirection = WalkableDirection.Right;
        }
    }
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}
