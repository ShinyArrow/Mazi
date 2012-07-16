using UnityEngine;
using System.Collections;

public class FlyFreedom : MonoBehaviour {
	
	public float flySpeed = 10;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position += new Vector3(0,flySpeed * Time.deltaTime,0);
		
		renderer.material.color -= new Color(0,0,0,0.7f*Time.deltaTime);
		
		Destroy(gameObject, 3);
	}
}
