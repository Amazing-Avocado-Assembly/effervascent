using UnityEngine;

public class EnemyController : MonoBehaviour
{
    protected Transform Player => Game.Instance.Player.transform;
    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}
