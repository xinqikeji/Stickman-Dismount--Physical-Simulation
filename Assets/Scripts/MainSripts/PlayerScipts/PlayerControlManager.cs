using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Com.LuisPedroFonseca.ProCamera2D;

[System.Serializable]
public class PoseType
{
	public DirectionAddForce directionAddForce;
	public GameObject playerPref;
}

public class PlayerControlManager : MonoBehaviour
{
	public static PlayerControlManager Instance;
	public Image progressBar;
	public float durationClick;
	bool pressing = true;
	//bool isPress = false;
	float timer = 0;
	bool isUp = false, isDown = false;
	[HideInInspector]
	public PlayerMovement playerMovement;
	[HideInInspector]
	public VehiclesMovement vehiclesMovement;
	public GameObject btnPlay, btnReset,btnView;
	//	bool playing = false;
	GameObject curPlayerPref;
	public Transform transPlay;
	public ProCamera2D procamera;
	int currentIndexPose = 0, maxIndexPose;
	DirectionAddForce curDirection;
	public PoseType[] POSE;
	public bool isVehicles = false;
	public VehiclesManager vehiclesManager;
	public int countads = 0;

	public void ModeReivew(bool status){
		btnPlay.SetActive (status);
		btnReset.SetActive (status);
		if (status) {
			btnReset.SetActive (false);
		}
	}
	void Awake ()
	{
		Physics2D.IgnoreLayerCollision (0, 0, false);
		if (Instance == null)
			Instance = this;
	}

	void Start ()
	{
		currentIndexPose = 0;
		maxIndexPose = this.POSE.Length;
		//	isPress = false;
		if (!isVehicles) {
			this.createNewPlayer (this.POSE [currentIndexPose], 0);
		} else {
			this.createWithVehicles (0);
		}
		this.offDirection ();
	}


	public void createWithVehicles (float duration)
	{
		this.pressing = false;
		this.progressBar.fillAmount = 0;
		this.vehiclesMovement = vehiclesManager.create_vehiclesMovement ();
		this.vehiclesMovement._Start ();
		procamera.AddCameraTarget (vehiclesMovement.playerMovenent.transform, 1, 1, duration, new Vector2 (0f, 0f));
		GUIManager.Instance._PanelUIGPLStatus (true);
	}

	public void createNewPlayer (PoseType pose, float duration)
	{
		this.curPlayerPref = pose.playerPref;
		this.curDirection = pose.directionAddForce;
		this.pressing = false;

		this.progressBar.fillAmount = 0;
		GameObject player = Instantiate (curPlayerPref) as GameObject;
		player.transform.position = transPlay.position;
		playerMovement = player.GetComponent<PlayerMovement> ();
		playerMovement._Start ();
		playerMovement.directionI = this.curDirection;
		playerMovement.transform.position = curDirection.PosStart.position;
		playerMovement.power = curDirection.power;
		procamera.AddCameraTarget (player.transform, 1, 1, duration, new Vector2 (0f, 0f));
		GUIManager.Instance._PanelUIGPLStatus (true);

	}

	float value;

	void Update ()
	{
		if (pressing) {
			timer += Time.deltaTime;
			if (isUp) {
				value = timer / durationClick;
			}
			if (isDown) {
				value = (durationClick - timer) / durationClick;
			}
			if (value > 1.15f) {
				timer = 0;
				isUp = false;
				isDown = !isUp;
			}
			if (value < -0.15f) {
				timer = 0;
				isDown = false;
				isUp = !isDown;
			}
			this.progressBar.fillAmount = value;
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			this.click_Reset ();
		}
	}

	public void OnStartPress ()
	{
		GUIManager.Instance._PanelUIGPLStatus (false);
		if (btnView.activeInHierarchy) {
			btnView.SetActive (false);
		}
		this.isUp = true;
		this.isDown = !isUp;
		this.pressing = true;
		this.timer = 0;
	}

	public void addTargetToCamera (Transform target)
	{
		this.procamera.RemoveAllCameraTargets ();
		this.procamera.AddCameraTarget (target, 1, 1, 0, new Vector2 (0f, 0f));
	}

