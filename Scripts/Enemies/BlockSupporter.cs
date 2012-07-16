using UnityEngine;
using System.Collections;

public class BlockSupporter : MonoBehaviour {
	
	public bool Supporting;
	
	public Transform SubPlane;
	//public float subTime = 
	
	public bool shakeCam;
	public float shakePower = 5;
	public float restTime = 0.3f;
	public float health = 100;
	public float maxHealth;
	public GameObject DamageSoundObject;
	public GameObject Attitude;
	public Vector3 PlusVector;
	
	public GameObject DestroyedCube;
	
	public Transform HealthBar;
	
	public Transform curObject;
	public float curHeight;
	public float curSide;
	
	public float AnRestTime = 1.65f;
	
	void Start () 
	{
		AnRestTime = animation["Support"].length -0.3F;
		maxHealth = health;
		if (!DamageSoundObject)
		{
			DamageSoundObject = GameObject.Find("DamageSound");
		}
		if (!Attitude)
		{
			Attitude = GameObject.Find("_Manger");
		}
	}
	

	void Update () 
	{	
		

		HealthBar.transform.localScale = new Vector3(1,1,health/maxHealth);

		
		if (health > maxHealth * 0.50f)
		{
			if (!Supporting)
			{
				if (animation)
				{
					animation.CrossFade("Idle");		
				}
			}
			else
			{
				if (animation)
					animation.CrossFade("SupIdle");
			}
		}
		else 
		{
			
			
			if (animation)
			{
				
				if (AnRestTime > 0 && !Supporting)
				{
					SubPlane.gameObject.active = true;
					AnRestTime -= Time.deltaTime;
					animation.CrossFade("Support");
				}
				
				else if (AnRestTime <= 0)
				{
					if (!animation.IsPlaying("SupIdle"))
					{
						Supporting = true;
						health = maxHealth/2;
					}
					animation.CrossFade("SupIdle");
					if (SubPlane.gameObject.active)
					SubPlane.gameObject.active = false;
					
					
				}
				
			}
		}
		if (shakeCam)
		{
			restTime -= Time.deltaTime;
			if (restTime > 0)
			{
				Camera.main.transform.position += new Vector3(0,Random.Range(-shakePower,shakePower),Random.Range(-shakePower,shakePower));
			}
			else
			{
				Camera.main.transform.localPosition = Vector3.zero;
				restTime = 0.1f;
				shakeCam = false;
			}
		}
		
		if (health <= 0)
		{
			health = 0;
			KillMe();
		}
		
		
		SubPlane.position = new Vector3(0,curObject.position.y + curHeight,curObject.position.z + curSide);
	}
	
	void ApplyDamage(int damage)
	{
		health -= damage;
		DamageSoundObject.audio.Play();
		shakeCam = true;
	}
	
	void KillMe()
	{
		Instantiate(DestroyedCube,transform.position,transform.rotation);
		Destroy(gameObject);
	}
	
}
