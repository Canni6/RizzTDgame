using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject enemy;
	public float TimeBetweenMobs = 1.0f;
	public int enemiesRemainingToSpawn;

	// TO DO:
	// implement waves of mobs of differing stats: health, speed, value
			// public Wave[] waves;



	// Use this for initialization
	void Start () {
	enemiesRemainingToSpawn = 5;
		
	}
	
	// Update is called once per frame
	void Update () {
		TimeBetweenMobs -= Time.deltaTime;
			if(TimeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) 
			{
				Instantiate(enemy);
				TimeBetweenMobs = 1.0f;
				enemiesRemainingToSpawn--;
			}
	}
}
