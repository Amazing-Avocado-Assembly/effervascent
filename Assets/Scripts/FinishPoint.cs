using System;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public event Action Finished;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Finished.Invoke();
        }
    }
}
