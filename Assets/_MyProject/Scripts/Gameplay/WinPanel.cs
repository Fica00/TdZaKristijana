using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsDisplay;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] Button homeButton;
    [SerializeField] Button loadNextLeve;

    public void Setup()
    {
        AdsManager.Instance.ShowInterstitialAd();
        Time.timeScale = 0;
        coinsDisplay.text = GameplayManager.Instance.Money.ToString();
        scoreDisplay.text = GameplayManager.Instance.Score.ToString();
        homeButton.onClick.AddListener(GoHome);
        loadNextLeve.onClick.AddListener(LoadNextLevel);
        gameObject.SetActive(true);

        DataManager.Instance.PlayerData.Gold += GameplayManager.Instance.Money;
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        homeButton.onClick.RemoveListener(GoHome);
        loadNextLeve.onClick.RemoveListener(LoadNextLevel);
    }

    void GoHome()
    {
        SceneManager.LoadMainMenu();
    }

    void LoadNextLevel()
    {
        LevelManager.Instance.SelectedLevel = LevelManager.Instance.Get(LevelManager.Instance.SelectedLevel.Id + 1);
        SceneManager.LoadGameplay();
    }
}
