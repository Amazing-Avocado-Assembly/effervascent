using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public RespawnPoint RespawnPoint;
    public FinishPoint FinishPoint;
    public UI UI;

    public Player Player => RespawnPoint.Player;

    private void Awake()
    {
        // if (Instance != null && Instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
        UI.Hide();
        FinishPoint.Finished += () =>
        {
            UI.Show();
        };

        UI.Ascended += () =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        };
        UI.Escaped += () =>
        {
            Application.Quit();
        };
    }
}
