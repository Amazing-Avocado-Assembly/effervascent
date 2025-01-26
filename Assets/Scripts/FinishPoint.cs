using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    public event Action Finished;
    public SpinningCog[] cogs;
    public ParticleSystem[] particles;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = Game.Instance.Player;
            player.RB.bodyType = RigidbodyType2D.Static;
            player.VictoryPop();
            player.Bubble.GetComponentInChildren<SpriteRenderer>().enabled = false;

            Cog cog = Game.Instance.RespawnPoint.cog;
            // Detach the cog from the player
            cog.transform.SetParent(null);
            cog.GetComponentInChildren<SpriteRenderer>().sortingOrder = 20;
            SpinningCog spinningCog = cog.AddComponent<SpinningCog>();
            spinningCog.isClockwise = true;
            spinningCog.maxSpeed = 360;
            spinningCog.delay = 0;

            // Move to the position of parent
            player.transform.DOMove(transform.parent.position + new Vector3(0, 2, 0), 3.0f).SetEase(Ease.InOutQuad).SetDelay(1.0f);
            cog.transform.DOScale(3.0f, 3.0f).SetEase(Ease.InOutQuad).SetDelay(1.0f);
            cog.transform.DOMove(transform.parent.position, 3.0f).SetEase(Ease.InOutQuad).SetDelay(1.0f).onComplete += () =>
            {
                spinningCog.StartSpinning();

                foreach (SpinningCog cog in cogs)
                {
                    cog.StartSpinning();
                }

                DOTween.To(() => 0, x => { }, 0, /* TODO: delay: */ 3).onComplete += () => {
                    foreach (ParticleSystem particle in particles)
                    {
                        particle.Play();
                    }

                    DOTween.To(() => 0, x => { }, 0, /* TODO: delay: */ 5).onComplete += () => {
                        Finished.Invoke();
                    };
                };
            };
        }
    }
}
