﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using SgLib;

public class DailyRewardUI : MonoBehaviour
{

    public GameObject dailyRewardBtn;
    public Text dailyRewardBtnText;
    public GameObject rewardUI;
	public GameObject rewardUIBG;
    Animator dailyRewardAnimator;
	public Text txtCoin;
	public GameObject TextCoinObj;

    // Use this for initialization
    void Start()
    {
		TextCoinObj.SetActive (false);
		dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!DailyRewardController.Instance.disable && dailyRewardBtn.gameObject.activeSelf)
        {
            if (DailyRewardController.Instance.CanRewardNow())
            {
                dailyRewardBtnText.text = "签到";
                dailyRewardAnimator.SetTrigger("activate");
            }
            else
            {
                TimeSpan timeToReward = DailyRewardController.Instance.TimeUntilReward;
                dailyRewardBtnText.text = string.Format("{0:00}-{1:00}-{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
                dailyRewardAnimator.SetTrigger("deactivate");
            }
        }

		txtCoin.text = "" + CoinManager.coin;
    }

    public void ShowStartUI()
    {
        ShowDailyRewardBtn();
    }

    void ShowDailyRewardBtn()
    {
        // Not showing the daily reward button if the feature is disabled
        if (!DailyRewardController.Instance.disable)
        {
            dailyRewardBtn.SetActive(true);
        }
    }

    public void GrabDailyReward()
    {
		
		if (DailyRewardController.Instance.CanRewardNow ()) 
		{

			dailyRewardBtn.SetActive (false);
			float value = UnityEngine.Random.value;
			int reward = DailyRewardController.Instance.GetRandomRewardCoins ();

			// Round the number and make it mutiplies of 5 only.
			int roundedReward = (reward / 5) * 5;
			// Show the reward UI
			ShowRewardUI (roundedReward);

			// Update next time for the reward
			DailyRewardController.Instance.ResetNextRewardTime ();
			TextCoinObj.SetActive (true);
		} 
//		else
//			SoundController.Sound.DisactiveButtonSound ();
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
//		Debug.Log ("DailyClose");
    }

    public void HideRewardUI()
    {
		rewardUIBG.SetActive (false);
        rewardUI.GetComponent<RewardUIController>().Close();
		TextCoinObj.SetActive (false);
//		Debug.Log ("DailyClose");
    }

}
