using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAiScript : MonoBehaviour 
{
    GameObject waypointGO;  // defines waypoint gameobject
    Transform targetWaypoint; // defines a movement 
    int waypointIndex = 0;
    public float speed;
    GameObject gameManager;
    GameManager gameManagerRef;
    SpawnerScript spawner;
    public int health;
    public int waypointCount;


	// Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
        gameManagerRef = gameManager.GetComponent<GameManager>();
        waypointGO = GameObject.Find("Waypoints");
        spawner = gameManager.GetComponent<SpawnerScript>();
        speed = spawner.getCurrentWave().getSpeed();
        health = spawner.getCurrentWave().getHealth();
        waypointCount = waypointGO.transform.childCount;
    }
    void GetNextWaypoint() {
        if(waypointIndex < (waypointCount)) {
            waypointGO.transform.GetChild(waypointIndex);
            targetWaypoint = waypointGO.transform.GetChild(waypointIndex);
            waypointIndex++;
        }
        else {
            ReachedGoal();
        }
    }
    void ReachedGoal() {
	    Destroy(gameObject);
        // subtract life from player
        gameManagerRef.addPlayerLife(-1);
        print("Reached goal");
        spawner.removeEnemy();
        checkEndRound();
    }
    // Update is called once per frame
    void Update () {
        if(targetWaypoint == null) {
            GetNextWaypoint();
        }
	    float step = speed * Time.deltaTime; // step size is equal to speed times frame time.
	    transform.position = Vector3.MoveTowards (transform.position, targetWaypoint.position, step);
	    Vector3 direction = targetWaypoint.position - this.transform.localPosition; // direction to our target
	    if (direction.magnitude <= step) {
            // we reached the waypoint
            GetNextWaypoint();
		}
	    else {
	        //move towards waypoint
		    transform.Translate( direction.normalized * step );
		}
	}

    public void setSpeed(float speed) {
        this.speed = speed;
    }

    void OnCollisionEnter(Collision something) {
        if (something.gameObject.tag.Equals("projectile") == true) {
            // apply projectile affix debuff - just frost atm
            if(something.gameObject.GetComponent<ProjectileScript>().getAffix() == ProjectileScript.Affix.Frost) {
                // apply 50% speed debuff (min speed cap)
                if(speed > 2f) {
                    setSpeed(speed * 0.5f);
                }
                // apply blue color
                this.gameObject.GetComponent<Renderer>().material.color = Color.blue;
            }
            health -= 1;
            if(health < 1) {
                Destroy(gameObject);
                print("collision - enemy destroyed!");
                gameManagerRef.addPlayerCredit(1);
                print("Added to player score");
                spawner.removeEnemy();
                // check end of round condition
                checkEndRound();
            }
        }
    }

    public void checkEndRound() {
        if (spawner.getEnemiesInScene() == 0 && spawner.getEnemiesRemainingToSpawn() == 0) {
            gameManagerRef.loadNextObjective();
        }
    }

    
}