using UnityEngine;
using Unity.Services.Core;
using Unity.Services.LevelPlay;

public class AdsManager : MonoBehaviour
{
    private LevelPlayRewardedAd rewardedAd;
    private LevelPlayInterstitialAd interstitialAd;

    private string rewardedPlacement = "bqpd3v72q9ynn8xc"; // bqpd3v72q9ynn8xc
    private string interstitialPlacement = "ql2lyyucqwkxgssy"; // ql2lyyucqwkxgssy

    private bool sdkInitialized = false; // Flag pour savoir si LevelPlay est prêt

    async void Start()
    {
        // Initialise Unity Services (Analytics, Remote Config, etc.)
        await UnityServices.InitializeAsync();

        // Register initialization callbacks pour LevelPlay
        LevelPlay.OnInitSuccess += SdkInitializationCompletedEvent;
        LevelPlay.OnInitFailed += SdkInitializationFailedEvent;

        // Initialise le SDK LevelPlay (nécessaire avant de charger les ads)
        LevelPlay.Init("24922f83d");
    }

    // ------------------ Callbacks SDK ------------------

    private void SdkInitializationCompletedEvent(LevelPlayConfiguration config)
    {
        Debug.Log("LevelPlay SDK initialized successfully");
        sdkInitialized = true;
        // Maintenant que le SDK est prêt, créer et charger les ads
        InitializeRewardedAd();
        InitializeInterstitialAd();
    }

    private void SdkInitializationFailedEvent(LevelPlayInitError error)
    {
        Debug.LogError($"LevelPlay SDK failed to initialize: {error}");
    }



    // ------------------ Rewarded Ad ------------------

    private void InitializeRewardedAd()
    {
        rewardedAd = new LevelPlayRewardedAd(rewardedPlacement);

        rewardedAd.OnAdLoaded += (info) =>
        {
            Debug.Log("Rewarded ad loaded");
        };

        rewardedAd.OnAdLoadFailed += (error) =>
        {
            Debug.LogError($"Rewarded ad failed to load: {error} ({error})");
        };

        rewardedAd.OnAdRewarded += (info, reward) =>
        {
            Debug.Log($"Reward granted: {reward.Name} x{reward.Amount}");
            // Donne la récompense au joueur ici
        };

        rewardedAd.OnAdClosed += (info) =>
        {
            Debug.Log("Rewarded ad closed, reloading...");
            rewardedAd.LoadAd();
        };

        rewardedAd.LoadAd();
    }

    public void ShowRewarded()
    {
        if (!sdkInitialized)
        {
            Debug.LogWarning("SDK not initialized yet!");
            return;
        }

        if (rewardedAd.IsAdReady() && !LevelPlayRewardedAd.IsPlacementCapped(rewardedPlacement))
        {
            rewardedAd.ShowAd();
            Debug.Log("Showing rewarded ad...");
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready or placement capped!");
        }
    }

    // ------------------ Interstitial Ad ------------------

    private void InitializeInterstitialAd()
    {
        interstitialAd = new LevelPlayInterstitialAd(interstitialPlacement);

        interstitialAd.OnAdLoaded += (info) =>
        {
            Debug.Log("Interstitial ad loaded");
        };

        interstitialAd.OnAdLoadFailed += (error) =>
        {
            Debug.LogError($"Interstitial ad failed to load: {error} ({error})");
        };

        interstitialAd.OnAdClosed += (info) =>
        {
            Debug.Log("Interstitial ad closed, reloading...");
            interstitialAd.LoadAd();
        };

        interstitialAd.LoadAd();
    }

    public void ShowInterstitial()
    {
        if (!sdkInitialized)
        {
            Debug.LogWarning("SDK not initialized yet!");
            return;
        }

        if (interstitialAd.IsAdReady())
        {
            interstitialAd.ShowAd();
            Debug.Log("Showing interstitial ad...");
        }
        else
        {
            Debug.LogWarning("Interstitial ad not ready!");
        }
    }
}
