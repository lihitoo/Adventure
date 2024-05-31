using UnityEngine;

public class Shootable : MonoBehaviour 
{
    [SerializeField] private float speed;

    public void Shoot(Vector2 direction)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = direction * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Xử lý va chạm nếu cần
        if(!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        
    }
}