using UnityEngine;
using System.Collections;

public class WheelScript : MonoBehaviour {
	public bool isWheel;
	public WheelJoint2D wheelJoint2D;
	public FixedJoint2D fixedJoint2D;
	bool breaked = false;
	void Awake(){
		breaked = false;
	}
	void Update(){
		if (!breaked) {
			if (isWheel) {
				if (wheelJoint2D == null) {
					breaked = true;
					if (SoundScript.Instance != null) {
						SoundScript.Instance._playClipBreakVehicle ();
						this.playClip ();
					}
					this.transform.SetParent (null);
				}
			} else {
				if (fixedJoint2D == null) {
					if (SoundScript.Instance != null) {
						SoundScript.Instance._playClipBreakVehicle ();
						this.playClip ();
					}
					breaked = true;
					this.transform.SetParent (null);
				}
			}
		}
	}
	public bool checkVehibreak(){
		return breaked;
	}
	public void playClip(){
		GearEffectMan.Instance.play (this.transform.position);
	}
}
