using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform sprite;
    public Transform[] doors;
    public LineRenderer lineRenderer;
    public Material activeLineMaterial;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            // Tween the button to the pressed position
            sprite.DOLocalMoveY(0f, 0.5f).SetEase(Ease.OutBounce);
            // Disable collider
            GetComponent<Collider2D>().enabled = false;

            // Tween the door to the open position
            foreach (Transform door in doors) {
                door.DOLocalMove(Vector3.zero, 2f).SetEase(Ease.InOutCubic);
            }

            // If the line renderer is set, change the material color to white
            if (lineRenderer != null) {
                lineRenderer.material = activeLineMaterial;
            }
        }
    }
}
