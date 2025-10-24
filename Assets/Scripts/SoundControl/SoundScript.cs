using UnityEngine;
using System.Collections;

public class SoundScript : MonoBehaviour
{

	public static SoundScript Instance;
	public AudioClip[] clipBodyHit;
	public AudioClip[] clipBoneBreak;
	public AudioClip[] clipBeakVehicle;
	public AudioClip clipFireWork;
	public AudioClip SuccesSound;
	public AudioSource[] ads;
	public AudioSource adBG;
	public AudioClip[] clipBG;
	bool muteMusic,muteSound;
	void Awake ()
	{
		Instance = this;
		muteMusic = (PlayerPrefs.GetInt ("MUSIC") == 0) ? false : true;
		muteSound = (PlayerPrefs.GetInt ("SOUND") == 0) ? false : true;
		print (muteSound);
	}
	public void _PlayClipHitBody ()
	{
		if (!muteSound) {
			if (!ads [0].isPlaying) {
				int i = Random.Range (0, 3);
				ads [0].PlayOneShot (clipBodyHit [i]);
			}
		}
	}

	public void _PlayClipBoneBreak ()
	{
		if (!muteSound) {
			if (!ads [1].isPlaying) {
				int i = Random.Range (0, 3);
				ads [1].PlayOneShot (clipBoneBreak [i]);
			}
		}
	}

	public void _playClipBreakVehicle ()
	{
		if (!muteSound) {
			if (!ads [2].isPlaying) {
				int i = Random.Range (0, 2);
				ads [2].PlayOneShot (clipBeakVehicle [i]);
			}
		}
	}
	public void playClip_fireWork()
	{
		if (!muteSound)
		{
			ads [2].PlayOneShot (clipFireWork);
	    }

	}
	public void SuccessSoundCallBack()
	{
		if (!muteSound)
		{
			ads [2].PlayOneShot (SuccesSound);
		}

	}
}
