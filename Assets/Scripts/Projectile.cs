using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Bubble Bubble { get; private set; }
    public int bouncesToLive = 1;
    public float transferBackRatio = 0.5f;

    void OnEnable()
    {
        Bubble = GetComponentInChildren<Bubble>();
    }

    void Update()
    {
        
    }

    // On collision with anything, kill self
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only kill self if the projectile has rigidbody
        if (Bubble.Rb == null) {
            return;
        }

        // If colliding with a bubble, add volume to the bubble
        Bubble otherBubble = collision.gameObject.GetComponentInChildren<Bubble>();
        if (otherBubble != null) {
            otherBubble.volume += Bubble.volume * transferBackRatio;
            Destroy(gameObject);
        } else {
            // If colliding with anything else, reduce bouncesToLive
            bouncesToLive--;

            if (bouncesToLive < 0) {
                Destroy(gameObject);
            }
        }
    }
}
