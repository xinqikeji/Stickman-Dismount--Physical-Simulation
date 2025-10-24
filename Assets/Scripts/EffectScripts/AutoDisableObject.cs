using UnityEngine;
using System.Collections;

public class AutoDisableObject : MonoBehaviour {
	public void disableGameObjectParent(){
		this.transform.parent.gameObject.SetActive (false);
	}
}
