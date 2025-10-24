using UnityEngine;
using System.Collections;

public class SlowMotionManager : MonoBehaviour
{
	public static SlowMotionManager Instance;
	int countSlowOnHitLate, countSlowOnHit;
	private bool slowOnHit = false;
	public float realtimeCDOnHit, timeSacleOnHit, timeScale;
	void Start(){
		Instance = this;
	}
	public void _SlowOnHit ()
	{
		countSlowOnHit += 1;
		if (!slowOnHit) {
			countSlowOnHitLate = countSlowOnHit;
			slowOnHit = true;
			StartCoroutine (delay_SlowOnHit (realtimeCDOnHit));
			//StartCoroutine (delay_SlowOnHitFix ());
		}
	}

	IEnumerator delay_SlowOnHit (float time)
	{
		yield return new WaitForSecondsRealtime (time);
		if (countSlowOnHitLate == countSlowOnHit) {
			countSlowOnHit = 0;
			countSlowOnHitLate = 0;
			slowOnHit = false;
		} else {
			countSlowOnHitLate = countSlowOnHit;
			StartCoroutine (delay_SlowOnHit (realtimeCDOnHit));
		}
	}
	void Update ()
	{
		if (!BadLogic.pause) {
			if (slowOnHit) {
				Time.timeScale = timeSacleOnHit;
		
			} else {
				Time.timeScale = timeScale;
			}
			Time.fixedDeltaTime = 0.02F * Time.timeScale;
		}
	}
}
