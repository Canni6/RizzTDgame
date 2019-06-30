using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	public GameObject bullet;
	public Transform BulletSpawn;
	public Transform StartingPoint;
//	GameObject TargetGo;
//	Transform TargetEnemy;
//	


//	void NewTargetEnemy()
//	{
//	TargetGo = GameObject.Find("enemyAI(Clone)");
//	}

//	void MoveTowardEnemy()
//	{
//	float step = Speed * Time.deltaTime;
//	transform.position = Vector3.MoveTowards(transform.position, TargetEnemy.position, step);
//	}

	// Transform targetEnemy;



	// Use this for initialization
	void Start () {

		

	}
	
	// Update is called once per frame
	void Update () {

	float Speed = 15f;
	float step = Speed * Time.deltaTime;

	if(Input.GetKeyDown(KeyCode.D))
	{
	Instantiate(bullet, BulletSpawn.position, BulletSpawn.rotation);
	Debug.Log("Bullet created");
	}
	transform.position = Vector3.MoveTowards(transform.position, StartingPoint.position, step);
//		if(TargetEnemy == null)
//		{
//		NewTargetEnemy();
//		Debug.Log("Target acquired");
//		}

	
//	MoveTowardEnemy();
//	Debug.Log("Ball is on the move");
//	}
	}
}