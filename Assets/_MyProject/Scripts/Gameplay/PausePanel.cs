using UnityEngine;
using UnityEngine.UI;

public class PausePanel : MonoBehaviour
{
    [SerializeField] Button goHomeButton;
    [SerializeField] Button resetButton;
    [SerializeField] Button continueButton;

    private void OnEnable()
    {
        Time.timeScale = 0;
        goHomeButton.onClick.AddListener(GoHome);
        resetButton.onClick.AddListener(Reset);
        continueButton.onClick.AddListener(Continue);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        goHomeButton.onClick.RemoveListener(GoHome);
        resetButton.onClick.RemoveListener(Reset);
        continueButton.onClick.RemoveListener(Continue);
    }

    void GoHome()
    {
        SceneManager.LoadMainMenu();
    }

    void Continue()
    {
        gameObject.SetActive(false);
    }

    void Reset()
    {
        SceneManager.ResetScene();
    }

    public void Setup()
    {
        gameObject.SetActive(true);
    }

}
