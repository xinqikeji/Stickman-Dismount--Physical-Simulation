using UnityEngine;
using System.Collections;

public class bgAutomove : MonoBehaviour {
	public Transform target;
	public float smoother;
	void Start () {
		
	}
	void FixedUpdate () {
		Vector3 temp = new Vector3(target.position.x,target.position.y,0);
		this.transform.position = Vector3.Slerp (this.transform.position, temp, smoother*Time.deltaTime);
	}
}
