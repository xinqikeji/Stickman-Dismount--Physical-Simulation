using UnityEngine;
using System.Collections;
public class Intro : MonoBehaviour
{
	public VehiclesMovement vehiclesMovement;

	void Awake ()
	{
		Time.timeScale = 1;
		vehiclesMovement._Start ();
		StartCoroutine (_delay ());
	}
	IEnumerator _delay(){
		yield return new WaitForSecondsRealtime (0.1f);
		vehiclesMovement._MoveVehicles (1f);
	}

}
