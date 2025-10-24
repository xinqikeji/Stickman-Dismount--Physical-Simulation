using UnityEngine;
using System.Collections;

public class EffectTNT : MonoBehaviour {
	public ParticleSystem parS;
	public AudioSource ads;
	public CircleCollider2D circleCollider2D;
	bool playing = false;
	public float maxRadius;
	public void Play(){
		//SlowMotionManager.Instance._SlowOnHit ();
		parS.Clear ();
		parS.Play ();
		ads.Play ();
		playing = true;
		circleCollider2D.enabled = true;
		StartCoroutine (_delay ());
	}
	IEnumerator _delay(){
		yield return new WaitForSecondsRealtime (5f);
		Destroy (this.gameObject);
	}
	void FixedUpdate(){
		if (playing) {
			if (circleCollider2D.radius < maxRadius) {
				circleCollider2D.radius += 0.1f;
			} else {
				circleCollider2D.enabled = false;
				playing = false;
			}
		} else {
			if (circleCollider2D.enabled) {
				circleCollider2D.enabled = false;
			}
		}
	}
}
