using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public Rigidbody2D rg2d;
	public float power;
	[HideInInspector]
	public DirectionAddForce directionI;
	public Rigidbody2D[] rg2dchils;
	float ratio;
	bool checking = false;
	public bool changeLimit = false;
	public bool I_II;
	public Transform pointStart;
	bool setOnSidedown = false;
	bool jump = false;
	public bool withVehicles;
	public bool contactRg;
	CollisionControl[] collisionControl;
	public bool localEject = false;
	public float powerLocalEject;
	float powerEject;
	public bool dame = false;
	bool intro = false;
	public float angleMove;

	void Awake ()
	{
		this.collisionControl = new CollisionControl[rg2dchils.Length];
		for (int i = 0; i < rg2dchils.Length; i++) {
			this.collisionControl [i] = rg2dchils [i].GetComponent<CollisionControl> ();
		}
		if (!localEject) {
			powerEject = BadLogic.powerEject;
		} else {
			powerEject = powerLocalEject;
		}
		this.changeLimit = false;
	}

	public void _Start ()
	{
		this.transform.position = this.pointStart.position;
		rg2d.isKinematic = true;
		this._KinematicOfRg2dChils (true);
		this.setInertia (0.34f);
		this.checking = false;
		changeLimit = false;
		setOnSidedown = false;
		jump = false;
		dame = true;
		if (GUIManager.Instance != null) {
			intro = false;
		} else {
			intro = true;
		}
		this.angleMove = 0;
	}

	public void setInertia (float value)
	{
		foreach (Rigidbody2D rgTemp in rg2dchils) {
			rgTemp.inertia = value;
		}
	}

	bool addFlipTime = false;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space)) {
			_AddForceAtPosition ();
		}
		if (checking) {
			if (this.rg2d.velocity.magnitude < 1f) {
				timer -= Time.deltaTime;
				if (timer < 2.5f) {
					if (!intro) {
						GUIManager.Instance._Countdown (Mathf.RoundToInt (timer + 1).ToString ());
						PlayerControlManager.Instance.StatusCam (false);
					}
					dame = false;
				}
				if (timer < -0.4f) {
					if (!intro) {
						if (ScoreManager.Instance.checkScore ()) {
							if (withVehicles) {
								ScoreManager.Instance.SetScoreALL (timerAir, (float)countTimeBone, angleMove, PlayerControlManager.Instance.vehiclesMovement.CountBreakVehicles ());
							} else {
								ScoreManager.Instance.SetScoreALL (timerAir, (float)countTimeBone, angleMove, 0);
							}
							BadLogic.playing = false;
							GUIManager.Instance.OnEndGame ();
							GUIManager.Instance.disable_txtCountdown ();
						
						} else {
							GUIManager.Instance._reset ();
						}
					}
					dame = false;
					checking = false;
				}
			} else {
				timer = 3;
				dame = true;
				if (!intro) {
					GUIManager.Instance.disable_txtCountdown ();
					PlayerControlManager.Instance.StatusCam (true);
				}
			}
		}
		if (!withVehicles) {
			if (jump) {
				if (!setOnSidedown) {
					if (rg2d.velocity.magnitude > 10f) {
						setOnSidedown = true;
						this.setInertia (0.05f);
						this.setAllRgToJump ();
					}
				}
			}
		}
		float temp = this.transform.eulerAngles.z;
		if (temp > 358) {
			if (!addFlipTime) {
				addFlipTime = true;
				angleMove += 1;
			}
		} else if (temp > 50 && temp < 300) {
			addFlipTime = false;
		}

		if (isAir) {
			timerAir += Time.deltaTime;
//			Debug.Log ("Time Air: "+ timerAir);
		}

	}

	public float timerAir = 0;
	public bool isAir = false;

	void OnTriggerEnter2D (Collider2D coll)
	{
	}

	void OnTriggerStay2D (Collider2D coll)
	{
		if (coll.tag == "PLATFORM") {
			this.isAir = false;
		}
		if (!changeLimit) {
			if (coll.tag == "CheckEnd") {
				this.setAllToNormal ();
				this.changeLimit = true;
			}

		}
	}

	void OnTriggerExit2D (Collider2D coll)
	{
		if (coll.tag == "PLATFORM") {
			this.isAir = true;
		}
	}

	void _AddForceAtPosition ()
	{
		checking = true;
		if (rg2d.isKinematic) {
			rg2d.isKinematic = false;
		}
		this._KinematicOfRg2dChils (false);
		this.StartCoroutine (delay__AddForceAtPosition ());
	}

	IEnumerator delay__AddForceAtPosition ()
	{
		yield return new WaitForEndOfFrame ();
		rg2d.AddForceAtPosition (directionI.Direction * power * ratio, directionI.transform.position);
		jump = true;
		timerAir = 0;
		setOnSidedown = false;
	
	}

	public void setAllRgToJump ()
	{
		foreach (CollisionControl temp in collisionControl) {
			temp._setAngleOnJump ();
		}
		this.changeLimit = false;
	}

	public void setAllToNormal ()
	{
		foreach (CollisionControl temp in collisionControl) {
			temp._setAngleNormal ();
		}
	}

	void _KinematicOfRg2dChils (bool status)
	{
		foreach (Rigidbody2D rgTemp in rg2dchils) {
			rgTemp.isKinematic = status;
		}
	}

	public void _ADDFORCE (float ratioTemp)
	{
		ratio = ratioTemp;
		this._AddForceAtPosition ();
	}

	public void _takeDameALL ()
	{
		foreach (CollisionControl coll in collisionControl) {
			coll._TakeDame ();
		}
	}

	float timer = 3;


	public void StartWithVehicles ()
	{
		changeLimit = false;
		checking = true;
		this.rg2d.isKinematic = false;
		this._KinematicOfRg2dChils (false);
		this.setInertia (0.34f);
	}

	public void _Eject (Vector2 direction)
	{
		this.setAllRgToJump ();
		rg2d.AddForce (direction * powerEject);
	}

	public bool isShowBoneall = false;

	public void showBoneALL ()
	{
		if (!isShowBoneall) {
			isShowBoneall = true;
			foreach (CollisionControl coll in collisionControl) {
				coll.showBoneAll ();
			}
		}
	}

	public int countTimeBone = 0;
}
