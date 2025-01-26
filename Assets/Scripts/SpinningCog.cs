using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class SpinningCog : MonoBehaviour
{
    public bool isClockwise = true;
    public float delay = 0;
    public float maxSpeed = 360;
    
    private float speed = 0;

    public void StartSpinning()
    {
        DOTween.To(() => speed, x => speed = x, maxSpeed, 1).SetDelay(delay).SetEase(Ease.Linear);
    }

    public void StopSpinning()
    {
        DOTween.To(() => speed, x => speed = x, 0, 1).SetEase(Ease.Linear);
    }

    void Update()
    {
        transform.Rotate(0, 0, isClockwise ? speed * Time.deltaTime : -speed * Time.deltaTime);
    }
}
