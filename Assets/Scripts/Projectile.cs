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
            return;
        }
        
        // If colliding with Captureable, determine if the projectile should bounce, or capture
        Captureable captureable = collision.gameObject.GetComponent<Captureable>();
        if (captureable != null) {
            if (captureable.minVolume <= Bubble.volume) {
                // Capture the captureable
                // 1) Tween
                //    - the center of the bubble to the center of the captureable
                //    - the volume of the bubble to the volume of the minWrappingBubbleVolume of the captureable

                // 2) Make the bubble parent of the captureable and disable the captureable's EnemyController

                // 3) Change gravity scale of the bubble to -0.1f
                
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
