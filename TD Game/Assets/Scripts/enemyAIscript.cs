using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIscript : MonoBehaviour 
{
    GameObject waypointGO;  // defines waypoint gameobject
    Transform targetWaypoint; // defines a movement 
    int waypointIndex = 0;
    public float Speed;
    GameObject gameManager;
    GameManager gameManagerRef;
    public int health;
    public int waypointCount;

	// Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
        gameManagerRef = gameManager.GetComponent<GameManager>();
        waypointGO = GameObject.Find("Waypoints");
        Speed = 20.0f;
        health = 3;
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
	}
    // Update is called once per frame
    void Update () {
        if(targetWaypoint == null) {
            GetNextWaypoint();
        }
	    float step = Speed * Time.deltaTime; // step size is equal to speed times frame time.
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

    void OnCollisionEnter(Collision target) {
        if (target.gameObject.tag.Equals("projectile") == true) {
            health -= 1;
            if(health < 1) {
                Destroy(gameObject);
                print("collision - enemy destroyed!");
                gameManagerRef.addPlayerCredit(1);
                print("Added to player score");
            }
        }
    }
}