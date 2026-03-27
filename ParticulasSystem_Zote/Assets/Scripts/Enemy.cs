using System;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Vector2 originOffset;

    [SerializeField]
    Vector2 toOffset;

    Rigidbody2D rb;

    [SerializeField]
    float speed;

    [SerializeField]
    LayerMask groundlayer;

    [SerializeField]
    float speedIncreaseOnWall = 5.5f;

    [SerializeField]
    float maxSpeed = 12f;

    bool hasTurnedAtObstacle;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        bool hasGround = HasGround();
        bool hasWall = HasWall();
        bool hasObstacle = hasWall || !hasGround;

        if (!hasObstacle)
        {
            hasTurnedAtObstacle = false;
            rb.linearVelocity = Vector2.right * speed;
            return;
        }

        // Evita virar/acelerar várias vezes enquanto ainda está no mesmo obstáculo
        if (hasTurnedAtObstacle)
            return;

        if (hasWall)
            IncreaseSpeed();

        ChangeDirection();
        hasTurnedAtObstacle = true;
    }

    void IncreaseSpeed()
    {
        float direction = Mathf.Sign(speed);
        if (direction == 0f)
            direction = 1f;

        float newAbsSpeed = Mathf.Min(Mathf.Abs(speed) + speedIncreaseOnWall, maxSpeed);
        speed = newAbsSpeed * direction;
    }

    void ChangeDirection()
    {
        speed = speed * -1;
        originOffset.x = originOffset.x * -1;
        toOffset.x = toOffset.x * -1;
    }

    bool HasGround()
    {
        Vector2 from = (Vector2)transform.position + originOffset;
        return Physics2D.Raycast(from, Vector2.down, 1, groundlayer);
    }

    bool HasWall()
    {
        Vector2 from = (Vector2)transform.position + originOffset;
        return Physics2D.Raycast(from, Vector2.left, 2, groundlayer);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Health>().TakeDemage(10);
            Debug.Log("Tomou dano");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector2 from = (Vector2)transform.position + originOffset;
        Vector2 to = (Vector2)transform.position + toOffset + Vector2.down;

        Gizmos.DrawLine(from, to);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.left);
    }
}