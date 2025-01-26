using System;
using UnityEngine;

public class TriggerListener : MonoBehaviour
{
    public event Action<Collider2D> Triggered;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Triggered?.Invoke(other);
    }
}
