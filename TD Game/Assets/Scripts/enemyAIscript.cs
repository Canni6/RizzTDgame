using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiScript : MonoBehaviour 
{
    GameObject waypointGO;  // defines waypoint gameobject
    public Transform targetWaypoint; // defines a movement 
    public int waypointIndex = 0;
    public float speed;
    GameObject gameManager;
    GameManager gameManagerRef;
    SpawnerScript spawner;
    public int health;
    public int healthMax;
    public int waypointCount;
    public float distanceToTarget;
    public SoundManager soundManager;
    public GameObject healthBar;
    Vector3 healthBarOffset;
    Vector3 healthBarScale;


	// Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
        gameManagerRef = gameManager.GetComponent<GameManager>();
        soundManager = gameManager.GetComponent<SoundManager>();
        waypointGO = GameObject.Find("Waypoints");
        spawner = gameManager.GetComponent<SpawnerScript>();
        speed = spawner.getCurrentWave().getSpeed();
        healthMax = spawner.getCurrentWave().getHealth();
        health = healthMax;
        waypointCount = waypointGO.transform.childCount;
        healthBar = (GameObject)Resources.Load("Prefabs/healthBar");
        healthBarOffset = new Vector3(0.0f, 5.0f, 0.0f);
        
        healthBar = Instantiate(healthBar, transform);
        healthBarScale = new Vector3(health / healthMax, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        healthBar.transform.position = transform.position + healthBarOffset;
    }
    void GetNextWaypoint() {
        if(waypointIndex < (waypointCount)) {
            waypointGO.transform.GetChild(waypointIndex);
            targetWaypoint = waypointGO.transform.GetChild(waypointIndex);
            waypointIndex++;
            transform.LookAt(targetWaypoint);
        }
        else {
            ReachedGoal();
        }
    }
    void ReachedGoal() {
	    Destroy(gameObject);
        // subtract life from player
        gameManagerRef.addPlayerLife(-1);
        //print("Reached goal");
        spawner.removeEnemy();
        soundManager.playSound(soundManager.audioDeathBio);
        checkEndRound();
    }
    // Update is called once per frame
    void Update () {
        if(targetWaypoint == null) {
            GetNextWaypoint();
        }
	    float step = speed * Time.deltaTime; // step size is equal to speed times frame time.
        // move enemy
	    transform.position = Vector3.MoveTowards (transform.position, targetWaypoint.position, step);
        // move health bar as well
        healthBar.transform.position = transform.position + healthBarOffset;
        // distance to our target
        Vector3 targetDirection = targetWaypoint.position - transform.localPosition;
        // if we reached the waypoint
        if (targetDirection.magnitude <= step) {            
            GetNextWaypoint();
		}
	}

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    void OnCollisionEnter(Collision something) {
        if (something.gameObject.tag.Equals("projectile") == true) {
            // apply projectile affix debuff - just frost atm
            if(something.gameObject.GetComponent<ProjectileScript>().getAffix() 
                == ProjectileScript.Affix.Frost) {
                // apply 50% speed debuff (min speed cap)
                if(speed > 2f) {
                    setSpeed(speed * 0.5f);
                }
                // apply blue color
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            health -= 1;
            // update health bar
            healthBar.transform.localScale = new Vector3(healthBar.transform.localScale.x *
                                            ((float)health / (float)healthMax), 
                                            healthBar.transform.localScale.y,
                                            healthBar.transform.localScale.z);
            if (health < 1) {
                //print("collision - enemy destroyed!");
                gameManagerRef.addPlayerCredit(1);
                //print("Added to player score");
                spawner.removeEnemy();
                soundManager.playSound(soundManager.audioDeathBio);
                // check end of round condition
                checkEndRound();
                Destroy(gameObject);
            }
        }
    }

    public void checkEndRound() {
        if (spawner.getEnemiesInScene() == 0 && spawner.getEnemiesRemainingToSpawn() == 0) {
            gameManagerRef.loadNextObjective();
        }
    }

    
}