using UnityEngine;
using System;
using System.Collections;

public class DataManager : MonoBehaviour 
{

	public static DataManager Instance;

	public int FreeAdNumber
	{ 
		get { return _freeadnum; }
		private set { _freeadnum = value; }
	}
	public int intCheckForAds
	{ 
		get { return _checkforads; }
		private set { _checkforads = value; }
	}
	public int IntAds
	{ 
		get { return _intads; }
		private set { _intads = value; }
	}
	public int Missions
	{ 
		get { return _missions; }
		private set { _missions = value; }
	}
	public int Maps
	{ 
		get { return _maps; }
		private set { _maps = value; }
	}
	public int Vehicle
	{ 
		get { return _vehicle; }
		private set { _vehicle = value; }
	}
	public int X2Coins
	{ 
		get { return _x2coins; }
		private set { _x2coins = value; }
	}
	public int GameAdvice = 1;
	public static event Action<int> FreeAdNumberUpdated = delegate {};
	public static event Action<int> intCheckForAdsUpdated = delegate {};
	public static event Action<int> IntAdsUpdated = delegate {};
	public static event Action<int> MissionsUpdated = delegate {};
	public static event Action<int> MapsUpdated = delegate {};
	public static event Action<int> VehicleUpdated = delegate {};
	public static event Action<int> X2CoinsUpdated = delegate {};

	[SerializeField]
	int initialFreeAdNumber = 10;

	[SerializeField]
	int initialcheckads = 0;

	[SerializeField]
	int initialintads = 0;

	[SerializeField]
	int _freeadnum=0;

	[SerializeField]
	int _checkforads=0;

	[SerializeField]
	int _intads=0;
	[SerializeField]
	int _missions=0;
	[SerializeField]
	int _maps=0;
	[SerializeField]
	int _vehicle=0;
	[SerializeField]
	int _x2coins=0;

	void Awake()
	{
		//Set all data new
//		PlayerPrefs.DeleteAll ();
		if (Instance)
		{
			DestroyImmediate(gameObject);
		}
		else
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
	}

	void Start()
	{
		Reset();
	}

	public void Reset()
	{
		// Initialize coins
		ResetFreeAds ();
	}
	//----------------------
	public void ResetFreeAds()
	{
		FreeAdNumber = PlayerPrefs.GetInt("FreeAdsNumber", initialFreeAdNumber);
		intCheckForAds = PlayerPrefs.GetInt("CheckForAds", initialcheckads);
		IntAds = PlayerPrefs.GetInt("IntAds", initialintads);
		Missions = PlayerPrefs.GetInt("Mis", _missions);
		Maps = PlayerPrefs.GetInt("Mapsown", _maps);
		Vehicle = PlayerPrefs.GetInt("Vehicleown", _vehicle);
		X2Coins = PlayerPrefs.GetInt("x2Coin", _x2coins);
	}
	public void AddMissions(int amount)
	{
		Missions += amount;
		PlayerPrefs.SetInt("Mis", Missions);
		MissionsUpdated(Missions);
	}
	public void AddMaps(int amount)
	{
		Maps += amount;
		PlayerPrefs.SetInt("Mapsown", Maps);
		MapsUpdated(Maps);
	}
	public void AddVehicle(int amount)
	{
		Vehicle += amount;
		PlayerPrefs.SetInt("Vehicleown", Vehicle);
		VehicleUpdated(Vehicle);
	}
	public void AddX2Coins(int amount)
	{
		X2Coins += amount;
		PlayerPrefs.SetInt("x2Coin", X2Coins);
		X2CoinsUpdated(X2Coins);
	}

	public void AddFreeAdNumber(int amount)
	{
		FreeAdNumber += amount;
		PlayerPrefs.SetInt("FreeAdsNumber", FreeAdNumber);
		FreeAdNumberUpdated(FreeAdNumber);
	}

	public void RemoveFreeAdNumber(int amount)
	{
		FreeAdNumber -= amount;
		PlayerPrefs.SetInt("FreeAdsNumber", FreeAdNumber);
		FreeAdNumberUpdated(FreeAdNumber);
	}

	//----------------------
	public void AddCheckForADS(int amount)
	{
		intCheckForAds += amount;
		PlayerPrefs.SetInt("CheckForAds", intCheckForAds);
		intCheckForAdsUpdated(intCheckForAds);
	}

	public void RemoveCheckForADS(int amount)
	{
		intCheckForAds -= amount;
		PlayerPrefs.SetInt("CheckForAds", intCheckForAds);
		intCheckForAdsUpdated(intCheckForAds);
	}

	//----------------------

	public void AddIntAds(int amount)
	{
		IntAds += amount;
		PlayerPrefs.SetInt("IntAds", IntAds);
		IntAdsUpdated(IntAds);
	}

	public void RemoveIntAds(int amount)
	{
		IntAds -= amount;
		PlayerPrefs.SetInt("IntAds", IntAds);
		IntAdsUpdated(IntAds);
	}

	//----------------------


}
