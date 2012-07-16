using UnityEngine;
using System.Collections;

public class LoadLevelTime : MonoBehaviour {

	public float restTime = 15.0f;
	
	public string nexetLeve;
	
	void Start () {
	
	}
	

	void Update ()
	{
		restTime -= Time.deltaTime;
		
		if (restTime <= 0)
		{
			Application.LoadLevel(nexetLeve);
		}
	}
}
