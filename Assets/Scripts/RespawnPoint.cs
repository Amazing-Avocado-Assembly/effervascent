using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Player playerPrefab;
    [SerializeField] private CinemachineCamera playerFollowCamera;
    public Player Player { get; private set; }

    private void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        if (Player != null)
        {
            Destroy(Player.gameObject);
        }
        Player = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        if (playerFollowCamera != null) {
            playerFollowCamera.Follow = Player.transform;
        }

        Bubble bubble = Player.GetComponentInChildren<Bubble>();
        bubble.volume = 0;
        bubble.Rb.bodyType = RigidbodyType2D.Static;

        Tween t = bubble.DOVolume(bubble.initialVolume, 2.0f);
        t.SetDelay(2f);
        t.onComplete += () => {
            bubble.Rb.bodyType = RigidbodyType2D.Dynamic;
            // Add up velocity to the player
            Player.RB.linearVelocity = new Vector2(0, 1);
        };
    }
}
