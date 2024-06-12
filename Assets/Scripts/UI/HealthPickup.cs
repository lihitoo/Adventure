using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    // Start is called before the first frame update
    public int healthRestored = 10;
    public float moveSpeed = 1.0f; // Tốc độ di chuyển
    public float moveDistance = 1.0f; // Khoảng cách di chuyển
    private Vector3 initialPosition; // Vị trí ban đầu
    void Start()
    {
        initialPosition = transform.position; // Lưu vị trí ban đầu
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();
        if(damageable && damageable.Health<damageable.MaxHealth)
        {
            damageable.Heal(healthRestored);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        float newY = Mathf.PingPong(Time.time * moveSpeed, moveDistance) + initialPosition.y;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
