using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] Button playButton;
    [SerializeField] Button shopButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(ShowLevelSelection);
        shopButton.onClick.AddListener(ShowShop);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(ShowLevelSelection);
        shopButton.onClick.RemoveListener(ShowShop);
    }

    void ShowLevelSelection()
    {
        SceneManager.LoadLevelSelection();
    }

    void ShowShop()
    {
        SceneManager.LoadShop();
    }
}
