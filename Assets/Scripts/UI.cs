using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public const string TheEndText = "The End";
    public const string PauseText = "Paused";
    public const string DeathText = "Try Again?";

    public event Action Ascended;
    public event Action Escaped;

    public UnityEngine.UI.Button AscendButton;
    public UnityEngine.UI.Button EscapeButton;
    public Canvas Canvas;
    public TMP_Text Header;
    
    public string HeaderText = PauseText;

    private void Awake()
    {
        AscendButton.onClick.AddListener(() => Ascended.Invoke());
        EscapeButton.onClick.AddListener(() => Escaped.Invoke());
    }

    public void Hide()
    {
        Time.timeScale = 1f;
        Canvas.gameObject.SetActive(false);
    }

    public void Show()
    {
        Time.timeScale = 0f;
        Header.text = HeaderText;
        Canvas.gameObject.SetActive(true);
    }
}
