using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SgLib;
public class iapShow : MonoBehaviour 
{
	public GameObject IAP_Obj;
	public GameObject SaleOff_Obj;
	public Animator iapAnimator;
	public Animator SaleOffAnimator;
	public Animator SaleOffBtnAnimator;
	public Text TextCoins;
	public Text TextCoins1;
	public GameObject SaleObj1;
	public GameObject SaleObj2;
	public GameObject SaleObj3;
	public GameObject SaleObj4;
	public int randomSaleObj;

	void Start()
	{
		
		if(DataManager.Instance.Maps<=5 )
		{
			SaleOffBtn_Show ();
		}
		else if(DataManager.Instance.Maps>6||DataManager.Instance.Maps<=9 )
		{
			SaleOffBtn_Show ();
		}
		else if(DataManager.Instance.Maps>11 )
		{
			SaleOffBtn_Show ();
		}
		else
			SaleOffBtn_Hide ();
		
		if(DataManager.Instance.Maps<5)
		{
			randomSaleObj = Random.Range (1,2);
		}
		else if (DataManager.Instance.Maps >=6&&DataManager.Instance.Maps <= 12)
		{
			randomSaleObj = Random.Range (2,5);
		}

		else if(DataManager.Instance.Maps >12)
		{
			randomSaleObj = Random.Range (1,5);
		}
		if(randomSaleObj==1)
		{
			SaleObj1.SetActive (true);
			SaleObj2.SetActive (false);
			SaleObj3.SetActive (false);
			SaleObj4.SetActive (false);
		}
		if(randomSaleObj==2)
		{
			SaleObj1.SetActive (false);
			SaleObj2.SetActive (true);
			SaleObj3.SetActive (false);
			SaleObj4.SetActive (false);
		}
		if(randomSaleObj==3)
		{
			SaleObj1.SetActive (false);
			SaleObj2.SetActive (false);
			SaleObj3.SetActive (true);
			SaleObj4.SetActive (false);
		}
		if(randomSaleObj==4)
		{
			SaleObj1.SetActive (false);
			SaleObj2.SetActive (false);
			SaleObj3.SetActive (false);
			SaleObj4.SetActive (true);
		}

	}
	void Update()
	{
		TextCoins.text = "" + CoinManager.coin;
		TextCoins1.text = "" + CoinManager.coin;
	}
	public void IAP_Show ()
	{ 
		IAP_Obj.SetActive (true);
		iapAnimator.SetBool ("Off", false);
		iapAnimator.SetTrigger ("On");

	}

	public void IAP_Hide ()
	{
		iapAnimator.SetBool ("On", false);
		iapAnimator.SetTrigger ("Off");

	}

	private void ResetAnimationParameters ()
	{
		if (iapAnimator == null) {
			return;
		}
		iapAnimator.SetBool ("On", false);
		iapAnimator.SetBool ("Off", false);
	}

	//---
	public void SaleOff_Show ()
	{ 
		SaleOff_Obj.SetActive (true);
		SaleOffAnimator.SetBool ("Off", false);
		SaleOffAnimator.SetTrigger ("On");

	}

	public void SaleOff_Hide ()
	{
		SaleOffAnimator.SetBool ("On", false);
		SaleOffAnimator.SetTrigger ("Off");

	}

	private void ResetSaleOffParameters ()
	{
		if (SaleOffAnimator == null) {
			return;
		}
		SaleOffAnimator.SetBool ("On", false);
		SaleOffAnimator.SetBool ("Off", false);
	}

	public void SaleOffBtn_Show ()
	{ 
		SaleOffBtnAnimator.SetBool ("Off", false);
		SaleOffBtnAnimator.SetTrigger ("On");

	}

	public void SaleOffBtn_Hide ()
	{
		SaleOffBtnAnimator.SetBool ("On", false);
		SaleOffBtnAnimator.SetTrigger ("Off");

	}

	private void ResetSaleOffBtnParameters ()
	{
		if (SaleOffBtnAnimator == null) {
			return;
		}
		SaleOffBtnAnimator.SetBool ("On", false);
		SaleOffBtnAnimator.SetBool ("Off", false);
	}

}
