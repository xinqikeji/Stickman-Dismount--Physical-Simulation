using UnityEngine;
using System.Collections;

public class VehiclesManager : MonoBehaviour
{
	public static VehiclesManager Instance;
	public VehiclesCreatePlayer vehicles_;
	public int indexVehicles;

	void Awake ()
	{
		Instance = this;
	}

	public VehiclesMovement create_vehiclesMovement ()
	{
		return vehicles_._Player (indexVehicles).GetComponent<VehiclesMovement> ();
	}

    public void ChangeVehicle(string name)
    {
        indexVehicles = this._GetIndex(name);
        PlayerControlManager.Instance.isVehicles = true;
        PlayerControlManager.Instance.click_Reset();
    }

    int _GetIndex (string s)
	{
		switch (s) {
		case "超级购物车":
			return 0;
		case "电动座椅":
			return 1;
		case "儿童滑板车":
			return 2;
		case "病床":
			return 3;
		case "悬浮滑板":
			return 4;
		case "摩托车":
			return 5;
		case "吉普SUV":
			return 6;
		case "运动自行车":
			return 7;
		case "吉普车炸弹":
			return 8;
		case "敞篷跑车":
			return 9;
		case "推土机":
			return 10;
		default:
			return 0;
		}
	}
}
