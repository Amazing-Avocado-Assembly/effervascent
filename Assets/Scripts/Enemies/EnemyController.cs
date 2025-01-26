using UnityEngine;
using NaughtyAttributes;

public class EnemyController : MonoBehaviour
{
    protected Rigidbody2D rb;
    [SerializeField] protected float despawnDistance = -1;
    [SerializeField, Layer] protected string groundLayer = "Ground";
    [SerializeField, Layer] protected string blockLayer = "EnemyBlocker";

    protected Transform Player => Game.Instance.Player ? Game.Instance.Player.transform : null;
    protected Vector2 PlayerDirection => Player.position - transform.position;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {
        if (despawnDistance > 0
            && Player != null
            && Vector2.Distance(transform.position, Player.position) > despawnDistance)
        {
            Destroy(gameObject);
        }
    }

    protected bool PlayerVisible => !Physics2D.Raycast(transform.position,
                                      Player.position - transform.position,
                                      PlayerDirection.magnitude,
                                      1 << LayerMask.NameToLayer("Ground"));
}
