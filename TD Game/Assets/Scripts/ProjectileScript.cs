using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{

    public Transform target = null;
    public float speed = 5.0f;
    //private TowerScript towerScript;
    

    // Use this for initialization
    void Start()
    {
        
    }

    private void Awake()
    {
        //towerScript = GetComponent<TowerScript>();
        //towerScript.target = GetComponent<TowerScript>().target;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime; 
        if (target)
        {
            //target = towerScript.target;
            print(target);
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);
            print("projectile moved");
            // check if position of enemy and projectile are approximately equal.
            if (Vector3.Distance(transform.position, target.position) < 0.001f)
            {
                Destroy(gameObject);
                print("projectile destroyed!");
            }
        }
        
    }
}
