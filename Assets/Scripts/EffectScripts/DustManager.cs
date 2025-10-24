using UnityEngine;
using System.Collections;

public class DustManager : MonoBehaviour {
	public static DustManager Instance;
	public CreateObjectPooling obs,Bloods;
	void Start () {
		Instance = this;
	}
	public void play(Vector3 pos){
		obs._ChooseEffect (pos);
	}
	public void playBlood(Vector3 pos){
		Bloods._ChooseEffect (pos);
	}

}
