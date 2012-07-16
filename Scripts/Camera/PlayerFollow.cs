using UnityEngine;
using System.Collections;

public class PlayerFollow : MonoBehaviour {

	public Transform Player;
	
	public Transform myCameraObject;
	
	public float Speed = 10;
	
	public float Height = 6;
	public float Distance = 10;
	
	public static Transform CamChange;
	
	public Transform cmChnge;
	
	void Start () 
	{
		Player = GameObject.Find("Player").transform;
		if (!myCameraObject)
		{
			myCameraObject = GameObject.Find("MainCamObject").transform;
		}
	}
	

	void LateUpdate ()
	{
		cmChnge = CamChange;
		transform.position = new Vector3(0,Player.position.y,Player.position.z);
		
		Vector3 plusVector = new Vector3(Distance,Height,0);
		if (!CamChange)
			myCameraObject.position = Vector3.MoveTowards(myCameraObject.position,transform.position + plusVector, Speed * Time.deltaTime);
		
		else
		{
			
			myCameraObject.position = Vector3.MoveTowards(myCameraObject.position,CamChange.position, Speed *Time.deltaTime);
		}
	}
}
