using UnityEngine;
using System.Collections;

public class FixAnimShopVehicle : MonoBehaviour {
	public VehicleSelectManager vehicles;
	public void disableAnim(){
		vehicles.DisableAnim ();
	}
}
