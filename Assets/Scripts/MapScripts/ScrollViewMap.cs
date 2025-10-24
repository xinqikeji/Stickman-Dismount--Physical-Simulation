using UnityEngine;
using System.Collections;

public class ScrollViewMap : MonoBehaviour {
	public RectTransform rectContent;
	public float minX,maxX;
	void Update(){
		rectContent.localPosition = new Vector3 (Mathf.Clamp(this.rectContent.localPosition.x,minX,maxX),
			this.rectContent.localPosition.y,
			this.rectContent.localPosition.z);
	}
}
