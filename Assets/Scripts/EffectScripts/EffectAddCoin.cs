using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class EffectAddCoin : MonoBehaviour {
	public static EffectAddCoin Instance;
	void Awake(){
		if(Instance == null)
			Instance = this;
	}
	public PanelAnimator anim;
	public Text textCoin;
	public void showCoin(int coin){
		textCoin.text = "+ " + coin;
		anim.gameObject.SetActive (true);
		anim.show ();
	}
}
