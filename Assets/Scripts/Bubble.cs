using UnityEngine;

public class Bubble : MonoBehaviour
{
    private static readonly float globalObjectScale = 0.2f;

    public float initialVolume = 100;
    public float volumeScale = 1;

    public float volume;

    private Rigidbody2D rb;

    private float VolumeToRadius(float volume)
    {
        // Sphere volume to radius
        return Mathf.Pow((3 * volume) / (4 * Mathf.PI), 1.0f / 3.0f) * globalObjectScale;
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
        rb = GetComponentInParent<Rigidbody2D>();
    }

    private void Update()
    {
        float radius = GetRadius();
        transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2);
        rb.mass = GetVolume();
    }

    public void ApplyForce(Bubble bubble, float force)
    {
        Vector2 direction = transform.position - bubble.transform.position;
        direction.Normalize();

        float myVolume = GetVolume();
        float otherVolume = bubble.GetVolume();

        float multiplier = otherVolume * force / (myVolume + otherVolume);
        Debug.Log(multiplier);

        // Apply force to the bubble
        rb.linearVelocity += direction * multiplier;
    }
}