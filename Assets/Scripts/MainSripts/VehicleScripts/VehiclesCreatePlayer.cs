using UnityEngine;
using System.Collections;

public class VehiclesCreatePlayer : MonoBehaviour {
	public GameObject[] _PlayerPref;
	public Transform[] posSpawn;
    public GameObject _Player(int index)
    {
        GameObject temp = Instantiate(_PlayerPref[index], posSpawn[index].position, _PlayerPref[index].transform.rotation) as GameObject;
        return temp;
    }
}
