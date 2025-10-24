using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Com.LuisPedroFonseca.ProCamera2D;
using EasyMobile;
using TTSDK;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine.Networking;

public class GUIManager : MonoBehaviour
{
    public ByteGameAdManager byteGameAdManager;
    public static GUIManager Instance;
	public GameObject panelUIGPL;
	public Text txtCountdown;
	public PlayerControlManager ControlManager;
	public GameObject goBtnEject;
	public Text txtCoinmain;
	public VehicleSelectManager vehicleSelect;
	public MissionControl missionControl;
	public ScoreEndGame scoreEndGame;
	public Text textStatus;
	public GameObject statusVehicle;
	public bool modeTest = false;

	public FireworkScript fireWork;

	IEnumerator delayCheckUpdateStatusVehicle ()
	{
		yield return new WaitForSecondsRealtime (1f);
		this.UpdateStatusVehicle ();
	}

	public void UpdateStatusVehicle ()
	{
		int i = vehicleSelect.CheckStatus ();
		if (i > 0) {
			statusVehicle.SetActive (true);
			textStatus.text = "" + i;
		} else {
			statusVehicle.SetActive (false);
		}
	}

	public void SetOrtSize (float size)
	{
	}

	void Awake ()
	{
		CoinManager.GetCoin ();
		this.UpdateTextCoin ();
		BadLogic.playing = false;
		BadLogic.pause = false;
		this.SetOrtSize (10);
	}

	void Start ()
	{
		Instance = this;
		this._PanelUIGPLStatus (true);
		this.disable_txtCountdown ();
		this.hideButtonEject ();
		this.missionControl.showMission ();
		this.scoreEndGame.gameObject.SetActive (false);
		this.StartCoroutine (delayCheckUpdateStatusVehicle ());
	}

	public void _PanelUIGPLStatus (bool status)
	{
		panelUIGPL.SetActive (status);
	}

	public void _Countdown (string text)
	{
		if (!this.txtCountdown.enabled) {
			this.txtCountdown.enabled = true;
		}
		this.txtCountdown.text = "" + text;
	}

	public void disable_txtCountdown ()
	{
		if (this.txtCountdown.enabled) {
			this.txtCountdown.enabled = false;
		}
	}

	public void _reset ()
	{
		ControlManager.click_Reset ();
	}

	public void showButtonEject ()
	{
		goBtnEject.SetActive (true);
	}

	public void hideButtonEject ()
	{
		goBtnEject.SetActive (false);
	}

	public void UpdateTextCoin ()
	{
		txtCoinmain.text = "" + CoinManager.coin;
	}

	public void OnClick_Vehicle ()
	{
		vehicleSelect.ShowVehicleSelect ();
	}

