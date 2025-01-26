using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Button : MonoBehaviour
{
    public Transform sprite;
    public Door[] doors;
    public LineRenderer lineRenderer;
    public Material activeLineMaterial;
    public bool IsInitiallyPressed = false;

    public event Action Pressed;
    public event Action Released;

    public bool IsPressed { get; set; }

    private float initialButtonY;

    private void Start()
    {
        Toggle(IsInitiallyPressed, animated: false);
        initialButtonY = sprite.localPosition.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Projectile"))
        {
            Toggle(isPressed: true);
        }
    }

    public void Toggle(bool? isPressed = null, bool animated = true, float duration = 0.5f)
    {
        if (isPressed != null)
        {
            if (isPressed.Value == IsPressed)
            {
                return;
            }

            IsPressed = isPressed.Value;
        }
        else
        {
            IsPressed = !IsPressed;
        }

        var targetY = IsPressed ? 0f : initialButtonY;
        if (animated)
        {
            if (TryGetComponent<AudioSource>(out var buttonSound))
            {
                buttonSound.Play();
            }

            // Tween the button to the pressed position
            sprite.DOLocalMoveY(targetY, 0.5f).SetEase(Ease.OutBounce);
        }
        else
        {
            sprite.localPosition = new Vector3(sprite.localPosition.x, targetY, sprite.localPosition.z);
        }

        // Disable collider
        // GetComponent<Collider2D>().enabled = false;

        // Tween the door to the open position
        foreach (Door door in doors)
        {
            door.Toggle(animated: animated);
        }

        // If the line renderer is set, change the material color to white
        if (lineRenderer != null)
        {
            lineRenderer.material = activeLineMaterial;
        }
    }
}
