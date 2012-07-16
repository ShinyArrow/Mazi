using UnityEngine;
using System.Collections;

public class Side_Animator : MonoBehaviour {

	public Side_Controller sController;
	
	public Transform myBody;
	
	public Transform SphereBody;
	public Transform BoxBody;

	void Awake ()
	{
		if (!sController)
		{
			sController = GetComponent("Side_Controller") as Side_Controller;
		}
	}
	

	void Update () 
	{
		if (Attitude.Cruelty != 100)
		{
			if (BoxBody.gameObject.active)
			{
				BoxBody.gameObject.SetActiveRecursively(false);
			}
			
			if (!SphereBody.gameObject.active)
			{
				SphereBody.gameObject.SetActiveRecursively(true);
			}
			
			if (myBody != SphereBody)
			{
				myBody = SphereBody;
			}
		}
		
		else
		{
			if (!BoxBody.gameObject.active)
			{
				BoxBody.gameObject.SetActiveRecursively(true);
			}
			
			if (SphereBody.gameObject.active)
			{
				SphereBody.gameObject.SetActiveRecursively(false);
			}
			
			if (myBody != BoxBody)
			{
				myBody = BoxBody;
			}
		}

		switch (sController.State)
		{
		case Side_Controller.currentState.Idle:
			if (!myBody.animation.IsPlaying("Land"))
			myBody.animation.CrossFade("Idle");
			break;
		case Side_Controller.currentState.Moving:
			if (!myBody.animation.IsPlaying("Land"))
			myBody.animation.CrossFade("Walk");
			break;
		case Side_Controller.currentState.Falling:
			myBody.animation.CrossFade("Fall");
			break;
		case Side_Controller.currentState.Landing:
			myBody.animation.Stop("Fall");
			myBody.animation.CrossFade("Land");
			break;
		case Side_Controller.currentState.Jumping:
			myBody.animation.CrossFade("Idle");
			break;

		}
	}
}
