using System;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public event Action Ascended;
    public event Action Escaped;

    public UnityEngine.UI.Button AscendButton;
    public UnityEngine.UI.Button EscapeButton;
    public Canvas Canvas;

    private void Awake()
    {
        AscendButton.onClick.AddListener(() => Ascended.Invoke());
        EscapeButton.onClick.AddListener(() => Escaped.Invoke());
    }

    public void Hide()
    {
        Canvas.gameObject.SetActive(false);
    }

    public void Show()
    {
        Canvas.gameObject.SetActive(true);
    }
}
