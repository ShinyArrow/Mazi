using UnityEngine;
using System.Collections;

public class Side_Controller : MonoBehaviour {
	
	public Transform AttackCollider;
	public string[] attackAnimations;
	public string lastAttackANM;
	public float currentAtAnTime;
	
	public float gravity;
	public int moveSpeed = 10;
	public float JumpPower = 10;
	public Transform myBody;
	
	public Transform SphereBody;
	public Transform BoxBody;
	
	public float VerticalVelocity;
	
	public enum currentMoveDirection
	{
		Stationary,
		Right,
		Left
	}
	public currentMoveDirection moveDir;
	
	public enum currentState
	{
		Idle,
		Moving,
		Jumping,
		Falling,
		Landing,
		Attack
	}
	
	public currentState State;
	
	//*****************************************
		

	public Vector3 MoveDirection;
	private CharacterController C_Controller;
	
	void Awake () {
		C_Controller = (CharacterController)GetComponent("CharacterController");
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

		MotionCallOrder();
		LookAtMoveDirection();
	}
	void LateUpdate()
	{
		CheckCurrentMoveDirection();
		CheckCurrentState();
		Attack();
		CheckIfCollidedWhileJumping();
		if(transform.position.x != 0)
		{
			transform.position = new Vector3(0,transform.position.y,transform.position.z);
		}
	}
	
	void MotionCallOrder()
	{

		Move();
		
		MoveDirection = new Vector3(MoveDirection.x,VerticalVelocity,MoveDirection.z);
		
		Gravity();
		
		Jump();
		
		C_Controller.Move(MoveDirection * Time.deltaTime);

	}
	
	void Move()
	{
		VerticalVelocity = MoveDirection.y;
		MoveDirection = Vector3.zero;
		
		float dead = 0.01f;
		
		if (Input.GetAxis("Horizontal") > dead || Input.GetAxis("Horizontal") < -dead)
		{
			MoveDirection += new Vector3(0,0,Input.GetAxis("Horizontal") * moveSpeed);
		}
	
	}
	
	void CheckCurrentMoveDirection()
	{

		if (MoveDirection.z == 0)
		{
			moveDir = currentMoveDirection.Stationary;
		}
		
		if (MoveDirection.z > 0)
		{
			moveDir = currentMoveDirection.Right;
		}
		
		if (MoveDirection.z < 0)
		{
			moveDir = currentMoveDirection.Left;
		}

	}
	
	void LookAtMoveDirection()
	{
		if (moveDir == currentMoveDirection.Right)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,0,0);
		}
		else if (moveDir == currentMoveDirection.Left)
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,180,0);
		}

	}
	
	void Gravity()
	{
		if (!C_Controller.isGrounded)
		{
			MoveDirection = new Vector3(MoveDirection.x,MoveDirection.y - gravity * Time.deltaTime,MoveDirection.z);
		}
		else if (C_Controller.isGrounded && MoveDirection.y < -1)
		{
			MoveDirection = new Vector3(MoveDirection.x,-1,MoveDirection.z);
		}
	}
	
	void Jump()
	{
		if (C_Controller.isGrounded)
		{
			if (Input.GetButtonDown("Jump")// > 0.01f 
				|| Input.GetKeyDown("w") 
				|| Input.GetKeyDown(KeyCode.UpArrow))
			{
				MoveDirection += new Vector3(0,JumpPower,0);
			}
		}
	}
	
	void CheckCurrentState()
	{
		if (!myBody.animation.IsPlaying("Attack1") &&
			!myBody.animation.IsPlaying("Attack2"))
		{
			if (MoveDirection.z != 0 && MoveDirection.y == -1)
			{
				State = currentState.Moving;
			}
			else if (MoveDirection.y > 0)
			{
				State = currentState.Jumping;
			}
			else if (MoveDirection.y < -1)
			{
				if (!Helper.Grounded(gameObject,1, Vector3.zero) &&
				!Helper.Grounded(gameObject,1, new Vector3(0,0,-0.75f)) &&
				!Helper.Grounded(gameObject,1, new Vector3(0,0,0.75f)))
				{
				State = currentState.Falling;
				}
				else
				{
					State = currentState.Landing;
				}
			}
			else if (MoveDirection.z == 0 && MoveDirection.y == -1)
			{
				State = currentState.Idle;
			}
		}
	}
	
	void CheckIfCollidedWhileJumping()
	{
		if(State == currentState.Jumping)
		{
			if (Physics.Raycast(transform.position, Vector3.up,1))
			{
				CutJumping();
			}
			else if (Physics.Raycast(transform.position + new Vector3(0,0,0.75f), Vector3.up,1))
			{
				CutJumping();
			}
			else if (Physics.Raycast(transform.position + new Vector3(0,0,-0.75f), Vector3.up,1))
			{
				CutJumping();
			}
		}
	}
	
	void CutJumping()
	{
		if (State == currentState.Jumping)
		MoveDirection = new Vector3(MoveDirection.x,-1,MoveDirection.z);
	}
	
	void Attack()
	{
		if (currentAtAnTime > 0)
		{
			currentAtAnTime -= Time.deltaTime;
		}
		else
		{
			lastAttackANM = "";
		}

		if (Input.GetButtonDown("Attack") && currentAtAnTime < 0.7f)
		{
			
			if (lastAttackANM == "" || lastAttackANM == attackAnimations[attackAnimations.Length-1]&& currentAtAnTime < myBody.animation[attackAnimations[0]].length * 0.1f)
			{
				myBody.animation.Stop();
				currentAtAnTime = myBody.animation[attackAnimations[0]].length * 1.25f;
				lastAttackANM = attackAnimations[0];
				myBody.animation.CrossFade(attackAnimations[0]);
				audio.Play();
			}
			else if ( lastAttackANM == attackAnimations[0] && currentAtAnTime > 0 && currentAtAnTime < myBody.animation[attackAnimations[0]].length * 0.7f)
			{
				myBody.animation.Stop();
				currentAtAnTime = myBody.animation[attackAnimations[1]].length * 1.25f;
				lastAttackANM = attackAnimations[1];
				myBody.animation.CrossFade(attackAnimations[1]);
				audio.Play();
			}
			State = currentState.Attack;
		}

	}
	
}
