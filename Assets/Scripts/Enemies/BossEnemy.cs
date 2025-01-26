using System;
using UnityEngine;
using UnityEngine.Splines;

public class BossEnemy : MonoBehaviour
{
    public SplineContainer path;
    public Transform center;

    private float pathPosition = 0f;

    public float Speed { get; set; }
    public float Size { get; set; } = 0.25f;

    public void Update()
    {
        pathPosition = (pathPosition + Time.deltaTime * 0.1f) % 1f;
        path.Spline.Evaluate(pathPosition, out var pos, out _, out var _);
        transform.position = new Vector3(pos.x, pos.y, pos.z) + path.transform.position;
        var up = center.position - transform.position;
        up.z = 0f;
        up.Normalize();
        transform.up = up;
        // transform.rotation = Quaternion.LookRotation(new Vector3(up.y, -up.x, 0), up);
    }
    
    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.CompareTag("Projectile")) {
            
        }
    }
}
