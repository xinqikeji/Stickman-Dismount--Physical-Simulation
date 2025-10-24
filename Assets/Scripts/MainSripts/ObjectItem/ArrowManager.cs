using UnityEngine;
using System.Collections;

public class ArrowManager : MonoBehaviour
{
	public static ArrowManager Instance;
	public GameObject go_Arrow;
	float timer;
	Vector3 posStart;
	int count = 0;
	GameObject[] arrows;

	void Start ()
	{
		Instance = this;
		this.posStart = this.transform.position;
		count = 0;
		arrows = new GameObject[100];
	}

	void Update ()
	{
		if (count < 100) {
			if (BadLogic.playing) {
				timer += Time.deltaTime;
				if (timer >= 0.25f) {
					timer = 0;
					this.ShotArrow ();
				}
			}
		}
	}

	void ShotArrow ()
	{
		int posY = Random.Range (-50, 30);
		GameObject ar = Instantiate (go_Arrow) as GameObject;
		ar.transform.position = new Vector3 (posStart.x, posY, 0);
		arrows [count] = ar;
		count++;
	}

	public void RessetArrow ()
	{
		foreach (GameObject go in arrows) {
			if (go != null) {
				Destroy (go);
			}
		}
		count = 0;
		timer = 0;
	}
}