	public void HideSelectVehicle ()
	{
		vehicleSelect.HideVehicleSelect ();
		this.UpdateStatusVehicle ();
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
    public void ClickAgain ()
	{
		byteGameAdManager.PlayRewardedAd("40gi3cfaae51133oke",
					(isValid, duration) =>
					{
						//isValid广告是否播放完，正常游戏逻辑在以下部分
						Debug.LogError(0);
						if (isValid)
						{
							StartCoroutine(SendPostRequest());

                            PlayerControlManager.Instance.click_Reset();
                            missionControl.resetsaukhiHoanthanh();
                            this.missionControl.showMission();
                            GameObject.FindObjectOfType<ScoreEndGame>().HideAnimation();
                            Time.timeScale = 1;
                            BadLogic.pause = false;
                            this.fireWork.resetGO();
                            GameObject.FindObjectOfType<ScoreEndGame>().ResetDoubleCoinText();

                            if (DataManager.Instance.intCheckForAds < 2)
                            {
                                if (DataManager.Instance.IntAds <= 3)
                                {
                                    DataManager.Instance.AddIntAds(1);

                                }
                                else
                                {
                                    //DataManager.Instance.RemoveIntAds (4);
                                    //            bool isReady = AdManager.IsInterstitialAdReady();
                                    //            // Show it if it's ready
                                    //            if (isReady)
                                    //            {
                                    //                AdManager.ShowInterstitialAd();
                                    //            }
                                }
                            }

						}


					},
					(errCode, errMsg) =>
					{
						Debug.LogError(1);
					});
		

	}

	public void ClickToMenu ()
	{
		SceneManager.LoadScene ("Home");
		if(DataManager.Instance.intCheckForAds<2)
		{
			if (DataManager.Instance.IntAds <= 3) 
			{
				DataManager.Instance.AddIntAds (1);

			} else
			{
				DataManager.Instance.RemoveIntAds (4);
                bool isReady = AdManager.IsInterstitialAdReady();
                // Show it if it's ready
                if (isReady)
                {
                    AdManager.ShowInterstitialAd();
                }
            }
		}

	}

	public void showEndGame ()
	{
		this.scoreEndGame.SHOW ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.E)) {
			this.showEndGame ();
			this.missionControl.ShowMissioEndgame ();
		}
	}

	public void OnEndGame ()
	{
		BadLogic.pause = true;
		Time.timeScale = 0;
		this.showEndGame ();
		this.missionControl.ShowMissioEndgame ();
		fireWork._Play ();
		ControlManager._OnEndGame ();

		//-----Report Achievement----
		#if UNITY_IOS
		if(DataManager.Instance.IntAds>=1&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_NewbieID", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}

		//-----------
		//-----------

		if(DataManager.Instance.Missions>=1&DataManager.Instance.Missions<3&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzeHandy", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Missions>=3&DataManager.Instance.Missions<5&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverHandy", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Missions>=10&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldHandy", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}

		//-----------
		//-----------
		if(DataManager.Instance.Maps>=5&DataManager.Instance.Maps<10&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzeDiscover", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Maps>=10&DataManager.Instance.Maps<15&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverDiscoverer", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Maps>=15&LeaderboardController.checkSignIn==1)
		{	GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldDiscoverer", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});

		}
		//------------
		//-----------
		if(DataManager.Instance.Vehicle>=5&DataManager.Instance.Vehicle<7&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzwCollector", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Vehicle>=7&DataManager.Instance.Vehicle<11&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverCollector", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.Vehicle>=11&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldCollector", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		//------------
		//-----------
		if(DataManager.Instance.X2Coins>=10&DataManager.Instance.X2Coins<20&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzeAs", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.X2Coins>=20&DataManager.Instance.X2Coins<50&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverAs", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(DataManager.Instance.X2Coins>=50&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldAs", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		//------------
		//-----------
		if( PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE")>=10000& PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE")<100000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzeClever", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if( PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE")>=100000& PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE")<1000000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverClever", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if( PlayerPrefs.GetInt (BadLogic.nameMap + "BESTSCORE")>=1000000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldClever", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		//------------
		//-----------
		if( PlayerPrefs.GetInt ("COIN")>=5000& PlayerPrefs.GetInt ("COIN")<50000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_BronzeRick", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}
		else if(PlayerPrefs.GetInt ("COIN")>=50000& PlayerPrefs.GetInt ("COIN")<250000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_SilverRick", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});

		}
		else if( PlayerPrefs.GetInt ("COIN")>=250000&LeaderboardController.checkSignIn==1)
		{
			GameCenterPlatform.ShowDefaultAchievementCompletionBanner (true);
			Social.ReportProgress("_GoldRick", 100, (result) => {
				Debug.Log(result ? "Reported achievement" : "Failed to report achievement");
			});
		}

		#endif
		//---------------------------

		
		//------------------------------------
	}

	public void showPause ()
	{
		BadLogic.pause = true;
		Time.timeScale = 0;
		if(DataManager.Instance.intCheckForAds<2)
		{
            bool isReady = AdManager.IsInterstitialAdReady();
            // Show it if it's ready
            if (isReady)
            {
                AdManager.ShowInterstitialAd();
            }
        }
	}

	public void hidePause ()
	{
		Time.timeScale = 1;
		BadLogic.pause = false;
	}


}
