using System;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Bubble Bubble { get; private set; }

    void OnEnable()
    {
        Bubble = GetComponentInChildren<Bubble>();    
    }

    void Update()
    {
        
    }
}
