using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<int, Vector2> damageableHit;
    Animator animator;
    [SerializeField]
    private float _maxHealth = 100f;
    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }
    [SerializeField]
    private int _health = 500;
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0) IsAlive = false;
        }
    }
    private bool isInvincible;
    private float timeSinceHit = 0f;
    public float invincibleTime = 5f;
    [SerializeField]
    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        set
        {
            _isAlive = value;
            animator.SetBool("isAlive", value);
        }
    }

  

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public bool IsHit
    {
        get
        {
            return animator.GetBool("isHit");
        }
        set
        {
            animator.SetBool("isHit", value);
        }
    }
    public bool Hit(int damage,Vector2 knockback)
    {
        if(IsAlive && !isInvincible)
        {
            Health -= damage;
            //animator.SetTrigger("hit");
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            return true;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isInvincible)
        {
            if(timeSinceHit > invincibleTime)
            {
                isInvincible = false;
                timeSinceHit = 0;
            }
            timeSinceHit += Time.deltaTime;
        }
        //Hit(2);
    }
}
