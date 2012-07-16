using UnityEngine;
using System.Collections;

public class EnemyDamageReceiver : MonoBehaviour {
		
	public bool shakeCam;
	public float shakePower = 5;
	public float restTime = 0.3f;
	public int health = 100;
	public GameObject DamageSoundObject;
	public GameObject Attitude;
	public Transform myFreeSub;
	public Vector3 PlusVector;
	
	public GameObject DestroyedCube;
	
	public GameObject AttackFade;
	
	public float planeRestTime = 1.65f;
	
	void Start () 
	{
		if(animation)
		{
			myFreeSub.gameObject.active = false;
		}
		
		else
		{
			myFreeSub.gameObject.active = true;
		}
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
		myFreeSub.position = transform.position + PlusVector;
		
		if (health == 100)
		{
			if (animation)
			animation.CrossFade("Idle");		
		}
		else
		{
			if (animation)
			animation.CrossFade("Tawheed");
			myFreeSub.gameObject.active = true;
		}
		if (shakeCam && PlayerFollow.CamChange == null)
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
			KillMe();
		
	}
	
	void ApplyDamage(int damage)
	{
		Instantiate(AttackFade,Camera.main.transform.position + new Vector3(-3,0,0),AttackFade.transform.rotation);
		health -= damage;
		DamageSoundObject.audio.Play();
		shakeCam = true;
	}
	
	void KillMe()
	{
		Instantiate(DestroyedCube,transform.position,transform.rotation);
		myFreeSub.gameObject.AddComponent("FlyFreedom");
		Attitude.SendMessage("KilledCube");
		Destroy(gameObject);
	}
	
	void ShowSub()
	{
		myFreeSub.gameObject.active = true;
	}
	void HideSub()
	{

		myFreeSub.gameObject.active = false;

	}

}
