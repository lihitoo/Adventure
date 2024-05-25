using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    // Start is called before the first frame update
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent<int, int> healthChanged;
    Animator animator;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
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
            healthChanged?.Invoke(_health, MaxHealth);
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
            if (Health - damage < 0) Health = 0;
            else Health -= damage;
            //animator.SetTrigger("hit");
            IsHit = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        return false;
    }
    public void Heal(int healthRestore)
    {
        if(IsAlive)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
        }
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
