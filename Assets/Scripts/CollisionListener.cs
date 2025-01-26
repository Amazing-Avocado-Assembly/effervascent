using System;
using UnityEngine;

public class CollisionListener : MonoBehaviour
{
    public event Action<Collision2D> Collided;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Collided?.Invoke(other);
    }
}
