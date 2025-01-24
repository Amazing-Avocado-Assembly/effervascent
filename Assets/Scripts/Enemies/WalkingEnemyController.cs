using UnityEditor.Callbacks;
using UnityEngine;
using NaughtyAttributes;

public class WalkingEnemyController : EnemyController
{
    [SerializeField] float width = .5f;
    [SerializeField] float groundDistance = .1f;
    [SerializeField] float speed = 1.0f;
    Direction currentDirection = Direction.Left;
    [SerializeField, Layer] int groundLayer;

    void FixedUpdate()
    {
        if (!CheckGround(Direction.None))
        {
            // Enemy is falling
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (!CheckGround(currentDirection))
        {
            currentDirection = currentDirection.Invert();
        }
        // Keep vertical velocity, set vertical velocity based on the current direction
        rb.linearVelocity = new Vector2((int)currentDirection * speed, rb.linearVelocity.y);
    }

    void OnDrawGizmos()
    {
        Vector3 leftPosition = transform.position + Vector3.left * width / 2;
        Vector3 rightPosition = transform.position + Vector3.right * width / 2;
        Vector3 groundOffset = Vector3.down * groundDistance;

        Gizmos.color = CheckGround(Direction.None) ? Color.green : Color.red;
        Gizmos.DrawLine(transform.position, transform.position + groundOffset);
        Gizmos.color = CheckGround(Direction.Left) ? Color.green : Color.red;
        Gizmos.DrawLine(leftPosition, leftPosition + groundOffset);
        Gizmos.color = CheckGround(Direction.Right) ? Color.green : Color.red;
        Gizmos.DrawLine(rightPosition, rightPosition + groundOffset);
    }

    /// <summary>
    /// Return true, if the enemy detects ground in the specified direction.
    /// </summary>
    bool CheckGround(Direction direction)
    {
        return Physics2D.Raycast(
            transform.position + new Vector3((int)direction * width / 2, 0.01f),
            Vector2.down,
            groundDistance + 0.01f,
            1 << groundLayer);
    }
}
