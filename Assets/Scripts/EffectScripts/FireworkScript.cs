using UnityEngine;
using System.Collections;

public class FireworkScript : MonoBehaviour
{
//	public ParticleSystem fireWork;
	float timer = 0;
	bool play = false;
	public SpriteRenderer spr;
	public void _Play ()
	{
//		this.fireWork.gameObject.SetActive (true);
//		this.fireWork.transform.position = this.transform.position;
//		this.fireWork.Clear ();
//		this.fireWork.Play ();
		play = true;
		spr.enabled = true;
		SoundScript.Instance.playClip_fireWork ();
	}

	void Update ()
	{
		if (play) {
			if (Time.timeScale < 0.01f) {
//				this.fireWork.Simulate (Time.unscaledDeltaTime, true, false);
				timer += Time.unscaledDeltaTime;
				if (timer > 5) {
					timer = 0;
					play = false;
//					this.fireWork.gameObject.SetActive (false);
				}
			}
		}
	}

	public void resetGO ()
	{
		timer = 0;
		play = false;
//		this.fireWork.gameObject.SetActive (false);
		spr.enabled = false;
	}
}
