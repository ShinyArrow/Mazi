using UnityEngine;
using System.Collections;

public class Thrower : MonoBehaviour {
	
	public bool Supporting;
		
	public bool shakeCam;
	public float shakePower = 5;
	public float restTime = 0.3f;
	public float health = 100;
	public float maxHealth;
	public GameObject DamageSoundObject;
	
	public Transform HealthBar;
		
	public Rigidbody Throwing;
	
	public float AnRestTime = 1.65f;
	
	public GameObject[] TargetEnemies;
	
	
	public GameObject lastSup;
	public Material lastSupMat;
	
	void Start () 
	{
		maxHealth = health;
		if (!DamageSoundObject)
		{
			DamageSoundObject = GameObject.Find("DamageSound");
		}	
	}
	

	void Update () 
	{	
		
		if (Throwing.position.y < 15.8f)
		{
			if (TargetEnemies.Length > 0)
			{
				foreach (GameObject enemy in TargetEnemies)
				{
					foreach (Transform child in enemy.transform)
					{
						child.gameObject.SendMessage("ApplyDamage", 1000,SendMessageOptions.DontRequireReceiver);
					}
				}
			}
		}
		
		HealthBar.transform.localScale = new Vector3(1,1,health/maxHealth);

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
		
		
	}
	
	void ApplyDamage(int damage)
	{
		if (health >0)
		{
			health -= damage;
		}
		DamageSoundObject.audio.Play();
		shakeCam = true;

	}
	
	void KillMe()
	{
		if (lastSup)
		{
			lastSup.renderer.material = lastSupMat;
			lastSup.transform.localScale = new Vector3(1,1,1);
		}
		
		
		Throwing.useGravity = true;
		Throwing.isKinematic = false;
	}
	
}
