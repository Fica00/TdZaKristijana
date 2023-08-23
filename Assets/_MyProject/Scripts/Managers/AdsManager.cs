using GoogleMobileAds.Api;
using System;
using UnityEngine;

public class AdsManager : MonoBehaviour
{
    public static AdsManager Instance;
    private InterstitialAd interstitial;
    private RewardedAd rewardedAd;
    private string interstitialAdUnitId;
    private string rewardedAdUnitId;

    public static Action RewardAdWatched;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SetIds();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void SetIds()
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            rewardedAdUnitId = "ca-app-pub-3940256099942544/4411468910";
        }
        else if (Application.platform == RuntimePlatform.OSXEditor)
        {
            interstitialAdUnitId = "ca-app-pub-3940256099942544/1033173712";
            rewardedAdUnitId = "ca-app-pub-3940256099942544/1712485313";
        }
        else if (Application.platform == RuntimePlatform.Android)
        {
            interstitialAdUnitId = "ca-app-pub-2080249310671832/6416888784";
            rewardedAdUnitId = "ca-app-pub-2080249310671832/1356133790";
        }
        else
        {
            interstitialAdUnitId = "ca-app-pub-2080249310671832/5659190948";
            rewardedAdUnitId = "ca-app-pub-2080249310671832/5850762631";
        }
    }

    void Start()
    {
        // Initialize the interstitial ad
        this.interstitial = new InterstitialAd(interstitialAdUnitId);
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        // Initialize the rewarded ad
        this.rewardedAd = new RewardedAd(rewardedAdUnitId);
        this.rewardedAd.OnUserEarnedReward += HandleRewardEarned;
        this.rewardedAd.OnAdClosed += HandleOnAdRewardedClosed;

        // Request an interstitial ad
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);

        // Request a rewarded ad
        AdRequest request2 = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request2);
    }

    public void ShowInterstitialAd()
    {
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
        }
    }

    public void ShowRewardedAd()
    {
        if (this.rewardedAd.IsLoaded())
        {
            this.rewardedAd.Show();
        }
        else
        {
            UIManager.Instance.OkDialog.Show("Ad is not ready");
        }
    }

    public void HandleRewardEarned(object sender, Reward args)
    {
        RewardAdWatched?.Invoke();
    }

    public void HandleOnAdClosed(object sender, System.EventArgs args)
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.interstitial.LoadAd(request);
    }

    void HandleOnAdRewardedClosed(object sender, System.EventArgs args)
    {
        AdRequest request = new AdRequest.Builder().Build();
        this.rewardedAd.LoadAd(request);
    }
}
