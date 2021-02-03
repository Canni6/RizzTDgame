using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour {

	public GameManager gameManager;
	public SoundManager soundManager;
	public GameObject enemy;
	public GameObject start;

	public float timeBetweenMobs = 2.0f;
	public float countdown = 10.0f;
	public int enemiesRemainingToSpawn;
	public int enemiesInScene;

	public int waveCounter;
	public Wave currentWave;
	public Wave wave0 = new Wave("Dots #1", 3, 6.0f, 5);
	public Wave wave1 = new Wave("MoreDots #2", 6, 8.0f, 5);
	public Wave wave2 = new Wave("Whelps #3", 10, 10.0f, 5);
	public Wave wave3 = new Wave("ManyWhelps #4", 10, 10.0f, 10);
	public Wave boss = new Wave("Handle it! #5 (Boss)", 50, 4.0f, 1);
	public Wave[] waves;
	public bool active;
	public bool counted;
	public string countString;
	public float countMax = 10.0f;
	private GUIStyle guiStyle = new GUIStyle();

	Vector3 startPosition;

	// Use this for initialization
	void Start ()
    {
		start = GameObject.Find("start");
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		soundManager = gameManager.GetComponent<SoundManager>();
		start.GetComponent<Renderer>().material.color = Color.green;
		startPosition = start.transform.position;
		waves = new Wave[] { wave0, wave1, wave2, wave3, boss };
		waveCounter = 0;
		currentWave = waves[waveCounter];
		enemiesRemainingToSpawn = currentWave.getSize();
		enemiesInScene = 0;
		gameManager.updateWaveString();
		active = true;
		counted = false;
		guiStyle.normal.textColor = Color.red;
		guiStyle.fontSize = 30;
	}
	
	// Update is called once per frame
	void Update ()
    {
		countdown -= Time.deltaTime;
		countString = Mathf.Round(countdown).ToString();
		if (countdown >= 0.0f) {
			// play sound each second of countdown
			if(countMax - countdown > 1.0f) {
				countMax -= 1;
				playBlip();
            }
		}
		else {
			counted = true;
        }
		if(active && counted) {
			timeBetweenMobs -= Time.deltaTime;
			if (timeBetweenMobs <= 0.0f && enemiesRemainingToSpawn > 0) {
				Instantiate(enemy, startPosition, Quaternion.Euler(0f, 0f, 0f));
				enemiesInScene += 1;
				timeBetweenMobs = 2.0f;
				enemiesRemainingToSpawn--;
			}
			start.transform.Rotate(0, -100 * Time.deltaTime, 0);
		}
		
	}

	void OnGUI() {
		if(!counted) {
			GUI.Label(new Rect(Screen.width / 2, Screen.height / 4, 1000, 200), countString, guiStyle);
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

	public void playBlip() {
		soundManager.playSound(soundManager.audioButtonBlip);
    }
}
