using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine.UI;

public class CameraEffectBeforeGame : MonoBehaviour
{
	public Camera cameraMain;
	public float sizeCamOut, sizeCamIn;
	Vector3 posZoomOut, posZoomIn;
	bool zoomOut = false;
	bool zoomIn = false;
	public float speedZoom = 100f;
	public ProCamera2D procam;
	public Button btnZoomOut;
	public Button btnDone;

	void Awake ()
	{
		zoomOut = false;
		zoomIn = false;
		btnZoomOut.onClick.AddListener (delegate {
			StartZoomOut ();
		});
		btnDone.onClick.AddListener (delegate {
			StartZoomIn ();
		});
		btnDone.gameObject.SetActive (false);
	}

	void Update ()
	{
		if (zoomOut) {
			this.transform.position = Vector3.Lerp (this.transform.position, posZoomOut, Time.deltaTime * speedZoom);
			float sizeNew = Mathf.Lerp (cameraMain.orthographicSize, sizeCamOut, Time.deltaTime * speedZoom);
			cameraMain.orthographicSize = sizeNew;
			zoomOut = !checkZoomComplete (posZoomOut);
			if (!zoomOut) {
				this.EndZoomOut ();
			}
		}
//		if (Input.GetKeyDown (KeyCode.Z)) {
//			StartZoomOut ();
//		}
//		if(Input.GetKeyDown(KeyCode.I)){
//			StartZoomIn ();
//		}
		if (zoomIn) {
			this.transform.position = Vector3.Lerp (this.transform.position, posZoomIn, Time.deltaTime * speedZoom);
			float sizeNew = Mathf.Lerp (cameraMain.orthographicSize, sizeCamIn, Time.deltaTime * speedZoom);
			cameraMain.orthographicSize = sizeNew;
			zoomIn = !checkZoomComplete (posZoomIn);
			if (!zoomIn)
				this.EndZoomIn ();
		}
	}

	public void EndZoomOut ()
	{
		btnDone.gameObject.SetActive (true);
	}

	public void EndZoomIn ()
	{
		btnZoomOut.gameObject.SetActive (false);
		btnDone.gameObject.SetActive (false);
		GUIManager.Instance.panelUIGPL.SetActive (true);
		PlayerControlManager.Instance.ModeReivew (true);
	}

	public void StartZoomOut ()
	{
		GUIManager.Instance.panelUIGPL.SetActive (false);
		PlayerControlManager.Instance.ModeReivew (false);
		btnZoomOut.gameObject.SetActive (false);
		sizeCamIn = cameraMain.orthographicSize;
		posZoomIn = this.transform.position;
		procam.enabled = false;
		zoomOut = true;
		//cameraMain.orthographicSize = sizeCamOut;
	}

	public void StartZoomIn ()
	{
		btnDone.gameObject.SetActive (false);
		zoomOut = false;
		zoomIn = true;
	}

	public void setInfo (Vector3 posZoomOut, float size)
	{
		this.posZoomOut = new Vector3 (posZoomOut.x, posZoomOut.y, this.transform.position.z);
		sizeCamOut = size;
	}

	public bool checkZoomComplete (Vector3 target)
	{
		float distance = Vector3.Distance (this.transform.position, target);
		if (distance <= 0.5f) {
			return true;
		} else {
			return false;
		}
	}
}
