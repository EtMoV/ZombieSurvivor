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

        // ------------------ Rewarded Ad ------------------
        rewardedAd = new LevelPlayRewardedAd(rewardedPlacement);

        // Callbacks
        rewardedAd.OnAdLoaded += (info) => Debug.Log("Rewarded ad loaded");
        rewardedAd.OnAdRewarded += OnRewardedReward;
        rewardedAd.OnAdClosed += (info) =>
        {
            Debug.Log("Rewarded ad closed, reloading...");
            rewardedAd.LoadAd();
        };

        // Charge l'ad
        rewardedAd.LoadAd();

        // ------------------ Interstitial Ad ------------------
        interstitialAd = new LevelPlayInterstitialAd(interstitialPlacement);

        // Callbacks
        interstitialAd.OnAdLoaded += (info) => Debug.Log("Interstitial ad loaded");
        interstitialAd.OnAdClosed += (info) =>
        {
            Debug.Log("Interstitial ad closed, reloading...");
            interstitialAd.LoadAd();
        };

        // Charge l'ad
        interstitialAd.LoadAd();
    }

    // ------------------ Rewarded Callbacks ------------------
    void OnRewardedReward(LevelPlayAdInfo info, LevelPlayReward reward)
    {
        Debug.Log($"Reward granted: {reward.Name} x{reward.Amount}");
        // Donne la récompense au joueur ici
    }

    // ------------------ Public Methods ------------------
    public void ShowRewarded()
    {
        Debug.Log($"IsAdReady: {rewardedAd.IsAdReady()}, IsPlacementCapped: {LevelPlayRewardedAd.IsPlacementCapped(rewardedPlacement)}");

        if (rewardedAd.IsAdReady() && !LevelPlayRewardedAd.IsPlacementCapped(rewardedPlacement))
        {
            rewardedAd.ShowAd(); // ✅ ne passe pas le placement ici
            Debug.Log("Showing rewarded ad...");
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready or placement capped!");
        }
    }

    public void ShowInterstitial()
    {
        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd(); // ✅ ne passe pas le placement ici
            Debug.Log("Showing interstitial ad...");
        }
        else
        {
            Debug.LogWarning("Interstitial ad not ready!");
        }
    }
}
