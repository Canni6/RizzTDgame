using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerScript : MonoBehaviour
{
	public Material highlightTarget;
    // public float minRange = 5.0f;
    // try max range only
    
    public float maxRange = 10.0f;

    // create reference to SpawnerScript for later use
    private SpawnerScript spawnScript;
    // create empty list of enemy proximites to populate later
    List<float> enemyProximities = null;


    Vector3 towerPosition;
    public void Start()
    {
        // create instance of SpawnerScript
        spawnScript = new SpawnerScript();
        Vector3 towerOffset = new Vector3(0, 1.5f, 0);
        Vector3 towerPosition = transform.position + towerOffset;
        print("Position of Tower is: " + towerPosition);
        
    }

    
    public void TrackEnemyProximities()
    {

        List<Transform> towerEnemyTransforms = spawnScript.enemyTransforms;
        foreach (Transform towerEnemyTransform in towerEnemyTransforms)
        {
            float enemyProximity = towerEnemyTransform.position.magnitude - towerPosition.magnitude;
            enemyProximities.Add(enemyProximity);
        }
    }

    public GameObject FindClosestEnemy(float max)
    {
        // THIS FUNCTION is going to execute every frame
               
        // GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy");
        GameObject closestEnemy = null;
        
        
        

        // reset closestDistance
        float closestDistance = Mathf.Infinity;
        
        
        
        //// track enemy transforms
        //List<Transform> enemyTransforms = new List<Transform>();
        //// track distances of each enemy
        //foreach (GameObject enemy in enemies)
        //{
        //    enemyTransforms.Add(enemy.transform);
        //}
        //// print(enemyTransforms);
        //foreach (var enemyItem in enemyTransforms)
        //{
        //    print("Enemy list transform is located at: " + enemyItem.transform.position);
        //}
        // TO DO: track each enemyItem's transform and use for comparison to tower position


        
        // loop through all enemies in scene
        foreach (GameObject enemy in enemies)
        {
                //// reset object colour
                //enemy.GetComponent<Renderer>().material.color = Color.cyan;
            // calculate distance between enemy and tower
            Vector3 diff = enemy.transform.position - towerPosition;
            // convert Vector3 to float with .magnitude
            float distanceToEnemy = diff.magnitude;
            // Debug.Log("min distance: " + min);
            Debug.Log("max distance: " + max);
            // Debug.Log("distance to enemy: " + distanceToEnemy);
            
            // check that current enemy is closest in the scene and in range (>= min <= max range values)
            if (distanceToEnemy < closestDistance && distanceToEnemy <= max)
            {
                // set new closest distance
                closestDistance = distanceToEnemy;
                // set closestEnemy equal to current loop item
                closestEnemy = enemy;
                print("found closest enemy" + closestEnemy);
                // highlight closest enemy
                closestEnemy.GetComponent<Renderer>().material = highlightTarget;
                // set distance to current distance
            }
            // if enemy out of range, remove target highlight
            //else if (distanceToEnemy < min && distanceToEnemy > max)
            //{
            //    enemy.GetComponent<Renderer>().material.color = Color.cyan;
            //}
        }
        return closestEnemy;
    }

    // updates every frame
    private void Update()
    {
        
        // create local list as copy of list in spawnScript instance
        
        FindClosestEnemy(maxRange);
    }

    private void FixedUpdate()
    {
        
    }
}
