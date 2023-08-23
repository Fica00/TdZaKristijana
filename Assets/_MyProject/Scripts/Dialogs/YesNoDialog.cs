using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;

public class YesNoDialog : MonoBehaviour
{
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] TextMeshProUGUI message;

    [HideInInspector] public UnityEvent YesButtonPressed;
    [HideInInspector] public UnityEvent NoButtonPressed;

    public void Show(string _message)
    {
        message.text = _message;
        gameObject.SetActive(true);
        yesButton.onClick.AddListener(Yes);
        noButton.onClick.AddListener(No);
    }

    void Yes()
    {
        YesButtonPressed?.Invoke();
        Close();
    }

    void No()
    {
        NoButtonPressed?.Invoke();
        Close();
    }

    void Close()
    {
        YesButtonPressed.RemoveAllListeners();
        NoButtonPressed.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
