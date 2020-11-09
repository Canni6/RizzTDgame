using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameManager gameManager;
	public GameObject enemy;
	public float timeBetweenMobs = 2.0f;
	public int enemiesRemainingToSpawn;
	public int enemiesInScene;
	public Wave currentWave;

	public int waveCounter;
	public Wave wave0 = new Wave("Rizzes", 3, 6.0f, 5);
	public Wave wave1 = new Wave("Jizzes", 2, 8.0f, 10);
	public Wave[] waves = new Wave[2];
	

	// Use this for initialization
	void Start ()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		waveCounter = 0;
		waves[0] = wave0;
		waves[1] = wave1;
		currentWave = waves[waveCounter];
		enemiesRemainingToSpawn = currentWave.getSize();
		enemiesInScene = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
		timeBetweenMobs -= Time.deltaTime;
		if(timeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) 
		{
            Instantiate(enemy);
			enemiesInScene += 1;
            timeBetweenMobs = 2.0f;
			enemiesRemainingToSpawn--;
		}
	}
	
	public void removeEnemy() {
		enemiesInScene -= 1;
    }

	public int getEnemiesInScene() {
		return enemiesInScene;
    }

	public int getEnemiesRemainingToSpawn() {
		return enemiesRemainingToSpawn;
    }

	public void loadNextWave() {
		waveCounter++;
		if (waveCounter == waves.Length) {
			print("Game is over.");
			gameManager.setGameWon();
        } else {
			currentWave = waves[waveCounter];
			enemiesRemainingToSpawn = currentWave.getSize();
			enemiesInScene = 0;
		}
	}

	public Wave getCurrentWave() {
		return currentWave;
    }

}
