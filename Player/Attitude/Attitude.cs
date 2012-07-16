using UnityEngine;
using System.Collections;
using UnityArabicSupport;

public class Attitude : MonoBehaviour {
	
	public static int Cruelty = 4;
	public static int Mercy = 96;
	
	public Texture2D AtBar;
	public float barLength = 20;
	public float barHeight = 20;
	
	public Texture2D AttitudeBar;
	public Texture2D AttitudeBarBG;
	public float atLeft = 40;
	public float atHeight;
	
	public Texture2D CrueltyBar;
	public float crWidth = 3.8f;
	public float maxCrWidth = 95;
	public float crLeft = 5;
	
	public Texture2D MercyBar;
	public float mrWidth = 96.2f;
	public float maxMrWidth = 95;
	public float mrLeft = 5;

	
	public string crueltyName;
	public float crNameLeft;
	
	public string mercyName;
	public float mrNameLeft;
	
	public float AtSpeed = 4;
	
	public GUISkin mySkin;
	
	public int crlty;
	
	void Start () 
	{
		
	}
	

	void Update () 
	{
		
		crWidth = Mathf.MoveTowards(crWidth,Cruelty*maxCrWidth/100, AtSpeed * Time.deltaTime);
		mrWidth = Mathf.MoveTowards(mrWidth,Mercy*maxMrWidth/100, AtSpeed * Time.deltaTime);
	}
	
	void KilledCube()
	{
		Cruelty += 2;
		Mercy -= 2;
	}
	
	void OnGUI()
	{
		GUI.skin = mySkin;
		Rect barRect = new Rect(0,barHeight,Screen.width,barLength);
		GUI.DrawTexture(barRect,AtBar);
		////////////////////////////////////////////
		Rect attitudeRect = new Rect(Screen.width * 0.70f,atHeight,100,20);
		Rect crueltyRect = new Rect(Screen.width * 0.70f + crLeft,atHeight,crWidth,20);
		GUI.DrawTexture(attitudeRect,AttitudeBarBG);
		GUI.DrawTexture(crueltyRect,CrueltyBar);
		GUI.DrawTexture(attitudeRect,AttitudeBar);
		
		Rect crueltyLaberRect = new Rect(Screen.width * 0.70f + crNameLeft, barHeight - 3,50,40);
		
		GUI.Label(crueltyLaberRect,ArabicFixer.Fix(crueltyName));
		////////////////////////////////////////////
		
		Rect mrAttitudeRect = new Rect(Screen.width * 0.10f,atHeight,100,20);
		Rect mrRect = new Rect(Screen.width * 0.10f + mrLeft,atHeight,mrWidth,20);
		GUI.DrawTexture(mrAttitudeRect,AttitudeBarBG);
		GUI.DrawTexture(mrRect,MercyBar);
		GUI.DrawTexture(mrAttitudeRect,AttitudeBar);
		
		Rect mercyLaberRect = new Rect(Screen.width * 0.10f + mrNameLeft, barHeight - 3,50,40);
		
		GUI.Label(mercyLaberRect,ArabicFixer.Fix(mercyName));
		
		
	}
}