	public void OnFinishPress ()
	{
		//isPress = false;
		this.pressing = false;
		float ratio = this.progressBar.fillAmount;
		if (ratio > 1) {
			ratio = 1;
		}
		if (ratio < 0) {
			ratio = 0;
		}

		BadLogic.playing = true;

		this.btnPlay.SetActive (false);
		this.btnReset.SetActive (true);
//		this.StartCoroutine (delay_pressStart ());
		print ("ratio = " + ratio);

		if (!isVehicles) {
			this.playerMovement._ADDFORCE (ratio);
		} else {
			vehiclesMovement._MoveVehicles (ratio);
		}
	}
	//
	//	IEnumerator delay_pressStart ()
	//	{
	//		yield return new WaitForSecondsRealtime (1f);
	//	}

	public void click_Reset ()
	{
		this.StatusCam (true);
		progressBar.gameObject.SetActive (true);
		if (MeteorScript.Instance != null) {
			MeteorScript.Instance.RemoveMetes ();
			MeteorScript.Instance._Start ();
		}
		if (ArrowManager.Instance != null) {
			ArrowManager.Instance.RessetArrow ();
		}
		GUIManager.Instance.disable_txtCountdown ();
		ScoreManager.Instance._resetScore ();
		procamera.RemoveAllCameraTargets (0);
		if (!isVehicles) {
			if (vehiclesMovement != null) {
				vehiclesMovement.RemoveOb ();
			}
			if (playerMovement != null) {
				Destroy (playerMovement.gameObject);
			}
			this.createNewPlayer (this.POSE [currentIndexPose], 0.5f);
		} else {
			if (vehiclesMovement != null) {
				vehiclesMovement.RemoveOb ();
			}
			if (playerMovement != null) {
				Destroy (playerMovement.gameObject);
			}
			this.createWithVehicles (0.5f);
		}
		this.btnPlay.SetActive (true);
		this.btnReset.SetActive (false);
		GUIManager.Instance.hideButtonEject ();
		countads += 1;
		if (countads >= 5) 
		{
			//ads
//			if (AdsController.Instance != null) {
//				AdsController.Instance.ShowInterstitial ();
				countads = 0;
		}

	}

	void _choosePose ()
	{
		procamera.RemoveAllCameraTargets (0);
		if (!isVehicles) {
			Destroy (playerMovement.gameObject);
			this.createNewPlayer (this.POSE [currentIndexPose], 0);
			this.offDirection ();
		} else {
			vehiclesMovement.RemoveOb ();
			this.createWithVehicles (0.5f);
		}
	}

	public void PrevPose ()
	{
		if (!isVehicles) {
			if (currentIndexPose > 0) {
				currentIndexPose -= 1;
			} else {
				currentIndexPose = (maxIndexPose - 1);
			}
			this._choosePose ();
		} else {
			this.vehiclesMovement.PrevPose ();
		}
	}

	public void NextPose ()
	{
		if (!isVehicles) {
			if (currentIndexPose < (maxIndexPose - 1)) {
				currentIndexPose += 1;
			} else {
				currentIndexPose = 0;
			}
			this._choosePose ();
		} else {
			this.vehiclesMovement.NextPose ();
		}

	}

	public void offDirection ()
	{
		for (int i = 0; i < maxIndexPose; i++) {
			if (i != currentIndexPose) {
				POSE [i].directionAddForce.gameObject.SetActive (false);
			} else {
				POSE [i].directionAddForce.gameObject.SetActive (true);
			}
		}
	}

	public void __OnClickEject ()
	{
		vehiclesMovement._Eject ();
		GUIManager.Instance.hideButtonEject ();
	}

	public void _OnEndGame ()
	{
		btnReset.SetActive (false);
		progressBar.gameObject.SetActive (false);
	}

	public void StatusCam (bool status)
	{
		procamera.enabled = status;
	}
	public void OutVehicle(){
		vehiclesMovement.OutVehicle ();
	}
}
