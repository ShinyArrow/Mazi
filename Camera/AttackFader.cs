using UnityEngine;
using System.Collections;

public class AttackFader : MonoBehaviour {

	public float fadeSpeed;
	void Start () {
	
	}
	

	void Update () {
		if (renderer.material.color.r > 0)
		{
			renderer.material.color -= new Color(0,0,0, fadeSpeed * Time.deltaTime);
		}
		else
		{
			Destroy(gameObject);
		}

	}
}
