using UnityEngine;
using System.Collections;

public class TNTScript : MonoBehaviour {
	public EffectTNT effect;
	public float breakForce;
	void Start () {
		effect.transform.SetParent (null);
	}
	void Update () {
		if(Input.GetKeyDown(KeyCode.N)){
			
		}
	}
	private void playEffect(){
		if (!SlowMotionManager.Instance != null) {
			SlowMotionManager.Instance._SlowOnHit ();
		}
		effect.transform.position = this.transform.position;
		effect.Play ();
		this.gameObject.SetActive (false);
	}
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.relativeVelocity.magnitude > breakForce) {
			this.playEffect ();
		}
	}
}
