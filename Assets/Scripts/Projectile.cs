using System;
using DG.Tweening;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Bubble Bubble { get; private set; }
    public int bouncesToLive = 1;
    public float transferBackRatio = 0.5f;
    public ParticleSystem popParticles;

    private Captureable captured = null;

    void OnEnable()
    {
        Bubble = GetComponentInChildren<Bubble>();
    }

    void Update()
    {
        // If the scale is zero or if distance from camera is greater, than 20, destroy self
        if (transform.localScale.magnitude < 0.01f || Vector3.Distance(transform.position, Camera.main.transform.position) > 20) {
            transform.DOComplete();
            Pop();
        }
    }

    // On collision with anything, kill self
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Only process if the projectile has rigidbody
        if (Bubble == null || Bubble.Rb == null) {
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
            Pop();
            return;
        }
        
        // If colliding with Captureable, determine if the projectile should bounce, or capture
        Captureable captureable = collision.gameObject.GetComponent<Captureable>();
        if (captureable != null) {
            if (captureable.minVolume <= Bubble.volume) {
                // Capture the captureable:
                // * Remove the captureable rigidbody
                Destroy(captureable.GetComponent<Rigidbody2D>());
                // * Make the bubble parent of the captureable
                captureable.transform.SetParent(transform);
                // * Disable the captureable's EnemyController on the captureable
                EnemyController ec = captureable.GetComponent<EnemyController>();
                if (ec) ec.enabled = false;
                // * Tween the captureOrigin of the captureable to the center of the bubble
                captureable.transform.DOLocalMove(-captureable.captureOrigin.localPosition, 0.5f);
                // * Tween the volume of the bubble to the volume of the minWrappingBubbleVolume of the captureable
                float finalVolume = Bubble.RadiusToVolume(captureable.minWrappingBubbleRadius);
                if (finalVolume > Bubble.GetVolume()) Bubble.DOVolume(finalVolume, 0.5f);
                // * Change gravity scale of the bubble to -0.1f and damping to 0.4f
                Bubble.Rb.gravityScale = -0.1f;
                Bubble.Rb.linearDamping = 5f;
                // * Tween the projectile scale to 0
                transform.DOScale(Vector3.zero, 1.0f);
                // * Set captured to the captureable
                captured = captureable;                
                return;
            }
            // else, bounce
        }

        // If coliding with tag enemy, destroy self immediately
        if (collision.gameObject.CompareTag("Enemy")) {
            Pop();
            return;
        }

        // If colliding with anything else, reduce bouncesToLive
        bouncesToLive--;

        if (bouncesToLive < 0) {
            Pop();
        }
    }

    private void Pop() {
        Destroy(gameObject);
        // Instantiate popParticles at the position of the projectile
        if (popParticles != null) {
            Instantiate(popParticles, transform.position, Quaternion.identity);
        }
    }
}
