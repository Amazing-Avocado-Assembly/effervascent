using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Assertions;

public class FlyingEnemyController : EnemyController
{
    enum State { Idle, Chase }

    [SerializeField] float speed = 5.0f;
    [SerializeField] float acceleration = 1.0f;
    [ShowNonSerializedField] State state = State.Idle;

    void FixedUpdate()
    {
        if (state == State.Idle)
        {
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, acceleration * Time.fixedDeltaTime);
        }
        else // (state == State.Chase)
        {
            Vector2 direction = (Player.position - transform.position).normalized;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, acceleration * Time.fixedDeltaTime);
        }
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
