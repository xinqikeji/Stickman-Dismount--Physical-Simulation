using UnityEngine;
using System.Collections;

public class PanelAnimator : MonoBehaviour 
{
	public Animator anim;
	void Start()
	{
		showStart ();
	}
	public void show()
	{
		anim.enabled = true;
//		anim.SetBool ("Off", false);
		anim.SetTrigger ("show");
	}
	public void showStart()
	{
		anim.enabled = true;
		anim.Rebind ();
		anim.SetTrigger ("start");
	}
	public void hide(){
		anim.enabled = true;
		anim.Rebind ();
		anim.SetTrigger ("hide");
	}
	public void disableGO(){
		this.gameObject.SetActive (false);
	}
	public void disableAnimator(){
		anim.enabled = false;
	}
}
