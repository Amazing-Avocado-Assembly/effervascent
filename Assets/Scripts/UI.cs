using System;
using TMPro;
using UnityEngine;

public enum UIMode {
    Start,
    Pause,
    End
}

public class UI : MonoBehaviour
{
    public const string StartText = "Unnamed Bubble Game";
    public const string StartAscendButtonText = "Ascend";
    public const string EndText = "The End";
    public const string EndAscendButtonText = "Ascend Again";
    public const string PauseText = "Paused";

    public event Action Ascended;
    public event Action Escaped;

    public UnityEngine.UI.Button AscendButton;
    public UnityEngine.UI.Button EscapeButton;
    public Canvas Canvas;
    public TMP_Text Header;
    public TMP_Text TutorialBit;
    public TMP_Text AscendButtonText;

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

    public void Show(UIMode mode = UIMode.Pause)
    {
        Time.timeScale = 0f;

        switch(mode) {
            case UIMode.Start:
                Header.text = StartText;
                AscendButtonText.text = StartAscendButtonText;
                TutorialBit.gameObject.SetActive(true);
                break;
            case UIMode.Pause:
                Header.text = PauseText;
                AscendButtonText.text = EndAscendButtonText;
                TutorialBit.gameObject.SetActive(false);
                break;
            case UIMode.End:
                Header.text = EndText;
                AscendButtonText.text = EndAscendButtonText;
                TutorialBit.gameObject.SetActive(false);
                break;
        }

        Canvas.gameObject.SetActive(true);
    }
}
