using UnityEngine;
using Unity.Services.Core;
using Unity.Services.LevelPlay;

public class AdsManager : MonoBehaviour
{
    private ILevelPlayRewardedAd rewardedAd;
    private ILevelPlayInterstitialAd interstitialAd;

    public string rewardedPlacement = "Rewarded_Android";
    public string interstitialPlacement = "Interstitial_Android";

    async void Start()
    {
        // Initialise Unity Services (nécessaire avant LevelPlay)
        await UnityServices.InitializeAsync();

        // Rewarded Ad
        rewardedAd = new LevelPlayRewardedAd(rewardedPlacement);
        rewardedAd.OnAdLoaded += OnRewardedLoaded;
        rewardedAd.OnAdRewarded += OnRewardedReward;
        rewardedAd.OnAdClosed += (info) => rewardedAd.LoadAd();

        rewardedAd.LoadAd();

        // Interstitial Ad
        interstitialAd = new LevelPlayInterstitialAd(interstitialPlacement);
        interstitialAd.OnAdLoaded += OnInterstitialLoaded;
        interstitialAd.OnAdClosed += (info) => interstitialAd.LoadAd();

        interstitialAd.LoadAd();
    }

    void OnRewardedLoaded(LevelPlayAdInfo info)
    {
        Debug.Log("Rewarded loaded");
    }

    void OnRewardedReward(LevelPlayAdInfo info, LevelPlayReward reward)
    {
        Debug.Log($"Rewarded! {reward.Name} x{reward.Amount}");
        // Donne la récompense ici
    }

    void OnInterstitialLoaded(LevelPlayAdInfo info)
    {
        Debug.Log("Interstitial loaded");
    }

    public void ShowRewarded()
    {
        if (rewardedAd.IsAdReady() && !LevelPlayRewardedAd.IsPlacementCapped(rewardedPlacement))
        {
            rewardedAd.ShowAd(rewardedPlacement);
        }
    }

    public void ShowInterstitial()
    {
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd(interstitialPlacement);
        }
    }
}
