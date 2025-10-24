using UnityEngine;
using System.Collections;

public class SmokeSteamManager : MonoBehaviour
{
	public WheelJoint2D wheel2D;
	public ParticleSystem parS;
	public Transform transCheck;
	LayerMask maskGround;
	bool enablePars = false;
	void Awake ()
	{
		//parS.transform.SetParent (null);
		this.maskGround = LayerMask.NameToLayer ("GROUND");
		this.parS.Stop ();
	}

	void Update ()
	{
		if (Physics2D.Raycast (transCheck.position, Vector2.down, 0.25f, 1 << maskGround)) {
			if (wheel2D != null) {
				if (wheel2D.jointSpeed > 10f) {
					if (!parS.isPlaying) {
						this.parS.Clear ();
						this.parS.Play ();
					}
				}
			} else {
				if (parS.isPlaying) {
					this.parS.Stop ();
				}
			}
		} else {
			if (!enablePars) {
				enablePars = true;
				this.parS.transform.SetParent (null);
				this.StartCoroutine (delay_ ());
			}
		}
	}
	IEnumerator delay_(){
		yield return new WaitForSecondsRealtime (1f);
		this.parS.gameObject.SetActive (false);
	}
}
