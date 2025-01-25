using System.Linq;
using NaughtyAttributes;
using UnityEngine;

public class FlyingEnemyController : EnemyController
{
    enum State { Idle, Chase }

    [SerializeField] float speed = 5.0f;
    [SerializeField] float maxAcceleration = 1.0f;
    [SerializeField] float avoidRadius = 1.0f;
    [SerializeField] float avoidAcceleration = 0.5f;
    [SerializeReference, Layer] string enemyLayer;
    [ShowNonSerializedField] State state = State.Idle;

    void FixedUpdate()
    {
        Vector2 acceleration = Vector2.zero;

        // Avoid other enemies
        var enemyPositions = Physics2D.OverlapCircleAll((Vector2)transform.position, avoidRadius, 1 << gameObject.layer)
                                      .Where(c => c.CompareTag("Enemy") && c.transform.parent != transform)
                                      .Select(c => c.transform.position);
        foreach (var enemyPosition in enemyPositions)
        {
            Vector2 direction = transform.position - enemyPosition;
            if (direction.sqrMagnitude < 0.01f)
            {
                direction = Random.insideUnitCircle * 0.01f;
            }
            acceleration += direction.normalized * avoidAcceleration / direction.sqrMagnitude;
        }

        if (state == State.Idle)
        {
            acceleration += -rb.linearVelocity.normalized * maxAcceleration;
        }
        else // (state == State.Chase)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            acceleration += direction * maxAcceleration;
        }

        acceleration = Vector2.ClampMagnitude(acceleration, maxAcceleration);
        rb.linearVelocity += acceleration * Time.fixedDeltaTime;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.Chase;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.Idle;
        }
    }
}
