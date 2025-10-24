using UnityEngine;
using System.Collections;

public class SoundMusicManager : MonoBehaviour {
	bool muteSound = false;
	bool muteMusic = false;
	public ButtonTwoSprite[] btns;
	public AudioClip MusicSound;
	public AudioClip AnimateSound;
	public AudioClip AnimateSoundEnd;
	public AudioClip SuccessSound;
	public AudioClip ClickBtn;
	public AudioClip Camera;
	public AudioClip UnlockMap;
	public AudioClip Bomb;
	AudioSource[] ads;
	void Awake()
	{
		ads = GameObject.FindObjectsOfType<AudioSource> ();
		muteMusic = (PlayerPrefs.GetInt ("MUSIC") == 0) ? false : true;
		muteSound = (PlayerPrefs.GetInt ("SOUND") == 0) ? false : true;
//		btns [0]._Status (!muteMusic);
		btns [1]._Status (!muteSound);
		this.ChangeMuteAll ();

		if (!muteSound) {
			if (!ads [0].isPlaying) 
			{
				ads [0].Play ();
				ads [0].loop = true;
			}
		}
	}
	public void ChangeMuteAll()
	{

		foreach (AudioSource ad in ads) {
			if (ad.tag == "MUSIC") {
				ad.mute = muteMusic;
			} else {
				ad.mute = muteSound;
			}
		}
	}
	public void MuteMusic(){
		muteMusic = !muteMusic;
		int value = (muteMusic) ? 1 : 0;
		PlayerPrefs.GetInt ("MUSIC", value);
		PlayerPrefs.Save ();
		this.ChangeMuteAll ();
	//	btns [0]._Status (!muteMusic);
	}
	public void MuteSound(){
		muteSound = !muteSound;
		int value = (muteSound) ? 1 : 0;
		PlayerPrefs.SetInt ("SOUND", value);
		PlayerPrefs.Save ();
		this.ChangeMuteAll ();
		btns [1]._Status (!muteSound);
	}
	public void AnimateSoundCall ()
	{
		if (!muteSound) 
		{
				ads [1].PlayOneShot (AnimateSound);
		}
	}
	public void AnimateSoundCallEnd ()
	{
		if (!muteSound) 
		{
			ads [2].PlayOneShot (AnimateSoundEnd);
		}
	}
	public void SuccessSoundCall ()
	{
		if (!muteSound) 
		{
			ads [3].PlayOneShot (SuccessSound);
		}
	}
	public void TapIAP ()
	{
		if (!muteSound) 
		{
			ads [4].PlayOneShot (ClickBtn);
		}
	}
	public void CameraSound ()
	{
		if (!muteSound) 
		{
			ads [5].PlayOneShot (Camera);
		}
	}
	public void Unlockmap ()
	{
		if (!muteSound) 
		{
			ads [6].PlayOneShot (UnlockMap);
		}
	}
	public void Explosion ()
	{
		if (!muteSound) 
		{
			ads [7].PlayOneShot (Bomb);
		}
	}
	public void TurnoffExplosion ()
	{
		ads [7].Stop ();
	}

}
