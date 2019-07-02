using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
	public Material highlightTarget;
    public float minRange = 5.0f;
    public float maxRange = 50.0f;

    public GameObject FindClosestEnemy(float min, float max)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
        {
            print("No enemies found");
        }
        if (enemies.Length > 0)
        {
            print("Current number of enemies: " + enemies.Length);
        }
        GameObject closestEnemy = null;
        
        Vector3 position = this.transform.position;
        print("Position of Tower is: " + position);

        // calculate squared distances for Unity specific performance quirk
        min = min * min;
        max = max * max;
        // reset closestDistance
        float closestDistance = Mathf.Infinity;

        // loop through all enemies in scene
        foreach (GameObject enemy in enemies)
        {
            // calculate distance between enemy and tower
            Vector3 diff = enemy.transform.position - position;
            // ^ as above - apparently .Magnitude calculation is slower than sqrMagnitude, so we use sqrMagnitude
            float distanceToEnemy = diff.sqrMagnitude; 
            // check that current enemy is closest in the scene and in range (>= min <= max range values)
            if (distanceToEnemy < closestDistance && distanceToEnemy >= min && distanceToEnemy <= max)
            {
                // set new closest distance
                closestDistance = distanceToEnemy;
                // set closestEnemy equal to current loop item
                closestEnemy = enemy;
                print("found closest enemy" + closestEnemy);
                // highlight closest enemy
                enemy.GetComponent<Renderer>().material = highlightTarget;
                // set distance to current distance
            }
            // if enemy out of range, remove target highlight
            else if (distanceToEnemy < min && distanceToEnemy > max)
            {
                enemy.GetComponent<Renderer>().material.color = Color.cyan;
            }
        }
        return closestEnemy;
    }

    private void Update()
    {
        FindClosestEnemy(minRange, maxRange);
    }
}
