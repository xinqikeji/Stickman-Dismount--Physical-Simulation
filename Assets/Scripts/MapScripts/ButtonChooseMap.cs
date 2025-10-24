using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ButtonChooseMap : MonoBehaviour
{
	public string key;
	public int price;
	public GameObject panelBlock;
	public Text txtPrice,txtName;
	bool unlocked = false;
	public Image imgMap;
	public int indexMap;
	public void _Start ()
	{
		unlocked = (PlayerPrefs.GetInt (key + "unlocked") == 0) ? false : true;
		panelBlock.SetActive (!unlocked);
		txtName.text = key;
		txtPrice.text = "" + price;
	}
	public void clickUnlock()
	{
		
		if (!unlocked) 
		{
			if (CoinManager.CheckBuy (price)) 
			{
				PlayerPrefs.SetInt (key + "unlocked", 1);
				CoinManager.coin -= price;
				CoinManager.UpCoin ();
				this.unlocked = true;
				this.panelBlock.SetActive (!unlocked);
				DataManager.Instance.AddMaps (1);
				GameObject.FindObjectOfType<SoundMusicManager> ().Unlockmap ();
				Debug.Log ("Unlock Map!");
		      } 
			else
			{
				Debug.Log ("Not Enough Coins");
			}
		}
	}
	public void clickChooseMap()
	{
		if (unlocked) 
		{
			// Choose Map!
			StartManager.Instance.fadeScipt.showFade();
			BadLogic.nameMap = indexMap;
			StartCoroutine (delayLoadScene ());
			Debug.Log ("Click ChooseMap");
		}

	}
	public void SetImageMap(Sprite spr){
		imgMap.sprite = spr;
	}
	IEnumerator delayLoadScene(){
		yield return new WaitForSecondsRealtime (0.5f);
		SceneManager.LoadScene("Main");
	}
	public int CheckStatus(){
		int temp = 0;
		if (!unlocked) {
			if (CoinManager.CheckBuy (price)) {
				temp = 1;
			}
		} else {
			temp = 0;
		}
		return temp;
	}
}