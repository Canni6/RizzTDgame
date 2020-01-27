using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject enemy;
	public float timeBetweenMobs = 2.0f;
	public int enemiesRemainingToSpawn;

    // Use this for initialization
	void Start ()
    {
	    enemiesRemainingToSpawn = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
		timeBetweenMobs -= Time.deltaTime;
		if(timeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) 
		{
            Instantiate(enemy);
            timeBetweenMobs = 2.0f;
			enemiesRemainingToSpawn--;
		}
	} 
}
