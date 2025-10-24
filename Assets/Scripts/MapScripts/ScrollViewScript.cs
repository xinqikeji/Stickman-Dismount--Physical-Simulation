using UnityEngine;
using System.Collections;

public class ScrollViewScript : MonoBehaviour {
	public RectTransform rectContent;
	public float min,max;
	void Update(){
		rectContent.localPosition = new Vector3 (this.rectContent.localPosition.x,
			Mathf.Clamp(this.rectContent.localPosition.y,min,max),
			this.rectContent.localPosition.z);
	}
}
