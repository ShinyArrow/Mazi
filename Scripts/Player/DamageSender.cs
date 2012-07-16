using UnityEngine;
using System.Collections;

public class DamageSender : MonoBehaviour {
	
	public LayerMask EnemeyLayer;
	public float rayLength = 2;
	
	public Transform hunted;
	
	void Update () 
	{
		RaycastHit hit;
		
		if (Physics.Raycast(transform.root.position,transform.root.forward,out hit, rayLength,EnemeyLayer))
		{
			hunted = hit.transform;
		}
		else
		{
			hunted = null;
		}
		
		Debug.DrawRay(transform.root.position,transform.root.forward * rayLength);
		
	}
	
	void SendDamage(int damage)
	{
		if (hunted)
		hunted.SendMessage("ApplyDamage",damage,SendMessageOptions.DontRequireReceiver);
	}
}
