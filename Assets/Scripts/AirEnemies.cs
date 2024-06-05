using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public DetectionZone attackZone;
    Animator animator;
    TouchingDirections TouchingDirections;
    Rigidbody2D rb;
    Damageable damageable;
    public float speed = 5f;
    Vector2 walkDirectionVector = Vector2.right;
    public bool _hasTarget = false;
    public float walkStopRate = 0.2f;
    [SerializeField] private Transform[] wayPoints;
    private int wayPointsNum = 0;
    private float distancePointToReach = 0.2f;

    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool("hasTarget", value);
        }
    }

    public bool CanMove
    {
        get { return animator.GetBool("canMove"); }
    }

    void Start()
    {
    }

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.FindIndex(item => item.tag == "Player") != -1;
        Fly();
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
    }

    private void Fly()
    {
        Vector2 directionToFly = (wayPoints[wayPointsNum].position - transform.position).normalized;
        rb.velocity = directionToFly * speed;
        float distance = Vector2.Distance(directionToFly, transform.position);
        Debug.Log(distance);
        if (distancePointToReach >= distance)
        {
            wayPointsNum++;
            if (wayPointsNum > wayPoints.Length)
            {
                wayPointsNum = 0;
            }
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}