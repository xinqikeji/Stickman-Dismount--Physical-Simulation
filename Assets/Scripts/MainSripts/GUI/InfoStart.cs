using UnityEngine;
using System.Collections;
[System.Serializable]
public class InfoStart_{
	public string key;
	public float angle;
}
public class InfoStart : MonoBehaviour {
	public InfoStart_[] infoStarts;
	float temp;
	public float _getInfo(string key){
		foreach(InfoStart_ info in infoStarts){
			if (info.key == key) {
				temp = info.angle;
				break;
			}
		}
		return temp;
	}
}
