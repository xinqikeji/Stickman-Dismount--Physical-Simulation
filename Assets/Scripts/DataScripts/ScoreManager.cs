using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

[System.Serializable]
public class SCORE
{
	public float value;
	public float times;
	public int index;
}

public class ScoreManager : MonoBehaviour
{
	public static ScoreManager Instance;
	public ScoreEndGame _scoreUI;
	public Text txtScore, txtBestScore;
	public int curScore;
	public SCORE CoinEarned, DamageScore, AirTimeScore, FlipScore, BoneBreakingScore, VehicleBreakingScore;
	SCORE[] SCORES;
	int bestScore;
	string[] texts;

	int coinEarned = 0;
	int totalScore = 0;

	void Start ()
	{
		Instance = this;
		this.SCORES = new SCORE[6];
		bestScore = PlayerPrefs.GetInt (BadLogic.nameMap + "最佳得分");
		txtBestScore.text = "最佳:" + bestScore;
		texts = new string[6];
	}

	public void MergeScores ()
	{
		SCORES [0] = CoinEarned;
		SCORES [1] = DamageScore;
		SCORES [2] = AirTimeScore;
		SCORES [3] = FlipScore;
		SCORES [4] = BoneBreakingScore;
		SCORES [5] = VehicleBreakingScore;
	}

	public void SetScore (int score)
	{
		curScore += score;
		txtScore.text = "" + curScore;
	}

	public void _resetScore ()
	{
		this.coinEarned = 0;
		this.totalScore = 0;
		this.curScore = 0;
		this.txtScore.text = "0";
	}

	public void setScore ()
	{
		_scoreUI.SetScore (texts);
	}

	public bool checkScore ()
	{
		if (curScore > 0) {
			return true;
		} else {
			return false;
		}
	}
	private void SaveNewBestScore ()
	{
		txtBestScore.text = "最佳:" + bestScore;
		PlayerPrefs.SetInt (BadLogic.nameMap + "BESTSCORE", bestScore);
		PlayerPrefs.Save ();
	}

	public void setTimeAir (float timeAir)
	{
		timeAir = (float)Math.Round (timeAir, 2);
		AirTimeScore.times = timeAir;
		float temp = timeAir * AirTimeScore.value;
		texts [AirTimeScore.index] = "" + Mathf.RoundToInt (temp) + " (" + timeAir + " 秒)";
		totalScore += Mathf.RoundToInt (temp);
	}

	public void SetBoneBreakingScore (float time)
	{
		float temp = BoneBreakingScore.value * time;
		texts [BoneBreakingScore.index] = "" + Mathf.RoundToInt (temp) + " (" + time + " 次)";
		totalScore += Mathf.RoundToInt (temp);
	}

	public void SetFlipScore (float time)
	{
		float temp = FlipScore.value * time;
		texts [FlipScore.index] = "" + Mathf.RoundToInt (temp) + " (" + time + " 翻转)";
		totalScore += Mathf.RoundToInt (temp);
	}

	public void SetBreakVehicle (float time)
	{
		float temp = VehicleBreakingScore.value * time;
		texts [VehicleBreakingScore.index] = "" + Mathf.RoundToInt (temp) + " (" + time + " 次)";
		totalScore += Mathf.RoundToInt (temp);
	}

	public void setDameScore ()
	{
		
		texts [DamageScore.index] = "" + curScore;
		totalScore += curScore;

	}

	public void SetScoreALL (float timeAir, float timeBone, float timeFlip, float timeVehicle)
	{
		Debug.Log ("Total Score: "+ totalScore);
		this.setDameScore ();
		this.setTimeAir (timeAir);
		this.SetBoneBreakingScore (timeBone);
		this.SetFlipScore (timeFlip);
		this.SetBreakVehicle (timeVehicle);
		this.coinEarned = Mathf.RoundToInt (totalScore / 100);
		texts [CoinEarned.index] = "" + coinEarned;
		CoinManager.coin += coinEarned;
		CoinManager.UpCoin ();
		GUIManager.Instance.UpdateTextCoin ();
		this.txtScore.text = "" + totalScore;
		if (totalScore > bestScore) 
		{
			bestScore = totalScore;
			this.SaveNewBestScore ();
		}
		this.setScore ();
		MissionControl.Instance.SetCurValue (timeBone, timeFlip, timeAir);
	}
}
