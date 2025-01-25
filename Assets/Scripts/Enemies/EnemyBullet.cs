using UnityEngine;

public class EnemyBullet : EnemyController
{
    [SerializeField] float speed = 5.0f;

    public void SetDirection(Vector2 direction)
    {
        rb.SetRotation(Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90);
        rb.linearVelocity = direction.normalized * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
