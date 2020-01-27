using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // TO DO: fix projectile rotation

    // target reference for the projectile
    public Transform target;
    public float speed = 20f;
    private Vector3 worldUp;

    // transform to pass in from the Tower Script - Brackeys tutorial: https://www.youtube.com/watch?v=oqidgRQAMB8
    public void Seek (Transform _target)
    {
        target = _target;
    }

    public void Awake()
    {
        this.transform.Rotate(0, 0, 90f, Space.World);
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        if(target == null)
        {
            Destroy(gameObject);
            print("target null - projectile destroyed");
            return;
        }

        Vector3 dir = target.position - transform.position;
        // FIX: Vector3.RotateTowards(this, target, 10 * Time.deltaTime);

        // check if position of enemy and projectile are approximately equal.
        if (Vector3.Distance(transform.position, target.position) <= 1f)
        {
            Destroy(gameObject);
            print("collision - projectile destroyed!");
            return;
        }
        this.transform.Translate(dir.normalized * step, Space.World);
        print("projectile moved");

    }
}
