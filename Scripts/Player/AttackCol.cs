using UnityEngine;
using System.Collections;

public class AttackCol : MonoBehaviour {

	public static GameObject HuntedOne;
	public GameObject H;
	void Start () {
	
	}
	
	void Update () 
	{
		
	}
	
	void OnCollisionEnter(Collision Others)
	{
		HuntedOne = Others.gameObject;
		H = Others.gameObject;
	}
	void OnCollisionExit(Collision Others)
	{
		HuntedOne = null;
		H = null;
	}

}
