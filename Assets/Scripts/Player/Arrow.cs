using System;
using UnityEngine;
[RequireComponent(typeof(Damageable))]
public class Arrow : Attack
{
    [SerializeField] private float speed;
    [SerializeField] private AudioClip arrowHitSound;
    private Animator animator;
    private Rigidbody2D rb;
    private void Start()
    {
        deliveredKnockback = transform.localScale.x * knockback.x > 0
            ? knockback
            : new Vector2(-knockback.x, knockback.y);
    }
    public void Shoot(Vector2 direction)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }
    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(arrowHitSound,transform.position);
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            Invoke("DestroyObject",0.3f);
        }

        if(other.CompareTag("Enemies"))
        {
            base.OnTriggerEnter2D(other);
        }
    }
    void DestroyObject()
    {
        gameObject.SetActive(false);
    }
}
