using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class InterstitialManager : MonoBehaviour
{
    private const string AD_ID = "YOUR_INTERSTITIAL_AD_UNIT_ID"; // ← 换成后台 ID

    private static TTInterstitialAd _ad;
    private static bool _loading;
    private static readonly DateTime _launchTime = DateTime.Now;
    private static DateTime _lastShowTime;
    private static DateTime _lastRewardTime;

    /* ---------- 对外接口 ---------- */
    public static void Init() => CreateAndPreload();

    public static void OnRewardVideoClose() => _lastRewardTime = DateTime.Now;

    public static void Show()
    {
        if (!CanShow()) return;

        if (_ad == null)
        {
            CreateAndPreload();
            return;
        }

        if (_ad.IsLoaded())
        {
            _ad.Show();
        }
        else if (!_loading)
        {
            _ad.Load();
            _loading = true;
        }
    }

    /* ---------- 内部 ---------- */
    private static void CreateAndPreload()
    {
        _ad = TT.CreateInterstitialAd(new CreateInterstitialAdParam
        {
            InterstitialAdId = AD_ID
        });

        _ad.OnLoad += () =>
        {
            Debug.Log("插屏广告预加载完成");
            _loading = false;
        };

        _ad.OnClose += () =>
        {
            _lastShowTime = DateTime.Now;
            CreateAndPreload();   // 预加载下一条
        };

        _ad.OnError += (errCode, errMsg) =>
        {
            Debug.LogError($"插屏广告错误：{errCode} - {errMsg}");
            _loading = false;
        };

        _ad.Load();
        _loading = true;
    }

    private static bool CanShow()
    {
        double sinceLaunch = (DateTime.Now - _launchTime).TotalSeconds;
        double sinceShow = (DateTime.Now - _lastShowTime).TotalSeconds;
        double sinceReward = (DateTime.Now - _lastRewardTime).TotalSeconds;

        return sinceLaunch >= 30 && sinceShow >= 60 && sinceReward >= 60;
    }
}
