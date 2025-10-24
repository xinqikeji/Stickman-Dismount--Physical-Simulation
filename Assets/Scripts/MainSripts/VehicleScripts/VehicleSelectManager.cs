using UnityEngine;
using System.Collections;
[System.Serializable]
public class ITEM_VEHICLE{
	public Sprite sprite;
	public string name;
	public int price;
}
public class VehicleSelectManager : MonoBehaviour {
	public GameObject panelVehicleSelect;
	public Transform content;
	ItemVehicleScript[] itemVehicles;
	public ITEM_VEHICLE[] _Vehicle;
	public Animator anim;
	bool hide = false;
	public void ShowVehicleSelect(){
		anim.enabled = true;
		anim.SetTrigger ("show");
		anim.gameObject.SetActive (true);
		this.panelVehicleSelect.SetActive (true);
	}
	public void HideVehicleSelect(){
		anim.enabled = true;
		anim.SetTrigger ("hide");
		hide = true;
	}
	void Awake(){
		int count = content.childCount;
		this.itemVehicles = new ItemVehicleScript[count];
		for (int i = 0; i < count; i++) {
			this.itemVehicles [i] = content.GetChild (i).GetComponent<ItemVehicleScript> ();
			this.itemVehicles [i].SetInfo (this._Vehicle[i]);
		}
	}
	public void DisableAnim(){
		anim.enabled = false;
		if (hide) {
			anim.gameObject.SetActive (false);
			hide = false;
		}
	}
	public int CheckStatus(){
		int count = 0;
		for (int i = 0; i < itemVehicles.Length; i++) {
			count += itemVehicles [i].CheckStatus ();
		}
		return count;
	}
}
