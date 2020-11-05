using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIscript : MonoBehaviour 
{

    GameObject waypointGO;  // defines waypoint gameobject
    Transform targetWaypoint; // defines a movement 
    int waypointIndex = 0;
    public float Speed;

    // TO DO:
    // Deal with projectiles: die
    // On death: Add to player economy/score

	// Use this for initialization
    void Start () 
    {
        waypointGO = GameObject.Find("Waypoints");
        Speed = 3.0f;
    }
    void GetNextWaypoint()
    {
        targetWaypoint = waypointGO.transform.GetChild(waypointIndex);
        waypointIndex++; // check this implementation
    }
    void ReachedGoal()
	{
	    Destroy(gameObject);
	}
    // Update is called once per frame
    void Update () 
    {
        if(targetWaypoint == null)
        {
            GetNextWaypoint();
        }
	    float step = Speed * Time.deltaTime; // step size is equal to speed times frame time.
	    transform.position = Vector3.MoveTowards (transform.position, targetWaypoint.position, step);
	    Vector3 direction = targetWaypoint.position - this.transform.localPosition; // direction to our target
	    if (direction.magnitude <= step)
	    // we reached the waypoint
		{
		    GetNextWaypoint();
		    if(waypointIndex == 16)
			{
			    ReachedGoal();
			}
		}
	    else
		{
	        //move towards waypoint
		    transform.Translate( direction.normalized * step );
		}
	}

    void OnCollisionEnter(Collision target) {
        if (target.gameObject.tag.Equals("projectile") == true) {
            Destroy(gameObject);
            print("collision - enemy destroyed!");
        }
    }
}