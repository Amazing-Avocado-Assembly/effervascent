using System;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bubble Bubble { get; private set; }
    public Rigidbody2D RB { get; private set; }

    public Transform projectilePrefab;
    public Transform indicator;
    public float pushForce = 10;
    public float volumePerSecond = 10f;
    public float maxVolume = 200;

    private Projectile projectile = null;
    private Vector3 projectileDirection;

    private static readonly float projectileOffset = 0.01f;
    public ParticleSystem popParticles;

    void Awake()
    {
        Bubble = GetComponentInChildren<Bubble>();
        RB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // If the player presses the left mouse button, spawn a projectile as a child of the player
        if (Input.GetMouseButtonDown(0))
        {
            if (projectile == null)
            {
                GameObject projectileObject = Instantiate(projectilePrefab.gameObject);
                projectileObject.transform.parent = transform;

                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 direction = mousePosition - transform.position;
                direction.z = 0;
                direction.Normalize();

                projectileDirection = direction;

                projectileObject.transform.localPosition = (Bubble.GetRadius() + projectileObject.GetComponentInChildren<Bubble>().GetRadius() + projectileOffset) * direction;

                projectile = projectileObject.GetComponent<Projectile>();
            }
        }
        // If the player holds the left mouse button, transfer volume from self to the bubble
        else if (Input.GetMouseButton(0))
        {
            if (projectile != null)
            {
                Bubble.volume -= Time.deltaTime * volumePerSecond;
                projectile.Bubble.volume += Time.deltaTime * volumePerSecond;

                projectile.transform.localPosition = (Bubble.GetRadius() + projectile.Bubble.GetRadius() + projectileOffset) * projectileDirection;
            }
        }
        // If the player let's go of the mouse button, release the projectile
        else if (Input.GetMouseButtonUp(0))
        {
            if (projectile != null)
            {
                projectile.transform.parent = null;

                // Add rigidbody to the projectile
                Rigidbody2D projectileRb = projectile.transform.AddComponent<Rigidbody2D>();
                projectileRb.gravityScale = RB.gravityScale;
                projectileRb.linearDamping = RB.linearDamping;
                projectileRb.freezeRotation = true;
                projectileRb.sharedMaterial = RB.sharedMaterial;
                projectile.Bubble.Rb = projectileRb;

                // Apply forces to the bubbles
                projectile.Bubble.ApplyForce(Bubble, pushForce);
                Bubble.ApplyForce(projectile.Bubble, pushForce);

                projectile = null;
            }
        }

        // If the mouse is not pressed, update indicator to be oriented towards the mouse
        if (!Input.GetMouseButton(0))
        {
            Vector3 mousePositionWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 directionIndicator = mousePositionWorld - transform.position;
            directionIndicator.z = 0;
            directionIndicator.Normalize();

            float angle = Mathf.Atan2(directionIndicator.y, directionIndicator.x) * Mathf.Rad2Deg - 90;
            indicator.rotation = Quaternion.Euler(0, 0, angle);
            indicator.localScale = Bubble.transform.localScale;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (popParticles != null) {
                Instantiate(popParticles, transform.position, Quaternion.identity);
            }
        Game.Instance.KillAndRespawn();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe"))
        {
            // If the player is colliding with a pipe, add volume to the bubble (deltaTime)
            Pipe pipe = collision.gameObject.GetComponent<Pipe>();
            if (!pipe) return;

            Bubble.volume = Math.Min(Bubble.volume + Time.deltaTime * pipe.volumePerSecond, maxVolume);
        }
    }
}
