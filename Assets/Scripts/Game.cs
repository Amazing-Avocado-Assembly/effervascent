using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game Instance { get; private set; }

    public RespawnPoint RespawnPoint;
    public FinishPoint FinishPoint;
    public UI UI;
    public Texture2D CursorTexture;

    public Player Player => RespawnPoint.Player;

    private InputAction pauseAction;
    private bool isPaused = false;
    private bool isFinished = false;
    private AudioSource audioSource;

    private void Awake()
    {
        // if (Instance != null && Instance != this)
        // {
        //     Destroy(gameObject);
        //     return;
        // }

        Instance = this;
        // DontDestroyOnLoad(gameObject);
        UI.Show(UIMode.Start);
        // UI.Hide();
        FinishPoint.Finished += () =>
        {
            UI.Show(UIMode.End);
            isFinished = true;
        };

        UI.Ascended += () =>
        {
            UI.Hide();
            if (isFinished || isPaused) {
                RespawnPoint.Kill().onComplete += () =>
                {
                    isFinished = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                };
            }
        };
        UI.Escaped += () =>
        {
            Application.Quit();
        };

        pauseAction = InputSystem.actions.FindAction("Pause");
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Spawn();
        Cursor.SetCursor(CursorTexture, Vector2.zero, CursorMode.Auto);
    }

    public void Spawn()
    {
        RespawnPoint.Spawn();
    }

    public void KillAndRespawn()
    {
        RespawnPoint.Respawn();
    }

    public void PlayGlobalSound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame() && !isFinished)
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                UI.Show(UIMode.Pause);
            }
            else
            {
                UI.Hide();
            }
        }
    }
}
