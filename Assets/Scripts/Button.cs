using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform sprite;
    public Door[] doors;
    public LineRenderer lineRenderer;
    public Material activeLineMaterial;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            if (TryGetComponent<AudioSource>(out var buttonSound))
            {
                buttonSound.Play();
            }

            // Tween the button to the pressed position
            sprite.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBounce);
            // Disable collider
            GetComponent<Collider2D>().enabled = false;

            // Tween the door to the open position
            foreach (Door door in doors)
            {
                door.Toggle();
            }

            // If the line renderer is set, change the material color to white
            if (lineRenderer != null)
            {
                lineRenderer.material = activeLineMaterial;
            }
        }
    }
}
