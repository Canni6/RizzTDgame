using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour {

	public GameObject projectile;
	public Transform projectileSpawn;
	public Transform startingPoint;
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

	float Speed = 5f;
	float step = Speed * Time.deltaTime;

	if(Input.GetKeyDown(KeyCode.D))
	{
	Instantiate(projectile, projectileSpawn.position, projectileSpawn.rotation);
	Debug.Log("Projectile created");
	}
	transform.position = Vector3.MoveTowards(transform.position, startingPoint.position, step);
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