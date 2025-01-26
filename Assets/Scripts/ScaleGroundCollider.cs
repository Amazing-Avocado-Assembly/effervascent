using NaughtyAttributes;
using UnityEngine;

public class ScaleGroundCollider : MonoBehaviour
{
    public float offset = 1.3f;
    
    [Button("Update Colliders")]
    void UpdateColliders() {
        // Scale the collider to the size of the sprite and offset it 1 from the left
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null) {
            BoxCollider2D collider = GetComponent<BoxCollider2D>();
            if (collider != null) {
                collider.size = spriteRenderer.size - new Vector2(offset, 0);
                collider.offset = new Vector2(offset / 2, 0);
            }
        }
    }

    void OnValidate() {
        UpdateColliders();
    }

    void Awake() {
        UpdateColliders();
    }
}
