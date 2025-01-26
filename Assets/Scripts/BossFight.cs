using System.Collections;
using UnityEngine;

public enum BossFightStatus
{
    NotBegun,
    InProgress,
    Ended
}

public class BossFight : MonoBehaviour
{
    public TriggerListener StartTrigger;
    public Door[] Entry;
    public Door[] Exit;
    public Button Button;
    public BossEnemy Boss;

    public BossFightStatus Status { get; set; } = BossFightStatus.NotBegun;

    public void Awake()
    {
        Reset();
    }
    
    public void Reset()
    {
        foreach (var door in Entry)
        {
            door.Toggle(isOpen: true, animated: false);
        }
        StartTrigger.Triggered += OnStartTrigger;
        Boss.SetPathPosition(Boss.InitialPathPosition);
        Status = BossFightStatus.NotBegun;
    }

    private void OnStartTrigger(Collider2D collider)
    {
        if (!collider.CompareTag("Player"))
        {
            return;
        }

        StartTrigger.Triggered -= OnStartTrigger;
        BeginFight();
    }

    private void BeginFight()
    {
        foreach (var door in Entry)
        {
            door.Toggle(isOpen: false, duration: 0.5f);
        }

        StartCoroutine(UnlockButton());
    }

    private IEnumerator UnlockButton()
    {
        yield return new WaitForSeconds(Random.Range(10, 20));
        if (Status == BossFightStatus.InProgress && Button.IsPressed)
        {
            Button.Toggle(isPressed: false);
        }
    }

    private void SpawnBullshit()
    {
    }
}
