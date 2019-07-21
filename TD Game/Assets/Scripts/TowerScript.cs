using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : MonoBehaviour
{
	public Material highlightTarget;
    // try max range only
    public float maxRange = 10.0f;
    public float projectileSpeed = 5.0f;
    public float timeBetweenShots = 2.0f;
    public GameObject projectile;
    public GameObject projectileSpawn;
    Vector3 towerPosition;
    Vector3 enemyPosition;
    Vector3 targetPosition;
    private ProjectileScript projectileScript;
    


    GameObject closestEnemy = null;
    public Transform target = null;


    private void Awake()
    {
        projectileScript = GetComponent<ProjectileScript>();
    }

    public void Start()
    {
        // locate tower
        Vector3 towerOffset = new Vector3(0, 1.5f, 0);
        towerPosition = transform.position + towerOffset;

        projectileSpawn = GameObject.Find("ProjectileSpawn");
        
    }

    //// projectile collision
    //public void OnCollisionEnter(Collision collision)
    //{
    //    Destroy(gameObject);
    //}
    
    
    // if target exists, fire projectile at it every x seconds
    public void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            // create projectile
            Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
            Debug.Log("projectile created");

            // reset timer
            timeBetweenShots = 2.0f;
        }
    }

    public GameObject FindClosestEnemy(float maxRange)
    {
        // This function is going to execute every frame
        
        // reset closest distance before each loop
        float closestDistance = Mathf.Infinity;
        // refresh array of enemies in scene before each loop
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject enemy in enemies)
        {
            enemyPosition = enemy.transform.position;
            float enemyProximity = Vector3.Distance(enemyPosition, towerPosition);
            if (enemyProximity <= maxRange & enemyProximity < closestDistance)
            {
                closestDistance = enemyProximity;
                // lock on to target whilst in range
                while (enemyProximity <= maxRange)
                {
                    closestEnemy = enemy;
                    print("Found closest enemy" + closestEnemy);
                    closestEnemy.GetComponent<Renderer>().material = highlightTarget;
                    target = closestEnemy.transform;
                    return closestEnemy;
                }
            }
            else
            {
                closestEnemy = null;
                target = null;
                enemy.GetComponent<Renderer>().material.color = Color.cyan;
                print("No enemies found");
            }
        }
        return closestEnemy;
    }
        // updates every frame
    public void FixedUpdate()
    {
        FindClosestEnemy(maxRange);
    }

    public void Update()
    {
        /* code references: https://answers.unity.com/questions/36255/lookat-to-only-rotate-on-y-axis-how.html
                            https://answers.unity.com/questions/950010/offset-lookat-rotation.html */

        // tower target to only consider y axis
        
        if (target)
        {
            projectileScript.target = target;

            // turret only rotates about y axis
            Vector3 targetPosition = new Vector3(enemyPosition.x, (this.transform.position.y), enemyPosition.z);
            // fix 90 degree rotation offset
            transform.right = (targetPosition - transform.position);
            // timer for shooting
            timeBetweenShots -= Time.deltaTime;
            // execute shoot function to create projectile
            Shoot();
            

            //// distance for projectile to move
            //float step = projectileSpeed * Time.deltaTime;
            //// move projectile toward target
            //projectile.transform.position = Vector3.MoveTowards(projectile.transform.position, enemyPosition, step);
            //print("projectile moved");
            //// check if position of enemy and projectile are approximately equal.
            //if (Vector3.Distance(projectile.transform.position, enemyPosition) < 0.001f)
            //{
            //    Destroy(projectile);
            //    print("projectile destroyed!");
            //}
        }
    }
}
