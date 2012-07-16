using UnityEngine;
using System.Collections;

public class _1KingScene : MonoBehaviour {
	
	public Transform myCamera;
	public Transform King;
	public Transform GaurdLeader;
	public float restTime = 1.0f;
	
	void Start () {
	
	}
	
	void Update () 
	{
		 if (restTime >= 0)
		{
			restTime -= Time.deltaTime;
		}
		if (restTime <=0 )
		{
			if (!GaurdLeader.gameObject.active)
			GaurdLeader.gameObject.SetActiveRecursively(true);
		}
	}
}
