using UnityEngine;

public class RespawnPoint : MonoBehaviour
{
    public static RespawnPoint Instance { get; private set; }

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Player playerPrefab;
    public Player Player { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

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
    }
}
