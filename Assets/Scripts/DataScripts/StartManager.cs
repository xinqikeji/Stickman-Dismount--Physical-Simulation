using UnityEngine;
using System.Collections;
using EasyMobile;
using System.Collections.Generic;
using TTSDK;
using UnityEngine.Networking;
using Newtonsoft.Json;
public class StartManager : MonoBehaviour {
	public static StartManager Instance;
	public SelectMapManager selectmapMan;
	public GameObject panelMap;
	public bool firstTimePlayGame = false;
	public FadeScript fadeScipt;
	public GameObject coinHUD;
	public static int click;
    private TTBannerStyle m_style = new TTBannerStyle();
    private TTBannerAd m_bannerAdIns;
    void Awake()
	{

        banner();

        click = 0;
		Instance = this;
		BadLogic.pause = false;
		firstTimePlayGame = (PlayerPrefs.GetInt ("FIRSTTIME") == 0) ? true : false;
		if (firstTimePlayGame) {
			PlayerPrefs.SetInt ("FIRSTTIME", 1);
			CoinManager.coin = 2000;
			CoinManager.UpCoin ();
		} else {
			CoinManager.GetCoin ();
		}
		statusCoin (false);
	}
    //插屏广告
    // 在程序入口调用一次（例如 Unity 的 RuntimeInitializeOnLoadMethod）
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void InitOnLoad()
    {
        InterstitialManager.Init(); // 初始化广告
    }
    private static float timer;

    [RuntimeInitializeOnLoadMethod]
    private static void Tick()
    {
        timer += Time.deltaTime;
        if (timer >= 60f)      // 每 60 秒检查一次
        {
            InterstitialManager.Show();
            timer = 0f;
        }
    }







    //
    #region banner广告
    public void banner()
    {
        
        m_style.top = 0;
        m_style.left = 20;
        m_style.width = 500;

        if (m_bannerAdIns != null && m_bannerAdIns.IsInvalid())
        {
            m_bannerAdIns.Destroy();
            m_bannerAdIns = null;
        }

        if (m_bannerAdIns == null)
        {
            var param = new CreateBannerAdParam
            {
                BannerAdId = "jd44ha0h34b346d3b9",
                Style = m_style,
                AdIntervals = 60
            };
            m_bannerAdIns = TT.CreateBannerAd(param);
            m_bannerAdIns.OnError += OnAdError;
            m_bannerAdIns.OnClose += OnClose;
            m_bannerAdIns.OnResize += OnBannerResize;
            m_bannerAdIns.OnLoad += OnBannerLoaded;
        }
        m_bannerAdIns.Show();

    }
    void OnAdError(int iErrCode, string errMsg)
    {
        Debug.LogError("错误 ： " + iErrCode + "  " + errMsg);
    }

    private void OnBannerLoaded()
    {
        m_bannerAdIns?.Show();
        
    }

    private void OnBannerResize(int width, int height)
    {
        Debug.Log($"OnBannerResize - width:{width} height:{height}");
    }

    private void OnClose()
    {
        StartCoroutine(SendPostRequest3());
        Debug.Log("banner广告关闭");
    }
    #endregion


    void Start(){
       
        StartCoroutine(SendPostRequest());
       
        StartCoroutine(SendPostRequest2()); 
        StartCoroutine(SendPostRequest1());
       
        panelMap.SetActive (false);
	}
	public void clickPlay()
	{
		click = 1;
		panelMap.SetActive (true);
		if(DataManager.Instance.intCheckForAds<2)
		{
		if (DataManager.Instance.IntAds <= 2) 
		{
			DataManager.Instance.AddIntAds (1);
			
		} else
		{
			DataManager.Instance.RemoveIntAds (3);
                bool isReady = AdManager.IsInterstitialAdReady();
                if (isReady)
                {
                    AdManager.ShowInterstitialAd();
                }
            }
		}
	}
	public void clickbackMap(){
		panelMap.SetActive (false);
		Time.timeScale = 1;
		this.statusCoin (false);
	}
	public void clickOption(){
	}
	public void clickExit(){
	}
	public void exitNo(){
	}
	public void exitYes(){
		Application.Quit ();
	}
	public void Moregame(){
		Application.OpenURL ("");
	}
	public void statusCoin(bool status){
		coinHUD.SetActive (status);
	}



    //ecpm返回值

    // 替换为实际的URL
    private string url = "https://analytics.oceanengine.com/api/v2/conversion";
    //激活
    IEnumerator SendPostRequest()
    {
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", "active" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
    //有效激活
    IEnumerator SendPostRequest1()
    {
        yield return new WaitForSeconds(1.5f); // 新增：等待1秒
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", " effective_active" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
    //创建角色
    IEnumerator SendPostRequest2()
    {
        yield return new WaitForSeconds(1f); // 新增：等待1秒
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", "create_gamerole" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
    //
    IEnumerator SendPostRequest3()
    {
        yield return new WaitForSeconds(2f); // 新增：等待1秒
        TTSDK.LaunchOption launchOption = TT.GetLaunchOptionsSync();
        Debug.LogError(launchOption.Query);
        if (launchOption.Query != null)
        {
            foreach (KeyValuePair<string, string> kv in launchOption.Query)
                if (kv.Value != null)
                    Debug.LogError(kv.Key + ": " + kv.Value);
                else
                    Debug.Log(kv.Key + ": " + "null ");
        }
        // 创建一个字典来存储POST请求的数据
        Dictionary<string, object> postData = new Dictionary<string, object>
        {
            { "event_type", "micro_game_ltv" },
            { "context", new Dictionary<string, object>
                {
                    { "ad", new Dictionary<string, object>
                    {
                            { "callback", launchOption.Query["clickid"]} // 替换为实际的clickid   
                        }
                    }
                }
            },
            { "timestamp", System.DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() } // 当前时间戳
        };
        // 将字典转换为JSON格式
        string json = JsonConvert.SerializeObject(postData);
        // 创建UnityWebRequest对象
        using (UnityWebRequest request = UnityWebRequest.Post(url, json))
        {
            // 设置请求头
            request.SetRequestHeader("Content-Type", "application/json");

            // 设置POST请求的body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();

            // 发送请求
            yield return request.SendWebRequest();

            // 检查请求是否成功
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("sssError: " + request.error);
            }
            else
            {
                Debug.Log("sssResponse: " + request.downloadHandler.text);
            }
        }
    }
}
