using UnityEngine;
using System.Collections;

public class Subtitle3 : MonoBehaviour {
	
	public bool Speak;
	
	public enum Speaking
	{
		Leader,
		King
	}
	public Speaking speaker;
	
	public int LeaderTalkTime = 1;
	public int KingTalkTime = 1;
	
	public Texture2D[] Subtitles;
	
	public Material LeaderSubMat;
	public Material KingSubMat;
	
	public Material curSubMat;
	
	public int currentSubtitle;
	
	public Transform SubtitlePlane;
	
	public Transform LeaderObject;
	public Transform KingObject;
	public Transform curObject;
	
	public Transform KingAnimationObject;
	
	public float LeaderHeight = 2.0f;
	public float LeaderSide = -3.2f;
	
	public float KingHeight = 2.0f;
	public float KingSide = 3.0f;
	
	public float curHeight;
	public float curSide;
	
	public float firstRestTime = 3.0f;
	
	public float restTime = 3.0f;
	
	void Start () 
	{
		curSubMat.mainTexture = Subtitles[currentSubtitle];
		SubtitlePlane.gameObject.SetActiveRecursively(false);
		
		curObject = LeaderObject;
		curHeight = LeaderHeight;
		curSide = LeaderSide;
		SubtitlePlane.renderer.material = curSubMat;
	}
	

	void Update ()
	{
		
		if (curSubMat.mainTexture != Subtitles[currentSubtitle])
		{
			curSubMat.mainTexture = Subtitles[currentSubtitle];
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
			Controller();
			ShowSubtitle();
		}
	}
	
	void ShowSubtitle()
	{
		if (!SubtitlePlane.gameObject.active)
		SubtitlePlane.gameObject.SetActiveRecursively(true);
		
		if (restTime >= 0)
		{
			restTime -= Time.deltaTime;
		}
		

		if (restTime <= 0)
		{
			if (Speak && Subtitles.Length > currentSubtitle +1)
			{
				currentSubtitle += 1;
				restTime = 4.0f;
			}
			else
			{
				SubtitlePlane.gameObject.SetActiveRecursively(false);
				Speak = false;
			}
		}
		
		SubtitlePlane.position = new Vector3(0,curObject.position.y + curHeight,curObject.position.z + curSide);

	}
	

	void Controller()
	{
		
		if (currentSubtitle == 0 || currentSubtitle == 1 || currentSubtitle == 3 || currentSubtitle == 5)
		{
			speaker = Speaking.Leader;
		}
		else if ( currentSubtitle == 2 || currentSubtitle == 4 || currentSubtitle == 6)
		{
			speaker = Speaking.King;
		}
		
		if (speaker == Speaking.Leader)
		{
		curObject = LeaderObject;
		curHeight = LeaderHeight;
		curSide = LeaderSide;

		}
		else if (speaker == Speaking.King)
		{
		curObject = KingObject;
		curHeight = KingHeight;
		curSide = KingSide;

		}
		
		if (currentSubtitle == 6)
		{
			KingAnimationObject.animation.CrossFade("Point");
		}
	}
}