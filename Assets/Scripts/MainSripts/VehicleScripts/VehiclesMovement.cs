using UnityEngine;
using System.Collections;

public class VehiclesMovement : MonoBehaviour
{
	[HideInInspector]
	public PlayerMovement playerMovenent;
	public WheelJoint2D[] wheelJonit2D;
	public Rigidbody2D rg2d;
	public float duration, speedMotor;
	JointMotor2D motor;
	LayerMask maskPlayer, maskVehicles;
	public Transform from_, _to;
	bool ignorePlayer;
	public VehiclesCreatePlayer _CreatePlayer;
	public int currentIndexPose;
	public WheelScript[] wheelScripts;
	public Transform[] directionEject = new Transform[2];
	bool contactARM;
	public Rigidbody2D ShopcartContact;
	public bool isChair = false;
	public bool intro = false;
	public float countBreak = 0;

	void Awake ()
	{
		maskPlayer = LayerMask.NameToLayer ("Player");
		maskVehicles = LayerMask.NameToLayer ("VEHICLES");
		this.ignore_Collision (true);
		currentIndexPose = 0;
		maxIndexPose = this._CreatePlayer.posSpawn.Length;
		duration = 10000;
	}

	public void _Start ()
	{

		rg2d.isKinematic = true;
		foreach (WheelJoint2D wheel in wheelJonit2D) {
			motor.motorSpeed = speedMotor;
			motor.maxMotorTorque = 100000;
			wheel.motor = this.motor;
			wheel.useMotor = false;
		}
		this.createPlayer ();
		this.ignore_Collision (true);
		ignorePlayer = true;
		playerExit = false;
	}

	public void _MoveVehicles (float ratioDuration)
	{
		if (GUIManager.Instance != null) {
			GUIManager.Instance.showButtonEject ();
		}
		rg2d.isKinematic = false;
		playerMovenent.StartWithVehicles ();
		float temp = duration * ratioDuration;
		foreach (WheelJoint2D wheel in wheelJonit2D) {
			if (wheel != null) {
				wheel.useMotor = true;
			}
		}
		this.StartCoroutine (delay_Move (temp));
		if (intro) {
		}
	}

	IEnumerator delay_Move (float time)
	{
		yield return new WaitForSecondsRealtime (time);
		foreach (WheelJoint2D wheel in wheelJonit2D) {
			if (wheel != null) {
				wheel.useMotor = false;
			}
		}
	}

	bool ignore_;

	public void ignore_Collision (bool ignore)
	{
		Physics2D.IgnoreLayerCollision (maskPlayer, maskVehicles, ignore);
		ignore_ = ignore;
	}

	bool playerExit;

	void Update ()
	{
		if (!playerExit) {
			playerExit = !Physics2D.Linecast (from_.position, _to.position, 1 << maskPlayer);
			if (playerExit) {
				ignore_ = false;
			}
		} else {
			if (ignorePlayer) {
				this.ignore_Collision (ignorePlayer = false);
				if (!intro) {
					GUIManager.Instance.hideButtonEject ();
				}
				playerMovenent.setInertia (0.05f);
				playerMovenent.setAllRgToJump ();
			}
			if (!ignorePlayer) {
				if (!ignore_) {
					if (rg2d.IsTouchingLayers (maskPlayer)) {
						this.ignore_Collision (true);
					} else {
						ignore_ = true;
						this.ignore_Collision (false);
					}
				}
			}
		}
	}

	public void createPlayer ()
	{
		playerMovenent = _CreatePlayer._Player (currentIndexPose).GetComponent<PlayerMovement> ();
		if (intro) {
			playerMovenent.transform.Rotate (new Vector3 (0, 0, -120f));
		}
		if (playerMovenent.contactRg) {
			this.contactARM = true;
			this._ContactToARM ();
		} else {
			this.contactARM = false;
		}
		playerMovenent._Start ();
	}

	public void RemoveOb ()
	{
		foreach (WheelScript w in wheelScripts) {
			Destroy (w.gameObject);
		}
		Destroy (playerMovenent.gameObject);
		Destroy (this.gameObject);
	}

	public float CountBreakVehicles ()
	{
		foreach (WheelScript w in wheelScripts) {
			if (w.checkVehibreak ())
				countBreak += 1;
		}
		return countBreak;
	}

	public void _Eject ()
	{
		if (contactARM) {
			for (int i = 0; i < rgTemp.fixedJoint2DARM.Length; i++) {
				if (rgTemp.fixedJoint2DARM [i] != null) {
					rgTemp.fixedJoint2DARM [i].connectedBody = null;
					rgTemp.fixedJoint2DARM [i].enabled = false;
				}
			}
		}
		Vector2 directionTemp;
		directionTemp = directionEject [1].position - directionEject [0].position;
		playerMovenent._Eject (directionTemp);
	}

	RigidbodyContact rgTemp;

	public void _ContactToARM ()
	{
		rgTemp = playerMovenent.GetComponent<RigidbodyContact> ();
		if (!isChair) {
			for (int i = 0; i < rgTemp.fixedJoint2DARM.Length; i++) {
				if (currentIndexPose != 3) {
					rgTemp.fixedJoint2DARM [i].connectedBody = this.rg2d;
				} else {
					rgTemp.fixedJoint2DARM [i].connectedBody = this.ShopcartContact;
				}
			}
		} else {
			if (rgTemp != null) {
				for (int i = 0; i < rgTemp.fixedJoint2DARM.Length; i++) {
					rgTemp.fixedJoint2DARM [i].connectedBody = this.ShopcartContact;
				}
			}
		}
	}

	int maxIndexPose;

	public void PrevPose ()
	{
		Destroy (playerMovenent.gameObject);
		if (currentIndexPose > 0) {
			currentIndexPose -= 1;
		} else {
			currentIndexPose = (maxIndexPose - 1);
		}
		this.createPlayer ();
		if (!intro)
			PlayerControlManager.Instance.addTargetToCamera (this.playerMovenent.transform);
	}

	public void NextPose ()
	{
		Destroy (playerMovenent.gameObject);
		if (currentIndexPose < (maxIndexPose - 1)) {
			currentIndexPose += 1;
		} else {
			currentIndexPose = 0;
		}
		this.createPlayer ();
		if (!intro)
			PlayerControlManager.Instance.addTargetToCamera (this.playerMovenent.transform);
	}

	bool showdust = false;
	void OnCollisionEnter2D (Collision2D coll)
	{
		if (!showdust) {
			if (coll.gameObject.tag == "PLATFORM") {
				DustManager.Instance.play (coll.contacts [0].point);
				showdust = true;
				StartCoroutine (delayShowdust ());
			}
		} 
	}

	IEnumerator delayShowdust ()
	{
		yield return new WaitForSeconds (2f);
		showdust = false;
	}
	bool blockOutVehicle = false;
	public void OutVehicle(){
		ignore_ = false;
	}
}
