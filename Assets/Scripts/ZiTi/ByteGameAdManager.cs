using UnityEngine;
using StarkSDKSpace;
using TTSDK;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine.SceneManagement;
using static TTSDK.TTAppLifeCycle;

using System.Reflection;
using UnityEngine.Networking;




public class ByteGameAdManager : MonoBehaviour
{
    

    // ���ID�洢��List�У��������˳��
    public List<string> RewardedAdIds = new List<string>();
    //{
    //    "mi5ag76n0bg4506577", // LEVEL_VIDEO_ID
    //    "your_bonus_video_id_here", // BONUS_VIDEO_ID
    //    "your_special_video_id_here" // SPECIAL_VIDEO_ID
    //};

    // �������ID
    public const string INTERSTITIAL_ID = "2efdh3aelb10i0q2ie";
    // ������ID
    public const string BANNER_ID = "2efdh3aelb10i0q2ie";
    public TTRewardedVideoAd tTRewardedVideoAd;
    // ���ʵ���洢
    private TTInterstitialAd _interstitialAd;
    private TTBannerAd _bannerAd;

    //// �������ʵ���洢��ʹ���ֵ�洢���ʵ����
    //public Dictionary<string, TTRewardedVideoAd> _rewardedAds = new Dictionary<string, TTRewardedVideoAd>();
    public int count = 0;
    // ��ǰ���״̬
    private bool _isRewardAdLoading;
    private bool _isInterstitialAdLoading = false;

    //public static ByteGameAdManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            //_instance = new GameObject("ByteGameAdManager").AddComponent<ByteGameAdManager>();
    //            //DontDestroyOnLoad(_instance.gameObject);
    //        }
    //        return _instance;
    //    }
    //}
    public void Update()
    {
    }


    private void Awake()
    {
        //if (_instance == null)
        //{
        //    _instance = this;
        //    //DontDestroyOnLoad(gameObject);
        //}

        TT.InitSDK((code, env) =>
        {
            Debug.Log($"SDK��ʼ����ɣ�������{env.m_HostEnum}");

            // Ԥ�������м������
            //foreach (var adId in RewardedAdIds)
            //{
            //    PreloadRewardAd(adId);
            //}

            // Ԥ���ز������
           // PreloadInterstitialAd();
        });
        TT.InitSDK((code, env) =>
        { });
        TT.CheckScene(TTSideBar.SceneEnum.SideBar, b =>
        {
            Debug.Log("check scene success��" + b);
            
        }, () =>
        {
            Debug.Log("check scene complete");
        }, (errCode, errMsg) =>
        {
            Debug.Log($"check scene error, errCode:{errCode}, errMsg:{errMsg}");
        });
        TestAppLifeCycle();
#if UNITY_EDITOR
        MockSetting.OpenAllMockModule();
#endif
    }
   
