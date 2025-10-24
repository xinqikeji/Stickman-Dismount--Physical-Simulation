using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HomeScripts : MonoBehaviour 
{
	public Text VersionText;
	void Awake()
	{
		VersionText.text="Version "+Application.version;

	}
	public void ShowLeaderboard()
	{
	
	}
}


