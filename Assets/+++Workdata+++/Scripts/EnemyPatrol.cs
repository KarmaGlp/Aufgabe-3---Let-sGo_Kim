using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform leftPoint;
    public Transform rightPoint;
    public float speed = 2f;

    private bool movingRight = true;
    private Rigidbody2D rb;

    public bool canMove = false;

    public void AllowMovement(bool allow)
    {
        canMove = allow;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (leftPoint == null || rightPoint == null)
        {
            Debug.LogError("❌ LeftPoint und/oder RightPoint wurden nicht im Inspector zugewiesen!");
            enabled = false;
        }
    }

    void Update()
    {
        if (!canMove) return;

        Move();
        Flip();
    }

    void Move()
    {
        if (movingRight)
        {
            rb.linearVelocity = new Vector2(speed, rb.linearVelocity.y);
            if (transform.position.x >= rightPoint.position.x - 0.1f)
                movingRight = false;
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, rb.linearVelocity.y);
            if (transform.position.x <= leftPoint.position.x + 0.1f)
                movingRight = true;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x = movingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }

    public void StopMoving()
    {
        canMove = false;
        rb.linearVelocity = Vector2.zero; // Gegner bleibt sofort stehen
    }

    void OnDrawGizmos()
    {
        if (leftPoint != null && rightPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(leftPoint.position, rightPoint.position);
            Gizmos.DrawSphere(leftPoint.position, 0.1f);
            Gizmos.DrawSphere(rightPoint.position, 0.1f);
        }
    }
}