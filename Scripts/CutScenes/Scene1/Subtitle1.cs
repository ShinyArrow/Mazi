using UnityEngine;
using System.Collections;

public class Subtitle1 : MonoBehaviour {
	
	public bool Speak;
	
	public Texture2D[] Subtitles;
	
	public Material SubMat;
	
	public int currentSubtitle;
	
	public Transform subtitlePlane;
	
	public Transform talkingObject;
	
	public float height = 5.0f;
	public float side = -5.0f;
	
	public float firstRestTime = 3.0f;
	
	public float restTime = 3.0f;
	
	void Start () 
	{
		SubMat.mainTexture = Subtitles[currentSubtitle];
		subtitlePlane.gameObject.SetActiveRecursively(false);
	}
	

	void LateUpdate ()
	{
		
		if (SubMat.mainTexture != Subtitles[currentSubtitle])
		{
			SubMat.mainTexture = Subtitles[currentSubtitle];
		}
		
		if (firstRestTime >= 0)
		{
			firstRestTime -= Time.deltaTime;
		}
		else
		{
			if (!Speak)
			{
				Speak = true;
			}
		}
		
		if (Speak)
		{
			ShowSubtitle();
		}
	}
	
	void ShowSubtitle()
	{
		if (!subtitlePlane.gameObject.active)
		subtitlePlane.gameObject.SetActiveRecursively(true);
		
		if (restTime >= 0)
		{
			restTime -= Time.deltaTime;
		}
		

		if (restTime <= 0)
		{
			if (Speak && Subtitles.Length > currentSubtitle +1)
			{
				currentSubtitle += 1;
				restTime = 5f;
			}
			else
			{
				subtitlePlane.gameObject.SetActiveRecursively(false);
				Destroy(this);
				//Speak = false;
			}
		}
		
		subtitlePlane.position = new Vector3(0,talkingObject.position.y + height,talkingObject.position.z + side);
	}
}