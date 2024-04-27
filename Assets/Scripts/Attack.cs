using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    // Start is called before the first frame update
    public int attackDamage = 10;
    public Vector2 knockback= Vector2.zero;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable != null)
        {
            Vector2 deliveredKnockback = transform.parent.localScale.x * knockback.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.Hit(attackDamage,deliveredKnockback);
            if(gotHit) Debug.Log(collision.name + attackDamage);
        }
    }
}
