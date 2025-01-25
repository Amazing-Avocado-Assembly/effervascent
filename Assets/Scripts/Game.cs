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
        UI.Hide();
        FinishPoint.Finished += () =>
        {
            UI.HeaderText = UI.TheEndText;
            UI.Show();
            isFinished = true;
        };

        UI.Ascended += () =>
        {
            UI.Hide();
            RespawnPoint.Kill().onComplete += () =>
            {
                isFinished = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            };
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
                UI.HeaderText = UI.PauseText;
                UI.Show();
            }
            else
            {
                UI.Hide();
            }
        }
    }
}
