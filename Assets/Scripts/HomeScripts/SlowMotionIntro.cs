using UnityEngine;
using System.Collections;

public class SlowMotionIntro : MonoBehaviour {
	public static SlowMotionIntro Instance;
	public bool slow;
	public float duration;
	public float magnitude;
	public PanelAnimator anim;
	void Start(){
		Instance = this;
		this.slow = false;
	}
	void Update () {
		if (!BadLogic.pause) {
			if (slow) {
				Time.timeScale = 0.002f;
				Time.fixedDeltaTime = Time.deltaTime * 0.02f;
			} else {
				if (!locked) {
					Time.timeScale = 1f;
				} else {
					Time.timeScale = 0.001f;
					Time.fixedDeltaTime = Time.deltaTime * 0.02f;
				}
			}
		}
	}
	bool locked = false;
	public void startSlow(){
		if (!locked) {
			locked = true;
			this.Shake ();
//			StartCoroutine (delay ());
			anim.show ();
		}
	}
	IEnumerator delay(){
		yield return new WaitForSecondsRealtime (duration);
		if (!slow) {
			slow = true;
			//	Physics2D.gravity = Vector2.zero;
		}
	
	}
	IEnumerator Shake() {

		float elapsed = 0.0f;

		Vector3 originalCamPos = Camera.main.transform.position;

		while (elapsed < duration) {

			elapsed += Time.deltaTime;          

			float percentComplete = elapsed / duration;         
			float damper = 1.0f - Mathf.Clamp(4.0f * percentComplete - 3.0f, 0.0f, 1.0f);

			// map value to [-1, 1]
			float x = Random.value * 2.0f - 1.0f;
			float y = Random.value * 2.0f - 1.0f;
			x *= magnitude * damper;
			y *= magnitude * damper;

			Camera.main.transform.position = new Vector3(x, y, originalCamPos.z);

			yield return null;
		}

		Camera.main.transform.position = originalCamPos;
	}
}
