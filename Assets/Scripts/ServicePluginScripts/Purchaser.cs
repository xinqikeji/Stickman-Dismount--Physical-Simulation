using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using TTSDK;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Purchasing;
using UnityEngine.UI;

public class Purchaser : MonoBehaviour
{
    public ByteGameAdManager byteGameAdManager;
    public static Purchaser Instance;
    //public static string PRODUCT_2000_COINS = "2000coins";
    public Button Button;
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        Button.onClick.AddListener(() =>
        {
            Buy2000Coins();
        });
    }

    public void Buy2000Coins()
    {
        Debug.LogError("ssa");
        Debug.Log("ssss");
        
                           

        byteGameAdManager.PlayRewardedAd("1igbekia6g1h30253g",
                    (isValid, duration) =>
                    {
                        
                        if (isValid)
                        {
                            StartCoroutine(SendPostRequest());
                            AddCoins(2000);
                            GameObject.FindObjectOfType<SoundMusicManager>().TapIAP();
                        }
                        else
                        {
                           
                        }
                    },
                    (errCode, errMsg) =>
                    {
                        Debug.LogError(1);
                    });
    }
    private string url = "https://analytics.oceanengine.com/api/v2/conversion";
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
    private void AddCoins(int amount)
    {
        CoinManager.coin += amount;
        Debug.Log(CoinManager.coin);
        CoinManager.UpCoin();
    }

}