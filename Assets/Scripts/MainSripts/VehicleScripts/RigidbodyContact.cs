using UnityEngine;
using System.Collections;

public class RigidbodyContact : MonoBehaviour {
	public FixedJoint2D[] fixedJoint2DARM;
	public float breakForceARM;
	void Awake(){
		foreach (FixedJoint2D temp in fixedJoint2DARM) {
			temp.breakForce = this.breakForceARM;
		}
	}
}
