using UnityEngine;
using System.Collections;

public class CharracterManager : MonoBehaviour {
	public Sprite[] sprS = new Sprite[5];
	public GameObject headpref;
	public Transform head;
	void Awake(){
		head = this.transform.Find ("head");
	}
	public void createCharractor(int index){
		GameObject temp = Instantiate (headpref) as GameObject;
		temp.transform.parent = this.head;
		temp.transform.localPosition = Vector3.zero;
		temp.GetComponent<SpriteRenderer> ().sprite = sprS [index];
	}
}
