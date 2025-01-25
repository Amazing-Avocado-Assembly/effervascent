using NaughtyAttributes;
using UnityEngine;

public class ShootingEnemy : EnemyController
{
    enum State { Idle, PlayerDetected }
    [SerializeField] EnemyBullet bulletPrefab;
    [SerializeField] float coolDown = 2.0f;
    [ShowNonSerializedField] State state = State.Idle;
    float lastAttackTime = 0;

    void Update()
    {
        if (state == State.Idle)
            return;

        if (Time.time - lastAttackTime > coolDown && PlayerVisible)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.SetDirection(PlayerDirection);
            lastAttackTime = Time.time;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            state = State.PlayerDetected;
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
