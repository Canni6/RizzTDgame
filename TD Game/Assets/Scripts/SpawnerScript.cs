using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameManager gameManager;
	public GameObject enemy;
	public GameObject start;

	public float timeBetweenMobs = 2.0f;
	public int enemiesRemainingToSpawn;
	public int enemiesInScene;

	public int waveCounter;
	public Wave currentWave;
	public Wave wave0 = new Wave("Bindis #1", 3, 6.0f, 5);
	public Wave wave1 = new Wave("Snags #2", 6, 8.0f, 5);
	public Wave wave2 = new Wave("BoatBungs #3", 10, 8.0f, 5);
	public Wave wave3 = new Wave("Magpies #4", 15, 8.0f, 5);
	public Wave boss = new Wave("Digimin #5 (Boss)", 50, 4.0f, 1);
	public Wave[] waves;
	public bool active;

	Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
		start = GameObject.Find("start");
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		start.GetComponent<Renderer>().material.color = Color.green;
		startPosition = start.transform.position;
		waves = new Wave[] { wave0, wave1, wave2, wave3, boss };
		waveCounter = 0;
		currentWave = waves[waveCounter];
		enemiesRemainingToSpawn = currentWave.getSize();
		enemiesInScene = 0;
		gameManager.updateWaveString();
		active = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(active) {
			timeBetweenMobs -= Time.deltaTime;
			if (timeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) {
				Instantiate(enemy, startPosition, Quaternion.Euler(0f, 0f, 0f));
				enemiesInScene += 1;
				timeBetweenMobs = 2.0f;
				enemiesRemainingToSpawn--;
			}
			start.transform.Rotate(75 * Time.deltaTime, 0, 0);
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
			gameManager.updateWaveString();
		}
	}

	public Wave getCurrentWave() {
		return currentWave;
    }

	public void setSpawnState(bool state) {
		this.active = state;
    }

}
