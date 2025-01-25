using System;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Bubble Bubble { get; private set; }
    public int bouncesToLive = 1;
    public float transferBackRatio = 0.5f;

    private Captureable captured = null;

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
        // Only process if the projectile has rigidbody
        if (Bubble.Rb == null) {
            return;
        }

        // If captured a captureable, just bounce
        if (captured != null) {
            return;
        }

        // If colliding with a bubble, add volume to the bubble
        Bubble otherBubble = collision.gameObject.GetComponentInChildren<Bubble>();
        if (otherBubble != null) {
            otherBubble.volume += Bubble.volume * transferBackRatio;
            Destroy(gameObject);
            return;
        }
        
        // If colliding with Captureable, determine if the projectile should bounce, or capture
        Captureable captureable = collision.gameObject.GetComponent<Captureable>();
        if (captureable != null) {
            if (captureable.minVolume <= Bubble.volume) {
                // Capture the captureable
                // * Remove the captureable rigidbody
                Destroy(captureable.GetComponent<Rigidbody2D>());                
                // * Make the bubble parent of the captureable and disable the captureable's EnemyController
                captureable.transform.SetParent(transform);
                // * Tween the captureOrigin of the captureable to the center of the bubble
                captureable.transform.DOLocalMove(-captureable.captureOrigin.localPosition, 0.5f);
                // * Tween the volume of the bubble to the volume of the minWrappingBubbleVolume of the captureable
                
                // * Change gravity scale of the bubble to -0.1f
                Bubble.Rb.gravityScale = -0.1f;

                captured = captureable;                
                return;
            }
            // else, bounce
        }

        // If colliding with anything else, reduce bouncesToLive
        bouncesToLive--;

        if (bouncesToLive < 0) {
            Destroy(gameObject);
        }
    }
}
