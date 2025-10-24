using UnityEngine;
using System.Collections;

[System.Serializable]
public class MISSION
{
	public string infoMission;
	public int coinCompleted;
	public int valueComplete;
}

public class MissionControl : MonoBehaviour
{
	public static MissionControl Instance;
	public MISSION missionBreakBone;
	public MissionManager missionMan;
	public string infoMission;
	public string status;
	public int coinComplete;
	int valueComplete;
	float curValue;
	public int indexMission;
	MISSION tempMission = new MISSION ();
	bool completed = false;
	void Awake ()
	{
		Instance = this;
		curValue = 0;
		this.RefeshMission ();
		this.setInfoMission ();
		this.missionMan.TickNormal ();
	}

	public void showMission ()
	{
		this.missionMan.ShowMission ();
		this.StartCoroutine (delay_hideMission ());
	}

	public void setInfoMission ()
	{
		this.missionMan.setInfoMission (coinComplete.ToString (), infoMission, status);
	}

	public void hideMission ()
	{
		this.missionMan.HideMission ();
	}

	IEnumerator delay_hideMission ()
	{
		yield return new WaitForSecondsRealtime (3f);
		this.hideMission ();
	}

	public void ShowMissioEndgame ()
	{
		this.missionMan.showMissionOnEndGame ();
	}

	public void SetCurValue (float boneBreak, float flipTime, float timeAir)
	{
		if (indexMission == 1) {
			curValue += boneBreak;
		} else if (indexMission == 2) {
			curValue += flipTime;
		} else if (indexMission == 3) {
			curValue += timeAir;
		}
		this.checkMissionComplete ();
	}

	public void checkMissionComplete ()
	{
		bool temp = false;
		if (Mathf.RoundToInt (curValue) >= valueComplete)
		{
			DataManager.Instance.AddMissions (1);
			// Refesh mission
			Debug.Log("Mission Complete");
			CoinManager.coin += tempMission.coinCompleted;
			CoinManager.UpCoin ();
			GUIManager.Instance.UpdateTextCoin ();
			this.missionMan.TickCompleted ();
			temp = true;
		} else 
		{
			// not complete
			Debug.Log("Mission not complete");
			temp = false;
		}
		this.status = "(" + Mathf.RoundToInt (curValue) + "/" + tempMission.valueComplete + ")";
		this.missionMan.txtStatic.text = "" + this.status;

		if (temp) {
			this.RefeshMission ();
		}
		completed = temp;
	}

	public void RefeshMission ()
	{
		this.indexMission = Random.Range (1, 4);
		if (indexMission == 1) {
			// Bone
			valueComplete = Random.Range (12, 50);
			tempMission.valueComplete = valueComplete;
			tempMission.infoMission = "骨折 " + valueComplete + " 次!";
			tempMission.coinCompleted = Random.Range (50, 250);
		} else if (indexMission == 2) {

			// Flip
			valueComplete = Random.Range (10, 30);
			tempMission.valueComplete = valueComplete;
			tempMission.infoMission = "翻滚 " + valueComplete + " 次!";
			tempMission.coinCompleted = Random.Range (60, 300);
			
		} else if (indexMission == 3) {
			// Air	
			valueComplete = Random.Range (8, 30);
			tempMission.valueComplete = valueComplete;
			tempMission.infoMission = "空中 " + valueComplete + " 秒!";
			tempMission.coinCompleted = Random.Range (100, 360);
		}

		this.curValue = 0;
		this.infoMission = tempMission.infoMission;
		this.coinComplete = tempMission.coinCompleted;
		this.valueComplete = tempMission.valueComplete;
		this.status = "(" + Mathf.RoundToInt (curValue) + "/" + tempMission.valueComplete + ")";
	}

	public void resetsaukhiHoanthanh ()
	{
		if (completed) {
			this.setInfoMission ();
			this.missionMan.TickNormal ();
			completed = false;
		}
	}
}
