using UnityEngine;
using System.Collections;

public class CoinManager
{
	public static int coin;
	public static void GetCoin(){
		coin = PlayerPrefs.GetInt ("COIN");
	}
	public static void UpCoin(){
		PlayerPrefs.SetInt ("COIN", coin);
		PlayerPrefs.Save ();
	}
	public static bool CheckBuy(int price){
		return (coin >= price) ? true : false;
	}
}
