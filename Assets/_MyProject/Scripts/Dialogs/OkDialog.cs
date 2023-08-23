using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class OkDialog : MonoBehaviour
{
    [SerializeField] Button okButton;
    [SerializeField] TextMeshProUGUI message;

    [HideInInspector] public UnityEvent OkPressed;

    public void Show(string _message)
    {
        message.text = _message;
        gameObject.SetActive(true);
        okButton.onClick.AddListener(Close);
    }

    private void OnDisable()
    {
        okButton.onClick.RemoveListener(Close);
    }

    void Close()
    {
        OkPressed?.Invoke();
        OkPressed.RemoveAllListeners();
        gameObject.SetActive(false);
    }
}
