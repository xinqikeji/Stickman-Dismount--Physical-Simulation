using UnityEngine;
using System.Collections;
public class CreateObjectPooling : MonoBehaviour {
	public GameObject pref;
	public GameObject[] objectsPool;
	public int maxnumOb;
	void Awake(){
		Init ();
	}
	public void Init(){
		objectsPool = new GameObject[maxnumOb];
		for (int i = 0; i < maxnumOb; i++) {
			objectsPool [i] = Instantiate (pref) as GameObject;
			objectsPool [i].name = pref.name + i;
			objectsPool [i].SetActive (false);
			objectsPool [i].transform.parent = transform;
		}
	}
	public void _ChooseEffect(Vector3 pos){
		for (int i = 0; i < objectsPool.Length; i++) {
			if (objectsPool[i] != null) {
				if (!objectsPool [i].activeInHierarchy) {
					objectsPool [i].transform.position = pos;
					objectsPool [i].transform.parent = null;
					objectsPool [i].SetActive (true);
					break;
				}
			}
		}
	}
}
