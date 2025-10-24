using UnityEngine;
using System.Collections;

public class DirectionAddForce : MonoBehaviour {
	public Transform posEnd;
	[HideInInspector]
	public Vector3 Direction;
	public Transform PosStart;
	public float power;
	void Awake(){
		Vector3 temp = posEnd.position - this.transform.position;
		Direction = temp.normalized;
	}
}
