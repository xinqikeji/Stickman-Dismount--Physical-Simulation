using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonTwoSprite : MonoBehaviour {
	public Sprite[] sprs;
	public Image img;
	public void _Status(bool status){
		int i = (status) ? 0 : 1;
		img.sprite = sprs [i];
	}
}