    private void OnShowCallback(Dictionary<string, object> data)
    {
        Debug.Log("OnShow callback triggered with data:");

        // �����ֵ䲢��ӡ���м�ֵ��
        foreach (var kvp in data)
        {
            Debug.Log($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        // ����ָ�� key ��ȡָ������
        if (data.ContainsKey("scene"))
        {
            Debug.Log("Scene: " + data["scene"]);
        }

        if (data.ContainsKey("launch_from"))
        {
            Debug.Log("Launch From: " + data["launch_from"]);
        }
        PlayerPrefs.SetString("Launch From", data["launch_from"].ToString());
        if (data.ContainsKey("location"))
        {
            Debug.Log("Location: " + data["location"]);
        }
        PlayerPrefs.SetString("location", data["location"].ToString());
        // �ж�С��Ϸ�û��Ƿ�ͨ���������ڳ�������
        if (data.ContainsKey("scene") && data["scene"] == "sidebar" &&
            data.ContainsKey("launch_from") && data["launch_from"] == "homepage" &&
            data.ContainsKey("location") && data["location"] == "sidebar_card")
        {
            Debug.Log("С��Ϸ�û�ͨ���������ڳ�������");
            // �������������߼�
        }
    }
    public TTAppLifeCycle lifeCycle;
    public void TestAppLifeCycle()
    {
        Dictionary<string, object> param = new Dictionary<string, object>
        {
            { "scene", "sidebar" },
            { "launch_from", "homepage" },
            { "location", "sidebar_card" }
        };
        lifeCycle = TT.GetAppLifeCycle();
        lifeCycle.OnShow += OnShowCallback;
        
        Debug.Log(1111111);
        
        MethodInfo methodInfo = typeof(TTAppLifeCycle).GetMethod("OnShowCall", BindingFlags.NonPublic | BindingFlags.Instance);
        methodInfo?.Invoke(lifeCycle, new object[] { param });
        //TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        //Debug.Log("path :" + launchOption.Path);
        //Debug.Log("scene :" + launchOption.Scene);
        //Debug.Log("subScene :" + launchOption.SubScene);
        //Debug.Log("group_id :" + launchOption.GroupId);
        //Debug.Log("shareTicket :" + launchOption.ShareTicket);
        //Debug.Log("is_sticky :" + launchOption.IsSticky);
        //Debug.Log("query :");
        //if (launchOption.Query != null)
        //{
        //    foreach (KeyValuePair<string, string> kv in launchOption.Query)
        //        if (kv.Value != null)
        //            Debug.Log(kv.Key + ": " + kv.Value);
        //        else
        //            Debug.Log(kv.Key + ": " + "null ");
        //}

        //Debug.Log("refererInfo :");
        //if (launchOption.RefererInfo != null)
        //{
        //    foreach (KeyValuePair<string, string> kv in launchOption.RefererInfo)
        //        if (kv.Value != null)
        //            Debug.Log(kv.Key + ": " + kv.Value);
        //        else
        //            Debug.Log(kv.Key + ": " + "null ");
        //}

        //Debug.Log("extra :");
        //if (launchOption.Extra != null)
        //{
        //    foreach (KeyValuePair<string, string> kv in launchOption.Extra)
        //        if (kv.Value != null)
        //            Debug.Log(kv.Key + ": " + kv.Value);
        //        else
        //            Debug.Log(kv.Key + ": " + "null ");
        //}

    }

    private void OnAppShow(Dictionary<string, object> param)
    {
        Debug.LogError(11111111111); 
        Debug.Log(param.Keys);
    }
    public void OnDisable()
    {
    }
    #region �������
    public void PlayRewardedAd(string adId,
    Action<bool, int> onClose = null,
    Action<int, string> onError = null)
    {
        tTRewardedVideoAd = TT.CreateRewardedVideoAd(adId,
            (isValid, duration) =>
            {
                onClose?.Invoke(isValid, duration);

            },
            (errCode, errMsg) =>
            {
                onError?.Invoke(errCode, errMsg);

            });
        tTRewardedVideoAd.Show();
      
    }
    // ����ָ��ID�ļ������
    //public void PlayRewardedAd(string adId,
    //    Action<bool, int> onClose = null,
    //    Action<int, string> onError = null)
    //{
    //    if (!_rewardedAds.ContainsKey(adId))
    //    {

    //        CreateRewardedAd(adId, onClose, onError);
    //        return;
    //    }

    //    var rewardedAd = _rewardedAds[adId];
    //    if (rewardedAd != null )
    //    {
    //        rewardedAd.Show();
    //        rewardedAd.OnClose += (ended, count) =>
    //        {
    //            onClose?.Invoke(ended, count); 
    //            Debug.Log($"������Ƶ�ر� ended: {ended}, count: {count}");
    //        };
    //    }
    //    else
    //    {
    //        if (!_isRewardAdLoading)
    //        {
    //            rewardedAd.Load();
    //            _isRewardAdLoading = true;
    //        }
    //    }
    //}

    //// Ԥ����ָ��ID�ļ������
    //private void PreloadRewardAd(string adId)
    //{

    //    if (!_rewardedAds.ContainsKey(adId))
    //    {
    //        CreateRewardedAd(adId);
    //    }
    //    else
    //    {
    //        _rewardedAds[adId].Load();
    //    }
    //}

    //// ����ָ��ID�ļ������
    //private void CreateRewardedAd(string adId,
    //    Action<bool, int> onClose = null,
    //    Action<int, string> onError = null)
    //{
    //    var newAd = TT.CreateRewardedVideoAd(adId,
    //        (isValid, duration) =>
    //        {
    //            onClose?.Invoke(isValid, duration);
    //            PreloadRewardAd(adId); // �Զ����¼���
    //        },
    //        (errCode, errMsg) =>
    //        {
    //            onError?.Invoke(errCode, errMsg);
    //            _isRewardAdLoading = false;
    //        });

    //    newAd.OnLoad += () =>
    //    {
    //        Debug.Log($"������棨ID: {adId}��Ԥ�������");
    //        _isRewardAdLoading = false;
    //    };
    //    Debug.Log(_rewardedAds.Count);
    //    _rewardedAds.Add(adId, newAd);
    //    _rewardedAds[adId] = newAd; // ���´����Ĺ��ʵ���洢���ֵ���

    //}
    #endregion

    #region �������
    public void PlayInterstitialAd(
        Action onClose = null,
        Action<int, string> onError = null)
    {
        if (_interstitialAd == null)
        {
            CreateInterstitialAd(onClose, onError);
            return;
        }

        if (_interstitialAd.IsLoaded())
        {
            _interstitialAd.Show();
        }
        else
        {
            if (!_isInterstitialAdLoading)
            {
                _interstitialAd.Load();
                _isInterstitialAdLoading = true;
            }
        }
    }

    private void PreloadInterstitialAd()
    {
        if (_interstitialAd == null)
        {
            CreateInterstitialAd();
        }
        else
        {
            _interstitialAd.Load();
        }
    }

    private void CreateInterstitialAd(
        Action onClose = null,
        Action<int, string> onError = null)
    {
        _interstitialAd = TT.CreateInterstitialAd(new CreateInterstitialAdParam
        {
            InterstitialAdId = INTERSTITIAL_ID
        });

        _interstitialAd.OnLoad += () =>
        {
            Debug.Log("�������Ԥ�������");
            _isInterstitialAdLoading = false;
        };

        _interstitialAd.OnClose += () =>
        {
            onClose?.Invoke();
            PreloadInterstitialAd();
        };

        _interstitialAd.OnError += (errCode, errMsg) =>
        {
            onError?.Invoke(errCode, errMsg);
            _isInterstitialAdLoading = false;
        };
    }
    #endregion

    #region ������
    public void ShowBanner(
        Action onShow = null,
        Action<int, string> onError = null)
    {
        if (_bannerAd == null)
        {
            CreateBannerAd(onShow, onError);
        }

        if (_bannerAd != null)
        {
            _bannerAd.Show();
        }
    }

    public void HideBanner()
    {
        if (_bannerAd != null)
        {
            _bannerAd.Hide();
        }
    }

    private void CreateBannerAd(
        Action onShow = null,
        Action<int, string> onError = null)
    {
        var style = new TTBannerStyle();
        style.top = 0;
        style.left = 0;
        style.width = 100;
        _bannerAd = TT.CreateBannerAd(new CreateBannerAdParam
        {
            BannerAdId = BANNER_ID,
            Style = style,
            AdIntervals = 1
        });

        _bannerAd.OnLoad += () =>
        {
            Debug.Log("������������");
            onShow?.Invoke();
        };

        _bannerAd.OnError += (errCode, errMsg) =>
        {
            onError?.Invoke(errCode, errMsg);
        };
        _bannerAd.Show();
    } 
    #endregion
}



// �������
//byteGameAdManager.PlayRewardedAd("mi5ag76n0bg4506577",
//            (isValid, duration) =>
//            {
//                //isValid����Ƿ񲥷��꣬������Ϸ�߼������²���
//                Debug.LogError(0);
//                if (!isValid)
//                {
//                    


//                }


//            },
//            (errCode, errMsg) =>
//            {
//                Debug.LogError(1);
//            });

//// �������
//ByteGameAdManager.Instance.PlayInterstitialAd(
//    () => { ResumeGame()},
//    (errCode, errMsg) => { HandleError()});

//// ������
//ByteGameAdManager.Instance.ShowBanner();
//ByteGameAdManager.Instance.HideBanner();