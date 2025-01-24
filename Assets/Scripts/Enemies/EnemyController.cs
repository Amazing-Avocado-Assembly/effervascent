using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Transform player;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // TODO: Cache this in a singleton
        rb = GetComponent<Rigidbody2D>();
    }
}
