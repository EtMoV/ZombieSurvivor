using GoogleMobileAds.Api;
using UnityEngine;

public class AdmobManager : MonoBehaviour
{
    private string rewardedAdUnitId = "ca-app-pub-5469928880838084/4215421994";
    private string interstitialAdUnitId = "ca-app-pub-5469928880838084/2024721979";
    private RewardedAd rewardedAd;
    private InterstitialAd interstitialAd;

    void Start()
    {
        MobileAds.Initialize((InitializationStatus initstatus) =>
        {
            if (initstatus == null)
            {
                Debug.LogError("Google Mobile Ads initialization failed.");
                return;
            }

            Debug.Log("Google Mobile Ads initialization complete.");
            LoadRewardAd();
            LoadInterstitialAd();
        });

    }

    public void showRewardedAd()
    {
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                Debug.Log("User earned reward of " + reward.Amount + " " + reward.Type);

            });
        }
    }

    public void showInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            interstitialAd.Show();
        }
    }

    private void LoadRewardAd()
    {
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        RewardedAd.Load(rewardedAdUnitId, adRequest, (RewardedAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                // The ad failed to load.
                Debug.LogError("Google Mobile Ads loading reward failed.");
                return;
            }
            // The ad loaded successfully.
            Debug.Log("Google Mobile Ads loading reward complete.");
            rewardedAd = ad;
            rewardedAd.OnAdFullScreenContentClosed += () =>
            {
                LoadRewardAd();
            };
            rewardedAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("OnAdFullScreenContentFailed: " + error.GetCause());
                LoadRewardAd();
            };
        });
    }

    private void LoadInterstitialAd()
    {
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }

        // Create our request used to load the ad.
        var adRequest = new AdRequest();

        // Send the request to load the ad.
        InterstitialAd.Load(interstitialAdUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null)
            {
                // The ad failed to load.
                Debug.LogError("Google Mobile Ads loading interstitial failed.");
                return;
            }
            // The ad loaded successfully.
            Debug.Log("Google Mobile Ads loading interstitial complete.");
            interstitialAd = ad;
            interstitialAd.OnAdFullScreenContentClosed += () =>
            {
                LoadRewardAd();
            };
            interstitialAd.OnAdFullScreenContentFailed += (AdError error) =>
            {
                Debug.LogError("OnAdFullScreenContentFailed: " + error.GetCause());
                LoadRewardAd();
            };
        });
    }
}
