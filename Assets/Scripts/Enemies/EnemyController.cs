using UnityEngine;
using NaughtyAttributes;

public class EnemyController : MonoBehaviour
{
    protected Transform Player => Game.Instance.Player.transform;
    protected Rigidbody2D rb;
    [SerializeField, Layer] protected string groundLayer = "Ground";
    [SerializeField, Layer] protected string blockLayer = "EnemyBlocker";

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected Vector2 PlayerDirection => Player.position - transform.position;

    protected bool PlayerVisible
    {
        get
        {
            var direction = Player.position - transform.position;
            return !Physics2D.Raycast(transform.position,
                                      Player.position - transform.position,
                                      direction.magnitude,
                                      1 << LayerMask.NameToLayer("Ground"));
        }
    }
}
