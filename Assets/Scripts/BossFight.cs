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
    public Transform Center;

    public BossFightStatus Status { get; set; } = BossFightStatus.NotBegun;

    public void Start()
    {
        Boss.Defeated += () =>
        {
            foreach (var door in Exit)
            {
                door.Toggle(isOpen: true);
            }
            Status = BossFightStatus.Ended;
            Game.Instance.PlayerFollowCamera.Follow = Game.Instance.Player.transform;
        };
        Reset();
    }

    public void Reset()
    {
        foreach (var door in Entry)
        {
            door.Toggle(isOpen: true, animated: false);
        }
        foreach (var door in Exit)
        {
            door.Toggle(isOpen: false, animated: false);
        }

        StartTrigger.Triggered += OnStartTrigger;
        Boss.Reset();
        Status = BossFightStatus.NotBegun;
    }

    private void OnStartTrigger(Collider2D collider)
    {
        if (!collider.transform.parent.CompareTag("Player"))
        {
            return;
        }

        StartTrigger.Triggered -= OnStartTrigger;
        BeginFight();
    }

    private void BeginFight()
    {
        if (Status != BossFightStatus.NotBegun)
        {
            return;
        }

        Status = BossFightStatus.InProgress;
        foreach (var door in Entry)
        {
            door.Toggle(isOpen: false, duration: 0.5f);
        }
        
        Game.Instance.PlayerFollowCamera.Follow = Center;

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
