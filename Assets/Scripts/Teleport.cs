using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform target;

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform player = collision.gameObject.transform;
            Rigidbody2D RB = player.GetComponentInParent<Rigidbody2D>();
            RB.MovePosition(target.position);
            RB.linearVelocity = Vector3.zero;
        }
    }
}
