using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : MonoBehaviour
{
	public Material highlightTarget;
    // try max range only
    public float maxRange = 10.0f;
    public float timeBetweenShots = 2.0f;
    public GameObject projectile;
    public Transform projectileSpawn;
    // public GameObject projectileSpawn;
    Vector3 towerPosition;
    Vector3 enemyPosition;
    Vector3 targetPosition;
    
    GameObject closestEnemy = null;
    public Transform target = null;


    private void Awake()
    {

    }

    public void Start()
    {
        // locate tower
        Vector3 towerOffset = new Vector3(0, 1.5f, 0);
        towerPosition = this.transform.position + towerOffset;
        projectileSpawn = this.gameObject.transform.GetChild(2);
        // projectileSpawn = GameObject.Find("ProjectileSpawn");  
    }
       
    // if target exists, fire projectile at it every x seconds
    public void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            // create projectile
            //GameObject projectileGO = (GameObject) Instantiate(projectile, this.projectileSpawn.transform.position, this.projectileSpawn.transform.rotation);
            GameObject projectileGO = (GameObject)Instantiate(projectile, projectileSpawn.position, this.projectileSpawn.rotation);
            ProjectileScript projectileRef = projectileGO.GetComponent<ProjectileScript>();
            Debug.Log("projectile created");

            if(projectileRef != null)
            {
                projectileRef.Seek(target);
                print("Target found");
            }

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
            //projectileScript.target = target;
            //print("target is set");

            // turret only rotates about y axis
            Vector3 targetPosition = new Vector3(enemyPosition.x, (this.transform.position.y), enemyPosition.z);
            // fix 90 degree rotation offset
            transform.right = (targetPosition - transform.position);
            // timer for shooting
            timeBetweenShots -= Time.deltaTime;
            // execute shoot function to create projectile
            Shoot();
        }
    }
}
