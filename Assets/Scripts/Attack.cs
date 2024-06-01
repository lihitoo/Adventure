using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;
    private Vector2 deliveredKnockback;

    private void Start()
    {
        Vector2 deliveredKnockback = transform.parent.localScale.x * knockback.x > 0
            ? knockback
            : new Vector2(-knockback.x, knockback.y);
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.gameObject.GetComponent<Damageable>();
        if (damageable != null)
        {
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            if (gotHit) Debug.Log(collision.name + attackDamage);
        }
        else
        {
            Debug.Log("damageable = null");
        }
    }
}
