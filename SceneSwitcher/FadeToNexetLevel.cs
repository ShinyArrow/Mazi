using UnityEngine;
using System.Collections;

public class FadeToNexetLevel : MonoBehaviour {
	
	public Transform FadePlane;
	
	public float fadeSpeed = 3.0f;
	
	public float restTime = 37.0f;
	
	void Start () {
	
	}
	

	void Update () 
	{
		if (restTime > 0)
		{
			restTime -= Time.deltaTime;
		}
		else
		{
			FadeScene();
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
			Application.LoadLevel("Level1");
		}
	}
}
