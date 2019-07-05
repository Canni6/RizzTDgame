using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameObject enemy;
	public float timeBetweenMobs = 2.0f;
	public int enemiesRemainingToSpawn;
    // track enemy transforms when spawned for use in TowerScript
    public List<Transform> enemyTransforms = new List<Transform>();

	// Use this for initialization
	void Start () {
	enemiesRemainingToSpawn = 5;
		
	}
	
	// Update is called once per frame
	void Update () {
		timeBetweenMobs -= Time.deltaTime;
		if(timeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) 
		{
			Instantiate(enemy);
            enemyTransforms.Add(enemy.transform);
			timeBetweenMobs = 2.0f;
			enemiesRemainingToSpawn--;
		}
        foreach (Transform enemyTransform in enemyTransforms)
        {
            print(enemyTransform);
        }
	}
}
