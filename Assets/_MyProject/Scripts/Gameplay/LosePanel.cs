using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class LosePanel : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsDisplay;
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] Button homeButton;
    [SerializeField] Button adButton;

    public static Action PlayerReviewd;

    public void Setup()
    {
        AdsManager.Instance.ShowInterstitialAd();
        Time.timeScale = 0;
        coinsDisplay.text = GameplayManager.Instance.Money.ToString();
        scoreDisplay.text = GameplayManager.Instance.Score.ToString();
        homeButton.onClick.AddListener(GoHome);
        adButton.onClick.AddListener(WatchAd);
        gameObject.SetActive(true);

        DataManager.Instance.PlayerData.Gold += GameplayManager.Instance.Money;
        AdsManager.RewardAdWatched += Review;
        AudioManager.Instance.PlaySoundEffect(AudioManager.LOSE);
    }

    private void OnDisable()
    {
        Time.timeScale = 1;
        homeButton.onClick.RemoveListener(GoHome);
        adButton.onClick.RemoveListener(WatchAd);
        AdsManager.RewardAdWatched -= Review;
    }

    void WatchAd()
    {
        AdsManager.Instance.ShowRewardedAd();
    }

    void Review()
    {
        PlayerReviewd?.Invoke();
        gameObject.SetActive(false);
    }

    void GoHome()
    {
        SceneManager.LoadMainMenu();
    }
}
