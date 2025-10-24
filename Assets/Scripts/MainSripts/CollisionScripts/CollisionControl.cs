using UnityEngine;
using System.Collections;

public class CollisionControl : MonoBehaviour
{
	public string key;
	public PlayerMovement movement;
	public FileSetup SETUP;
	SpriteRenderer sprMain;
	public bool HidenBody = false;
	HingeJoint2D hingeJoint2D;
	public float maxForce;
	public SpriteRenderer sprStatus, sprHit;
	Color32 colorStatus;
	public bool isSlow = false;
	public float[] angleNormal = new float[2];
	public float[] angleOnJump = new float[2];
	float[] angleIdleWithVehicle = new float[2];
	bool withVehicles = false;
	bool intro = false;

	void Awake ()
	{
		this.hingeJoint2D = this.GetComponent<HingeJoint2D> ();
		movement = this.GetComponentInParent<PlayerMovement> ();
		this.withVehicles = movement.withVehicles;

		if (this.name == "spine") {
			angleNormal [1] += 15;
		} else if (this.name == "spine (1)") {
			angleNormal [1] += 20;
		} else if (this.name == "spine (2)") {
			angleNormal [1] += 30;
		}
		if (!withVehicles) {
			this._setAngleNormal ();
		} else {
			float baseAngle = hingeJoint2D.jointAngle;
			this.angleIdleWithVehicle [0] = baseAngle - 10f;
			this.angleIdleWithVehicle [1] = baseAngle + 10f;
			this.SetLimitStartWithVehicles ();
		}

	}

	void SetLimitStartWithVehicles ()
	{
		angleLimit.min = angleIdleWithVehicle [0];
		angleLimit.max = angleIdleWithVehicle [1];
		this.hingeJoint2D.limits = angleLimit;
	}

	Color colorBase;

	IEnumerator delay_changeRotation ()
	{
		yield return new WaitForSeconds (0.25f);
		hingeJoint2D.anchor = this.transform.InverseTransformPoint (this.GetComponent<LegFix> ().AnchorHingeJoint2d.position);
	}

	public void _setAngleNormal ()
	{
		angleLimit.min = angleNormal [0];
		angleLimit.max = angleNormal [1];
		this.hingeJoint2D.limits = angleLimit;
	}

	public void _setAngleOnJump ()
	{
		angleLimit.min = angleOnJump [0];
		angleLimit.max = angleOnJump [1];
		this.hingeJoint2D.limits = angleLimit;
	}

	void Start ()
	{
		sprMain = this.GetComponent<SpriteRenderer> ();
		if (!HidenBody) {
			colorBase = SETUP.colorNormal [0];
		} else {
			colorBase = SETUP.colorNormal [1];
		}
		sprMain.color = colorBase;
		colorStatus = sprStatus.color;
		colorStatus.a = 0;
		sprHit.enabled = false;
		this.sprStatus.color = this.colorStatus;
		sprStatus.enabled = true;

		if (this.name == "head") {
			this.transform.Find ("spine").GetComponent<SpriteRenderer> ().color = colorBase;
		}
		intro = (GUIManager.Instance != null) ? false : true;
	}

	float totalTemp;
	JointAngleLimits2D angleLimit;



	void Update ()
	{
		totalTemp = this.hingeJoint2D.reactionForce.magnitude;
	}

	bool changeLimited = false;
	bool showbone = false;
	bool blockdame = false;
	float x, y;

	public void _TakeDame ()
	{
		if (movement.dame) {
			totalTemp = 0;
			if (!blockdame) {
				this.blockdame = true;
				if (totalTemp < this.maxForce) {
					this.colorStatus.a = (byte)((totalTemp / this.maxForce) * 255f);
					if (totalTemp > 200) {
						if (!intro)
							ScoreManager.Instance.SetScore ((int)(totalTemp * 0.1f));
					}
					if (totalTemp > 600) {
					}
				} else {
					this.colorStatus.a = 255;
					if (!intro)
						ScoreManager.Instance.SetScore ((int)(totalTemp * 0.2f));
				}
				showbone = (this.colorStatus.a >= 220) ? true : false;
				this._switchColor ();
				this.movement._takeDameALL ();
			}
		}
	}

	void OnCollisionEnter2D (Collision2D coll)
	{
		if (movement.dame) {
			totalTemp = 0;
			if (coll.gameObject.layer.ToString () != "Player") {
				if (!blockdame) {
					this.blockdame = true;
					totalTemp = this.hingeJoint2D.reactionForce.magnitude;
					if (totalTemp < this.maxForce) {
						this.colorStatus.a = (byte)((totalTemp / this.maxForce) * 255f);
						if (totalTemp > 200) {
							if (!intro)
								ScoreManager.Instance.SetScore ((int)(totalTemp * 0.1f));
						}
					
					} else {
						this.colorStatus.a = 255;
						if (!intro)
							ScoreManager.Instance.SetScore ((int)(totalTemp * 0.2f));
					}
					if (totalTemp > 1000000) {
						this.movement.showBoneALL ();
					}
					showbone = (this.colorStatus.a >= 220) ? true : false;
					if (this.colorStatus.a >= 180) {
						if (!intro) {
							DustManager.Instance.playBlood (coll.contacts [0].point);
						} else {
							DustManager.Instance.playBlood (this.transform.position);
						}
					}
					if (this.colorStatus.a >= 120) {
						if (coll.transform.tag == "PLATFORM") {
							if (!intro) {
								DustManager.Instance.play (coll.contacts [0].point);
								SoundScript.Instance._PlayClipHitBody ();
							} else {
								DustManager.Instance.playBlood (this.transform.position);
							}
						}
					}
					this._switchColor ();
				}
			}
		}
	}

	public void _switchColor ()
	{
		if (!isShowbomeAll) {
			this.StartCoroutine (this.delay_switchColor ());
		}
	}

	IEnumerator delay_switchColor ()
	{
		if (showbone) {
			this._setAngleNormal ();
			movement.countTimeBone += 1;
			if (!intro) {
				SlowMotionManager.Instance._SlowOnHit ();
				SoundScript.Instance._PlayClipBoneBreak ();
			}
		}
		this.sprStatus.color = colorStatus;
		if (showbone) {
			this.sprHit.enabled = true;
		}
		yield return new WaitForSeconds (0.1f);
		this.colorStatus.a = (byte)(this.colorStatus.a / 2);
		this.sprStatus.color = colorStatus;
		yield return new WaitForSeconds (0.1f);
		this.colorStatus.a = 0;
		this.sprStatus.color = colorStatus;
		if (showbone) {
			sprHit.enabled = false;
			showbone = false;
		}
		this.blockdame = false;
	}

	bool isShowbomeAll = false;

	public void showBoneAll ()
	{
		this.isShowbomeAll = true;
		this.blockdame = true;
		this.sprHit.enabled = true;
		colorStatus.a = 255;
		sprStatus.color = colorStatus;
		StartCoroutine (HideBone ());
	}
	void OnCollisionStay2D(Collision2D coll){
		if (coll.gameObject.layer.ToString() == "VEHICLES") {
			if (!intro)
				PlayerControlManager.Instance.OutVehicle ();
		}
	}
	IEnumerator HideBone ()
	{
		yield return new WaitForSeconds (1f);
		this.blockdame = false;
		sprHit.enabled = false;
		this.colorStatus.a = 0;
		this.sprStatus.color = colorStatus;
		this.isShowbomeAll = false;
		if (movement.isShowBoneall) {
			this.movement.isShowBoneall = false;
		}
	}
}
