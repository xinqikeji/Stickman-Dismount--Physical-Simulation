using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {
	public GameObject[] Maps;
	int indexMap;
	public CameraEffectBeforeGame cameraEffect;
	void Awake () {
		indexMap = BadLogic.nameMap;
		GameObject map = Instantiate (Maps [indexMap]) as GameObject;
		map.transform.position = Vector3.zero;
		MapConfigScript tempMapConfig = map.GetComponent<MapConfigScript> ();
		cameraEffect.setInfo (tempMapConfig._Center.position, tempMapConfig.sizeCam);
	}
}
