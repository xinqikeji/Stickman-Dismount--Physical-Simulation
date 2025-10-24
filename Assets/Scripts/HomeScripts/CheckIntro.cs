using UnityEngine;
using System.Collections;

public class CheckIntro : MonoBehaviour {
	public ParticleSystem pars;
	bool play = false;
	public GameObject CheckSoundObj;
	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.layer.ToString () != "GROUND")
		{
			if (!play)
			{
				play = true;
				pars.Play ();
				if(StartManager.click==0)
				{
				GameObject.FindObjectOfType<SoundMusicManager> ().Explosion ();
				Invoke ("turnoffbombsound", 1.0f);
				}

			}
			GameObject.FindObjectOfType<PanelAnimator> ().show ();
		}
	}
	public void turnoffbombsound()
	{
		GameObject.FindObjectOfType<SoundMusicManager> ().TurnoffExplosion ();
	}
}
