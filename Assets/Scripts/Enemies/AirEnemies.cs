using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AirEnemies : MonoBehaviour
{
    public DetectionZone attackZone;
    Animator animator;
    Rigidbody2D rb;
    Damageable damageable;
    public float speed = 5f;
    Vector2 walkDirectionVector = Vector2.right;
    public bool _hasTarget = false;
    public float walkStopRate = 0.2f;
    [SerializeField] private Transform[] wayPoints;
    private int wayPointsNum = 0;
    private float distancePointToReach = 0.5f;
    private Transform nextWayPoints;

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

    private void Update()
    {
        HasTarget = attackZone.detectedColliders.FindIndex(item => item.tag == "Player") != -1;
        Fly();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        damageable = GetComponent<Damageable>();
        nextWayPoints = wayPoints[0];
    }
    private void Fly()
    {
        Vector2 directionToFly = (nextWayPoints.position - transform.position).normalized;
        if (directionToFly.x >= 0) transform.eulerAngles = new Vector3(0, 180, 0);
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        if (_hasTarget) directionToFly = Vector2.zero;
        rb.velocity = directionToFly * speed;
        float distance = Vector2.Distance(nextWayPoints.position, transform.position);
        if (distancePointToReach >= distance)
        {
            wayPointsNum++;
            if (wayPointsNum > wayPoints.Length)
            {
                wayPointsNum = 0;
            }

            nextWayPoints = wayPoints[wayPointsNum];
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, knockback.y + rb.velocity.y);
    }
}