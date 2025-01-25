using DG.Tweening;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private static readonly float globalObjectScale = 0.2f;

    public float initialVolume = 100;

    public float volumeScale = 1;

    public float volume;

    public Rigidbody2D Rb { get; set; }

    public float VolumeToRadius(float volume)
    {
        if (volume <= 0) return 0;

        // Sphere volume to radius
        return Mathf.Pow((3 * volume) / (4 * Mathf.PI), 1.0f / 3.0f) * globalObjectScale;
    }

    public float RadiusToVolume(float radius)
    {
        if (radius <= 0) return 0;

        // Sphere radius to volume
        return ((4 * Mathf.PI * Mathf.Pow(radius / globalObjectScale, 3)) / 3) / volumeScale;
    }

    public float GetRadius()
    {
        return VolumeToRadius(GetVolume());
    }

    public float GetVolume()
    {
        return volume * volumeScale;
    }

    private void OnEnable()
    {
        volume = initialVolume;
        Rb = GetComponentInParent<Rigidbody2D>();

        float radius = GetRadius();
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
    }

    private void Update()
    {
        float radius = GetRadius();
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        if (Rb != null) Rb.mass = GetVolume();
    }

    public void ApplyForce(Bubble bubble, float force)
    {
        Vector2 direction = transform.position - bubble.transform.position;
        direction.Normalize();

        float myVolume = GetVolume();
        float otherVolume = bubble.GetVolume();

        float multiplier = otherVolume * force / (myVolume + otherVolume);

        // Apply force to the bubble
        Rb.linearVelocity += direction * multiplier;
    }

    public Tween DOVolume(float to, float duration)
    {
        return DOTween.To(() => volume, x => volume = x, to, duration);
    }
}