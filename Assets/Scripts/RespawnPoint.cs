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
        if (playerFollowCamera != null)
            playerFollowCamera.Follow = Player.transform;
    }
}
