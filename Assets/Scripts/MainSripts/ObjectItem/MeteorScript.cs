using UnityEngine;
using System.Collections;

public class MeteorScript : MonoBehaviour
{
	public static MeteorScript Instance;
	public Transform[] pointSpawn;
	public GameObject meteo;
	public int number;
	GameObject[] metes;
	float timer;
	public float secondsDrop;
	Transform target;

	void Start ()
	{
		Instance = this;
		this._Start ();
	}

	public void RemoveMetes ()
	{
		foreach (GameObject go in metes) {
			if (go != null) {
				Destroy (go);
			}
		}
	}

	public void _Start ()
	{
		target = Camera.main.transform;
		metes = new GameObject[number];
		for (int i = 0; i < number; i++) {
			GameObject go = Instantiate (meteo) as GameObject;
			metes [i] = go;
			metes [i].SetActive (false);
		}
	}

	void Update ()
	{
		if (BadLogic.playing) {
			timer += Time.deltaTime;
			if (timer >= secondsDrop) {
				timer = 0;
				this._DropMeteo ();
			}
		}
	}

	void _DropMeteo ()
	{
		int i = Random.Range (0, pointSpawn.Length);
		foreach (GameObject go in metes) {
			if (go != null) {
				if (!go.activeInHierarchy) {
					go.transform.position = pointSpawn [i].position;
					go.SetActive (true);
					break;
				}
			}
		}
	}
}
