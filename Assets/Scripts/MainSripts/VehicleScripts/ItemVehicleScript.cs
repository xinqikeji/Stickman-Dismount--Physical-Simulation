using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemVehicleScript : MonoBehaviour {
	public string keyName;
	public Sprite spAvatar;
	public Image imgAvatar;
	public int price;
	public Text txtName,txtPrice;
	public Button btnUnlock, btnSelect;
	public bool unlocked = false;
	bool notVehicle = false;
//	public static int intVehicle;
	public void SetInfo(ITEM_VEHICLE TEMP){
		this.spAvatar = TEMP.sprite;
		this.keyName = TEMP.name;
		this.price = TEMP.price;
		this._Start ();
	}
	public void _Start () {
		if (keyName != "") {
			unlocked = (PlayerPrefs.GetInt (keyName + "unlocked") == 0) ? false : true;
			txtName.text = keyName;
			notVehicle = false;

		} else {
			txtName.text = "NONE";
			unlocked = true;
			notVehicle = true;
		}
		if (spAvatar) {
			imgAvatar.sprite = spAvatar;
		} else {
			imgAvatar.enabled = false;
		}
		if (!unlocked) {
			txtPrice.text = ""+price;
			btnUnlock.gameObject.SetActive (true);
			btnUnlock.onClick.AddListener(delegate {
				this.ClickUnlock();
			});
		} else {
			btnUnlock.gameObject.SetActive (false);
		}
		btnSelect.onClick.AddListener (delegate {
			this.SelectVehicle();
		});
	}
	public void ClickUnlock(){
		if (CoinManager.CheckBuy (price)) 
		{
			CoinManager.coin -= price;
			PlayerPrefs.SetInt (keyName + "unlocked", 1);
			CoinManager.UpCoin ();
			GUIManager.Instance.UpdateTextCoin ();
			btnUnlock.gameObject.SetActive (false);
			DataManager.Instance.AddVehicle (1);
		}
	}
	public void SelectVehicle(){
		// Chon Vehicles;
		if (!notVehicle) {
			VehiclesManager.Instance.ChangeVehicle (keyName);
		} else {
			PlayerControlManager.Instance.isVehicles = false;
			PlayerControlManager.Instance.click_Reset ();
		}
		GUIManager.Instance.HideSelectVehicle ();
	}
	public int CheckStatus(){
		int temp = 0;
		if(!unlocked){
			if (CoinManager.CheckBuy (price)) {
				temp = 1;
			}
		}
		return temp;
	}
}
