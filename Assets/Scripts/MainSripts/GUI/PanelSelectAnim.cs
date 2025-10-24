using UnityEngine;
using System.Collections;

public class PanelSelectAnim : MonoBehaviour
{
	public Animator anim;
	void OnEnable(){
		this.show ();
	}
	public void show ()
	{
		anim.enabled = true;
		anim.SetTrigger ("show");
	}

	public void disableAnim ()
	{
		anim.enabled = false;
		StartManager.Instance.statusCoin (true);
	}
}
