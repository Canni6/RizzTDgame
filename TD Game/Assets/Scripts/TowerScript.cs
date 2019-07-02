using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{

	public Material highlightTarget;
    public GameObject FindClosestEnemy(float min, float max)
    {
        // THIS needs fixing - triggers UnityException: Tag: Enemy is not defined.
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        if (enemies.Length == 0)
        {
            print("No enemies found");
        }
        if (enemies.Length > 0)
        {
            foreach (GameObject enemy in enemies)
            {
                print("Current enemies are: " + enemy);
            }
        }
        GameObject closestEnemy = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        // calculate squared distances because of Unity specific sqrMagnitude function
        min = min * min;
        max = max * max;
        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = enemy.transform.position - position;
            float curDistance = diff.sqrMagnitude; // apparently .Magnitude calculation is slower than sqrMagnitude, so we use sqrMagnitude
                                                   // check that current enemy is in range (>= min <= max)
            if (curDistance < distance && curDistance >= min && curDistance <= max)
            {
                // set closestEnemy equal to current loop item
                closestEnemy = enemy;
                print("found closest enemy");
                print(closestEnemy);
                // set distance to current distance
                distance = curDistance;
                enemy.GetComponent<Renderer>().material = highlightTarget;
            }
        }
        return closestEnemy;
    }
}
