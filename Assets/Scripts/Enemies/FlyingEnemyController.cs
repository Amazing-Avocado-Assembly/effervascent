using UnityEngine;
using UnityEngine.Assertions;

public class FlyingEnemyController : EnemyController
{
    [SerializeField] float speed = 5.0f;
    [SerializeField] float acceleration = 1.0f;

    protected override void Awake()
    {
        base.Awake();
        Assert.IsTrue(rb.bodyType == RigidbodyType2D.Kinematic, "Flying enemies should have a Kinematic Rigidbody2D");
    }

    void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, direction * speed, acceleration * Time.fixedDeltaTime);
    }
}
