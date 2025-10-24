using UnityEngine;
using System.Collections;

public class GearEffectMan : MonoBehaviour {
	public static GearEffectMan Instance;
	public CreateObjectPooling Ob;
	void Awake(){
		Instance = this;
	}
	public void play(Vector3 pos){
		Ob._ChooseEffect (pos);
	}
}
