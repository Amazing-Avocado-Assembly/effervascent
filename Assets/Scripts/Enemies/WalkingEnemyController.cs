using UnityEngine;
using NaughtyAttributes;

public class WalkingEnemyController : EnemyController
{
    [SerializeField] float width = .5f;
    [SerializeField] float groundDistance = .1f;
    [SerializeField] float wallDistance = .05f;
    [SerializeField] float speed = 1.0f;
    [SerializeField, Layer] int groundLayer;
    Direction currentDirection = Direction.Left;

    void FixedUpdate()
    {
        if (!CheckGround(Direction.None))
        {
            // Enemy is falling
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            return;
        }

        if (!CheckGround(currentDirection) || CheckWall(currentDirection))
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

        Vector3 wallOffset = Vector3.right * wallDistance;
        Gizmos.color = CheckWall(Direction.Left) ? Color.green : Color.red;
        Gizmos.DrawLine(leftPosition, leftPosition - wallOffset);
        Gizmos.color = CheckWall(Direction.Right) ? Color.green : Color.red;
        Gizmos.DrawLine(rightPosition, rightPosition + wallOffset);
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

    bool CheckWall(Direction direction)
    {
        return Physics2D.Raycast(
            transform.position + new Vector3((int)direction * (width / 2 - 0.01f), 0.01f),
            Vector2.right * (int)direction,
            wallDistance + 0.01f,
            1 << groundLayer);
    }
}
