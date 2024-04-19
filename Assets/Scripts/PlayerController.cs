using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector2 moveInput;
    private bool _IsMoving = false;
    public bool isMoving { get 
        {
            return _IsMoving ;
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
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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
        rb.velocity = new Vector2(moveInput.x * walkSpeed , rb.velocity.y);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
    }
}
