using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Splines;

public class BossEnemy : MonoBehaviour
{
    public const float InitialSpeed = 2f;
    public const float InitialSize = 1.0f;

    public SplineContainer path;
    public Transform center;
    public float InitialPathPosition = 0f;
    public int MaxHealth = 2;
    public CollisionListener Collider;

    private float pathPosition = 0f;

    public float Speed { get; set; } = InitialSpeed;
    public int Health { get; set; }

    public event Action Defeated;

    public void Update()
    {
        // pathPosition = (pathPosition + Time.deltaTime * 0.1f) % 1f;
    }

    public void Reset()
    {
        SetPathPosition(InitialPathPosition);
        SetHealth(MaxHealth);
    }

    private void Start()
    {
        Reset();
        Collider.Collided += OnCollided;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.parent.gameObject.CompareTag("Projectile"))
        {
            var projectile = collider.transform.parent;
            var localColliderPos = transform.worldToLocalMatrix.MultiplyPoint3x4(projectile.transform.position);
            // NB: The projectile on the right-hand side relative to the boss, move 
            var dir = Math.Sign(localColliderPos.x);
            if (dir == 0)
            {
                dir = 1;
            }
            var tweenPathPosition = pathPosition;
            DOTween.To(() => tweenPathPosition, p =>
            {
                tweenPathPosition = p;
                SetPathPosition(p);
            }, tweenPathPosition + -0.1f * dir, 1f / Speed);
        }
    }

    private void OnCollided(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            SetHealth(Health - 1);
        }
    }

    public void SetPathPosition(float p)
    {
        if (p < 0f)
        {
            p = p % 1f + 1f;
        }

        pathPosition = p % 1f;
        path.Spline.Evaluate(pathPosition, out var pos, out _, out var _);
        transform.position = new Vector3(pos.x, pos.y, pos.z) + path.transform.position;
        var up = center.position - transform.position;
        up.z = 0f;
        up.Normalize();
        transform.up = up;
    }

    public void SetHealth(int h)
    {
        if (h <= 0)
        {
            Health = 0;
            transform.DOScale(0f, 0.5f);
            Defeated?.Invoke();
            return;
        }

        Health = h;
        transform.DOScale(h / (float)MaxHealth, 0.5f);
        Speed = InitialSpeed * Math.Max(1, MaxHealth - h + 1);
    }
}
