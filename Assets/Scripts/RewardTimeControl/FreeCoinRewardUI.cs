using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using SgLib;
using UnityEngine.Advertisements;
using EasyMobile;

public class FreeCoinRewardUI : MonoBehaviour
{

    public GameObject dailyRewardBtn;
    public Text dailyRewardBtnText;
	public Text dailyRewardBtnText1;
    public GameObject rewardUI;
	public GameObject rewardUIBG;
    Animator dailyRewardAnimator;
	public static int isFreeAd=0;
	public Text CountNumAds;
	public Button FreeCoinBtn;
	public GameObject TextCoinObj;
	public Text textCoin;

    // Use this for initialization
    void Start()
    {
		TextCoinObj.SetActive (false);
		dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
		textCoin.text = ""+CoinManager.coin;
		CountNumAds.text = ""+DataManager.Instance.FreeAdNumber;
        if (!DailyRewardController1.Instance.disable && dailyRewardBtn.gameObject.activeSelf)
        {
            bool isReady = AdManager.IsRewardedAdReady();
            if (DailyRewardController1.Instance.CanRewardNow()&& isReady)
            {
                dailyRewardBtnText.text = "ACTIVE";
				dailyRewardBtnText1.text = "ACTIVE";
                dailyRewardAnimator.SetTrigger("activate");
            }
            else
            {
                TimeSpan timeToReward = DailyRewardController1.Instance.TimeUntilReward;
                dailyRewardBtnText.text = string.Format("{0:00}-{1:00}-{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
				dailyRewardBtnText1.text = string.Format("{0:00}-{1:00}-{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
                dailyRewardAnimator.SetTrigger("deactivate");
            }
			if (DailyRewardController1.Instance.CanRewardNow()&&!AdManager.IsRewardedAdReady())
			{
				dailyRewardBtnText.text = "WAIT";
				dailyRewardBtnText1.text = "WAIT";
				dailyRewardAnimator.SetTrigger("deactivate");
			}
        }
    }

    public void ShowStartUI()
    {

        ShowDailyRewardBtn();
    }

    void ShowDailyRewardBtn()
    {
        // Not showing the daily reward button if the feature is disabled
        if (!DailyRewardController1.Instance.disable)
        {
            dailyRewardBtn.SetActive(true);
        }
    }
	public void FreeCoinWatchAds()
	{
        bool isReady = AdManager.IsRewardedAdReady();
        if (DailyRewardController1.Instance.CanRewardNow () && isReady ) 
		{

			if (DataManager.Instance.FreeAdNumber <= 10 && DataManager.Instance.FreeAdNumber > 0) 
			{
                AdManager.ShowRewardedAd();
                FreeCoinBtn.interactable = false;
				DataManager.Instance.RemoveFreeAdNumber (1);
                isFreeAd = 1;
				TextCoinObj.SetActive (true);
			} 
			if(DataManager.Instance.FreeAdNumber<=0)
			{
				DailyRewardController1.Instance.ResetNextRewardTime ();	
				DataManager.Instance.AddFreeAdNumber (10);
			}
//			else SoundController.Sound.DisactiveButtonSound ();


		} 
//		else 
//			SoundController.Sound.DisactiveButtonSound ();
	}

    void OnEnable()
    {
        AdManager.RewardedAdCompleted += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
    {
        FreeAdsCallBack();
    }
    // Unsubscribe
    void OnDisable()
    {
        AdManager.RewardedAdCompleted -= RewardedAdCompletedHandler;
    }
    public void FreeAdsCallBack()
	{
		isFreeAd = 0;
		dailyRewardBtn.SetActive (false);
		FreeCoinBtn.interactable = true;
		float value = UnityEngine.Random.value;
		int reward = DailyRewardController1.Instance.GetRandomRewardCoins ();

		// Round the number and make it mutiplies of 5 only.
		int roundedReward = (reward / 5) * 5;
		// Show the reward UI
		ShowRewardUI (roundedReward);
	}


    public void ShowRewardUI(int reward)
    {
        rewardUI.SetActive(true);
		rewardUIBG.SetActive (true);
        rewardUI.GetComponent<RewardUIController>().Reward(reward);

        RewardUIController.RewardUIClosed += OnRewardUIClosed;
    }

    void OnRewardUIClosed()
    {
        RewardUIController.RewardUIClosed -= OnRewardUIClosed;
        dailyRewardBtn.SetActive(true);
		TextCoinObj.SetActive (false);
    }

    public void HideRewardUI()
    {
		rewardUIBG.SetActive (false);
        rewardUI.GetComponent<RewardUIController>().Close();
		TextCoinObj.SetActive (false);
//		Debug.Log ("Close");
    }

}
