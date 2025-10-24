using UnityEngine;
using System.Collections;

public class MeteoChil : MonoBehaviour {
	public EffectTNT tnt;
	// Use this for initialization
	void OnCollisionEnter2D(Collision2D coll){
			tnt.transform.SetParent (null);
			tnt.transform.rotation = Quaternion.Euler (Vector3.zero);
			tnt.Play ();
			Destroy (this.gameObject);
	}
}
