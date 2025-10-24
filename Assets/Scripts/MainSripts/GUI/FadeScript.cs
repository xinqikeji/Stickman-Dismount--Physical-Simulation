using UnityEngine;
using System.Collections;

public class FadeScript : MonoBehaviour
{
	public Animator anim;

	public void showFade ()
	{
		this.gameObject.SetActive (true);
		anim.enabled = true;
		anim.SetTrigger ("show");
	}

	public void hideFade ()
	{
		this.gameObject.SetActive (true);
		anim.enabled = true;
		anim.SetTrigger ("hide");
	}

	public void DisableAnim ()
	{
		anim.enabled = false;
	}
}
