using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using EasyMobile;

public class ScoreEndGame : MonoBehaviour {
	public Animator anim;
	public Text[] TEXTS;
	public Button DoubleCoinBtn;
	public GameObject TextLastEarn;
	public Text TextLastEarnTxt;
	public Text TextDoubleTxt;
	int CoinsEarn;
	void Awake()
	{
        bool isReady = AdManager.IsRewardedAdReady();
        if (isReady) 
		{
			DoubleCoinBtn.interactable = true;
		}
		else
			DoubleCoinBtn.interactable = false;
	}
	void Start()
	{
		CoinsEarn = int.Parse (TEXTS [0].text);
		TextDoubleTxt.text = "";
		TextLastEarn.SetActive (false);

	}
	public void SetScore(string[] texts)
	{
		for(int i = 0 ; i < TEXTS.Length;i++)
		{
			TEXTS [i].text = texts[i];
		}
	}
	public void SHOW()
	{
		this.gameObject.SetActive (true);
		anim.enabled = true;
		anim.Rebind ();
		anim.SetTrigger ("show");
	}
	public void endShow(){
		anim.enabled = false;
		Time.timeScale = 0;
	}
	public void DoubleCoinCall()
	{
        bool isReady = AdManager.IsRewardedAdReady();
        if (isReady)
        {
            AdManager.ShowRewardedAd();
            DoubleCoinBtn.interactable = false;
        }
	}
    void OnEnable()
    {
        AdManager.RewardedAdCompleted += RewardedAdCompletedHandler;
    }
    // The event handler
    void RewardedAdCompletedHandler(RewardedAdNetwork network, AdLocation location)
    {
        DoubleCoinCallBack();
    }
    // Unsubscribe
    void OnDisable()
    {
        AdManager.RewardedAdCompleted -= RewardedAdCompletedHandler;
    }
    public void DoubleCoinCallBack()
	{
		TextLastEarn.SetActive (true);
		TextLastEarnTxt.text=""+CoinsEarn;
		TextDoubleTxt.text = ""+2*CoinsEarn;
		TEXTS [0].text = "";
		//Add DoubleCoin
		CoinManager.coin += CoinsEarn;
		CoinManager.UpCoin ();
		GameObject.FindObjectOfType<GUIManager> ().UpdateTextCoin ();
		GameObject.FindObjectOfType<SoundScript> ().SuccessSoundCallBack ();
		DataManager.Instance.AddX2Coins (1);
	}
	public void ResetDoubleCoinText()
	{
		TextLastEarn.SetActive (false);
		TextLastEarnTxt.text="";
		TextDoubleTxt.text = "";

        bool isReady = AdManager.IsRewardedAdReady();
        if (isReady)
        {
			DoubleCoinBtn.interactable = true;
		}
		else
			DoubleCoinBtn.interactable = false;
	}

	public void HideAnimation ()
	{
		anim.SetBool ("On", false);
		anim.SetTrigger ("Off");

	}

	private void ResetAnimationParameters ()
	{
		if (anim == null) {
			return;
		}
		anim.SetBool ("On", false);
		anim.SetBool ("Off", false);
	}
}
