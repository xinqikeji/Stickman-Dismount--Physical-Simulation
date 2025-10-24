using UnityEngine;
using System.Collections;

public class ArrowScript : MonoBehaviour {
	public float moveSpeed;
	public Rigidbody2D rg2d;
	public Transform target;
	Vector3 direction;
	void Start(){
		direction = target.position - this.transform.position;
		direction = direction.normalized;
	}
	void FixedUpdate () {
		rg2d.velocity = (direction * moveSpeed * Time.fixedDeltaTime);
	}
	void OnCollisionEnter2D(Collision2D coll){
//		if (coll.gameObject.tag == "Player") {
//			//
//		}
		if (coll.gameObject.layer.ToString() == "Player" || coll.gameObject.tag == "PLATFORM") {
			Destroy(rg2d);
			this.enabled = false;
			this.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, 10));
			this.transform.SetParent (coll.transform);
		}
	}
}
