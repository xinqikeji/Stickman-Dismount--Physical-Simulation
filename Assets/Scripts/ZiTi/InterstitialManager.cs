using System;
using System.Collections;
using System.Collections.Generic;
using TTSDK;
using UnityEngine;

public class InterstitialManager : MonoBehaviour
{
    private const string AD_ID = "YOUR_INTERSTITIAL_AD_UNIT_ID"; // �� ���ɺ�̨ ID

    private static TTInterstitialAd _ad;
    private static bool _loading;
    private static readonly DateTime _launchTime = DateTime.Now;
    private static DateTime _lastShowTime;
    private static DateTime _lastRewardTime;

    /* ---------- ����ӿ� ---------- */
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

    /* ---------- �ڲ� ---------- */
    private static void CreateAndPreload()
    {
        _ad = TT.CreateInterstitialAd(new CreateInterstitialAdParam
        {
            InterstitialAdId = AD_ID
        });

        _ad.OnLoad += () =>
        {
            Debug.Log("�������Ԥ�������");
            _loading = false;
        };

        _ad.OnClose += () =>
        {
            _lastShowTime = DateTime.Now;
            CreateAndPreload();   // Ԥ������һ��
        };

        _ad.OnError += (errCode, errMsg) =>
        {
            Debug.LogError($"����������{errCode} - {errMsg}");
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
