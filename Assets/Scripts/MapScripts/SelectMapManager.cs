using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SelectMapManager : MonoBehaviour
{
	public static SelectMapManager Instance;
	public Transform btnMaps;
	ButtonChooseMap[] btnsChooseMap;
	public Text txtCoin;
	public Sprite[] imgMaps;
	public string[] nameMaps;
	public int[] prices;
	public GameObject status;
	public Text textStatus;
	int count = 0;
	void Awake ()
	{
		Instance = this;
		count = btnMaps.childCount;
		btnsChooseMap = new ButtonChooseMap[count];
		for (int i = 0; i < count; i++) {
			btnsChooseMap [i] = btnMaps.GetChild (i).GetComponent<ButtonChooseMap> ();
			btnsChooseMap [i].SetImageMap (this.imgMaps [i]);
			btnsChooseMap [i].key = this.nameMaps [i];
			btnsChooseMap [i].price = this.prices [i];
			btnsChooseMap [i].indexMap = i;
			btnsChooseMap [i]._Start ();
		}
	}
	void Start ()
	{
		this.SetTextCoin ();
	}

	public void SetTextCoin ()
	{
		txtCoin.text = "" + CoinManager.coin;
	}
	void Update()
	{
		txtCoin.text = "" + CoinManager.coin;
	}
	IEnumerator delayCheckStatus ()
	{
		yield return new WaitForSecondsRealtime (1f);
		this.checkStatus ();
	}

	public void checkStatus ()
	{
	}


}
