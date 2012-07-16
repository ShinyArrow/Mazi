using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	

	void Update () {
	
	}
	
	public static bool Grounded(GameObject _gameObject, float rayLength, Vector3 plusVector)
	{
		bool grnded;
		
		if (Physics.Raycast(_gameObject.transform.position + plusVector, Vector3.down, rayLength))
		{
			grnded = true;
		}
		else
		{
			grnded = false;
		}
		
		return grnded;
	}
}
