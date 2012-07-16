using UnityEngine;
using System.Collections;

public class Subtitle2 : MonoBehaviour {
	
	public enum Speak
	{
		Leader,
		King,
		None
	}
	
	public Speak curSpeaker;
	
	public int LeaderTalkTime = 1;
	public int KingTalkTime = 1;
	
	public Texture2D[] LeaderSubtitles;
	public Texture2D[] KingSubtitles;
	
	public Material LeaderSubMat;
	public Material KingSubMat;
	
	public int LeaderCurrentSubtitle;
	public int KingCurrentSubtitle;
	
	public Transform LeaderSubtitlePlane;
	public Transform KingSubtitlePlane;
	
	public Transform LeaderObject;
	public Transform KingObject;
	
	public float LeaderHeight = 5.0f;
	public float LeaderSide = -5.0f;
	
	public float KingHeight = 5.0f;
	public float KingSide = -5.0f;
	
	public float firstRestTime = 3.0f;
	
	public float LeaderRestTime = 3.0f;
	public float KingRestTime = 3.0f;
	
	void Start () 
	{
		LeaderSubMat.mainTexture = LeaderSubtitles[LeaderCurrentSubtitle];
		LeaderSubtitlePlane.gameObject.SetActiveRecursively(false);
		
		KingSubMat.mainTexture = KingSubtitles[KingCurrentSubtitle];
		KingSubtitlePlane.gameObject.SetActiveRecursively(false);
		
		curSpeaker = Speak.None;
	}
	

	void Update ()
	{
		
		if (LeaderSubMat.mainTexture != LeaderSubtitles[LeaderCurrentSubtitle])
		{
			LeaderSubMat.mainTexture = LeaderSubtitles[LeaderCurrentSubtitle];
		}
		if (KingSubMat.mainTexture != KingSubtitles[KingCurrentSubtitle])
		{
			KingSubMat.mainTexture = KingSubtitles[KingCurrentSubtitle];
		}

		if (firstRestTime >= 0)
		{
			firstRestTime -= Time.deltaTime;
		}
		else
		{
			if (curSpeaker == Speak.None)
			{
				curSpeaker = Speak.Leader;
			}
		}
		
		if (curSpeaker == Speak.Leader)
		{
			ShowLeaderSubtitle();
		}
		else if (curSpeaker == Speak.King)
		{
			ShowKingSubtitle();
		}
	}
	
	void ShowLeaderSubtitle()
	{
		if ( KingTalkTime == 1)
		{
			KingTalkTime = 2;
		}

		if (!LeaderSubtitlePlane.gameObject.active)
		LeaderSubtitlePlane.gameObject.SetActiveRecursively(true);
		
		if (LeaderRestTime >= 0)
		{
			LeaderRestTime -= Time.deltaTime;
		}
		

		if (LeaderRestTime <= 0)
		{
			if (LeaderSubtitles.Length > LeaderCurrentSubtitle +1)
			{
				if (LeaderCurrentSubtitle == 0 && LeaderTalkTime == 1)
				{
					LeaderCurrentSubtitle += 1;
					
					LeaderRestTime = 3f;
				}
				else if (LeaderCurrentSubtitle == 1 && LeaderTalkTime == 1)
				{
					Debug.Log("S");
					LeaderSubtitlePlane.gameObject.SetActiveRecursively(false);
					curSpeaker = Speak.King;
				}
				
				if (LeaderCurrentSubtitle == 1 && LeaderTalkTime == 2)
				{
					Debug.Log("Increase");
					LeaderCurrentSubtitle += 1;
					
					LeaderRestTime = 3f;
				}
				else if (LeaderCurrentSubtitle == 3 && LeaderTalkTime ==2)
				{
					Debug.Log("H");
					LeaderSubtitlePlane.gameObject.SetActiveRecursively(false);
					curSpeaker = Speak.King;
				}
			}
			else
			{
				//LeaderSubtitlePlane.gameObject.SetActiveRecursively(false);
			}
		}
		
		LeaderSubtitlePlane.position = new Vector3(0,LeaderObject.position.y + LeaderHeight,LeaderObject.position.z + LeaderSide);
	}
	void ShowKingSubtitle()
	{
		if (KingTalkTime == 0)
		{
			KingTalkTime = 1;
		}
		if (LeaderTalkTime == 1)
		{
			LeaderTalkTime = 2;
		}
		if (LeaderTalkTime == 2)
		{
			LeaderTalkTime = 3;
		}
		
		if (!KingSubtitlePlane.gameObject.active)
		KingSubtitlePlane.gameObject.SetActiveRecursively(true);
		
		if (KingRestTime >= 0)
		{
			KingRestTime -= Time.deltaTime;
		}
		

		if (KingRestTime <= 0)
		{
			if (KingSubtitles.Length > KingCurrentSubtitle +1)
			{
				if (KingCurrentSubtitle == 1 && KingTalkTime == 1)
				{
					KingCurrentSubtitle += 1;
					KingRestTime = 3f;
				}
				else
				{
					Debug.Log("a");
					KingSubtitlePlane.gameObject.SetActiveRecursively(false);
					curSpeaker = Speak.Leader;
				}

			}
			else
			{
				KingSubtitlePlane.gameObject.SetActiveRecursively(false);
			}
		}
		
		KingSubtitlePlane.position = new Vector3(0,KingObject.position.y + KingHeight,KingObject.position.z + KingSide);
	}
	
	void Controll()
	{
		
	}
}