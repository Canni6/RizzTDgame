using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TowerScript : MonoBehaviour
{
	public Material highlightTarget;
    // try max range only
    public float maxRange = 10.0f;
    Vector3 towerPosition;
    Vector3 enemyPosition;
    GameObject closestEnemy = null;
    public Transform turret = transform.FindChild
    public Transform target;

    public void Start()
    {
        // locate tower
        Vector3 towerOffset = new Vector3(0, 1.5f, 0);
        towerPosition = transform.position + towerOffset;
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
                enemy.GetComponent<Renderer>().material.color = Color.cyan;
                print("No enemies found");
            }
        }
        return closestEnemy;
    }
        // updates every frame
    private void FixedUpdate()
    {
        FindClosestEnemy(maxRange);
    }

    private void Update()
    {
        transform.LookAt(target);
    }
}
