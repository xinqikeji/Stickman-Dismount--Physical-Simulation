using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MissionManager : MonoBehaviour {
	public Animator animator;
	public Text txtCoinCompleted,txtInfoMission,txtStatic;
	public Color32 colorCompleted;
	void Awake () {
		this.TickNormal ();
	}
	public void ShowMission(){
		this.gameObject.SetActive (true);
		this.animator.enabled = true;
		animator.SetTrigger ("show");
	}
	public void HideMission(){
		this.animator.enabled = true;
		animator.SetTrigger ("hide");
	}
	public void setInfoMission(string cointake,string info,string txtstatic){
		this.txtCoinCompleted.text = cointake;
		this.txtInfoMission.text = info;
		this.txtStatic.text = txtstatic;
	}
	public void eventAimHide(){
		this.gameObject.SetActive (false);
	}
	public void disableAnim()
	{
	}
	public void showMissionOnEndGame(){
		this.gameObject.SetActive (true);
		this.animator.enabled = true;
		animator.SetTrigger ("showend");
	}
	public void _disableAnim(){
		animator.enabled = false;
	}
	public void TickCompleted(){
		txtStatic.color = colorCompleted;
		txtInfoMission.color = colorCompleted;
	}
	public void TickNormal(){
		txtStatic.color = Color.white;
		txtInfoMission.color = Color.white;
	}
}
