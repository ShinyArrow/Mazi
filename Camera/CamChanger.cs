using UnityEngine;
using System.Collections;

public class CamChanger : MonoBehaviour {
	
	public bool normal = true;
	
	public Transform myPos;
	
	public float restTime = 2.0f;
	public float bigRestTime = 5.0f;
	
	public Transform FadePlane;
	
	public float fadeSpeed = 3.0f;

	void Start () {
	
	}
	
	
	void Update () 
	{
		if (Attitude.Cruelty >= 100)
		{
			normal = false;
			restTime -= Time.deltaTime;
			bigRestTime -= Time.deltaTime;
			
			if (restTime <= 0)
			{
				PlayerFollow.CamChange = null;
			}
			
			if (bigRestTime <= 0)
			{
				FadeScene();
			}
		}
	}
	
	void OnTriggerEnter(Collider Others)
	{
		if (normal)
		{
			if (Others.tag == "Player")
			{
				PlayerFollow.CamChange = myPos;
			}
		}
	}
	
	void OnTriggerExit(Collider Others)
	{
		if(normal)
		{
			if (Others.tag == "Player")
			{
				PlayerFollow.CamChange = null;
			}
		}
	}
	
	void FadeScene()
	{
		if (FadePlane.renderer.material.color.a < 1)
		{
		FadePlane.renderer.material.color += new Color(0,0
														,0,fadeSpeed * Time.deltaTime);
		}
		else
		{
			Application.LoadLevel("Level2");
		}
	}


}
