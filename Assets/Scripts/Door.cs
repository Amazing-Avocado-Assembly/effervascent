using System.Linq;
using DG.Tweening;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform[] SlidingObjects;
    public bool IsInitiallyOpen;

    public bool IsOpen { get; set; }

    private Vector3[] closedPositions;

    private void Start()
    {
        closedPositions = SlidingObjects.Select(s => s.localPosition).ToArray();
        Toggle(IsInitiallyOpen, animated: false);
    }

    public void Toggle(bool? isOpen = null, bool animated = true, float duration = 2f)
    {
        if (isOpen != null)
        {
            if (IsOpen == isOpen)
            {
                return;
            }

            IsOpen = isOpen.Value;
        }
        else
        {
            IsOpen = !IsOpen;
        }

        for (int i = 0; i < SlidingObjects.Length; ++i)
        {
            var door = SlidingObjects[i];

            var targetPosition = IsOpen ? Vector3.zero : closedPositions[i];
            if (animated)
            {
                door.DOLocalMove(targetPosition, duration).SetEase(Ease.InOutCubic);
                if (door.TryGetComponent<AudioSource>(out var audioSource))
                {
                    audioSource.Play();
                }
            }
            else
            {
                door.localPosition = targetPosition;
            }
        }
    }
}
