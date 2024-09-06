using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ads : MonoBehaviour
{
    private string _adsUnitbnr = "ca-app-pub-3940256099942544/6300978111";
    private string _adsUnitGcs = "ca-app-pub-3940256099942544/1033173712";
    private string _adsUnitOdl = "ca-app-pub-3940256099942544/5224354917";

    BannerView _bannerView;
    InterstitialAd _InterstitialAd;
    RewardedAd _rewardedAd;

    private void Start()
    {
        MobileAds.Initialize(initStatus => { LoadAd(); LoadInterstitialAd(); LoadRewardedAd(); });
    }

    public void LoadAd()
    {
        if(_bannerView == null)
        {
            CreateBannerView();
        }

        var adRequest = new AdRequest();
        _bannerView.LoadAd(adRequest);
    }

    public void CreateBannerView()
    {
        if(_bannerView != null)
        {
            DestroyAd();
        }

        _bannerView = new BannerView(_adsUnitbnr, AdSize.Banner, AdPosition.Bottom);
    }

    public void DestroyAd()
    {
        if (_bannerView != null)
        {

            _bannerView.Destroy();
            _bannerView = null;
        }
    }

    public void LoadInterstitialAd()
    {
        if (_InterstitialAd != null)
        {
            CreateInterstitialView();
        }

        var adRequest = new AdRequest();

        InterstitialAd.Load(_adsUnitGcs,adRequest,(InterstitialAd ad, LoadAdError error) =>
        {
            if(error != null || ad == null)
            {
                Debug.Log("Geçiþ reklamý yüklenmesi durumunda hata oluþtu." + error);
                return;
            }
            _InterstitialAd = ad;
        });
    }

    public void CreateInterstitialView()
    {
        if (_InterstitialAd != null && _InterstitialAd.CanShowAd())
        {
            _InterstitialAd.Show();
        }
        else {
            Debug.Log("Geçiþ reklamý hazýr deðil");
        }
    }

    public void LoadRewardedAd()
    {
        if(_rewardedAd != null)
        {
            _rewardedAd.Destroy();
            _rewardedAd = null;
        }

        var Adrequest = new AdRequest();

        RewardedAd.Load(_adsUnitOdl,Adrequest,(RewardedAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                Debug.Log("Ödül reklamý yüklenmesi durumunda hata oluþtu." + error);
                return;
            }
            _rewardedAd= ad;
        });
    }

    public void ShowRewardedAd()
    {
        const string rewerdMsg = "Ödüllü reklam kullanýcýya gönderildi. tür: {0}, miktar: {1}";

        if(_rewardedAd != null && _rewardedAd.CanShowAd())
        {
            _rewardedAd.Show((Reward reward) =>
            {
                Debug.Log(string.Format(rewerdMsg, reward.Type, reward.Amount));
            });
        }
        else
        {
            Debug.Log("Ödüllü reklam hazýr deðil");
        }
    }
}
