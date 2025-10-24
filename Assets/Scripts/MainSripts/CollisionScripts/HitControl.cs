using UnityEngine;
using System.Collections;

public class HitControl : MonoBehaviour {
	ParticleSystem pars;
	void OnEnable(){
		if (pars == null)
			pars = this.GetComponent<ParticleSystem> ();
		if (pars) {
			pars.Play ();
		}
	}
	void OnDisable(){
		pars.Clear ();
	}
	void Update(){
		if (pars.isStopped) {
			this.gameObject.SetActive (false);
		}
	}
}
