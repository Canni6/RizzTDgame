using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    // target reference for the projectile
    public Transform target;
    public float speed;

    // Projectile affix to be set from Tower. Interacts with enemy AI.
    public enum Affix {
        Basic,
        Frost,
        Rapid
    }

    public Affix affix;

    void Start() {
        // default speed
        speed = 20f;
    }

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
        this.transform.Translate(dir.normalized * step, Space.World);
        print("projectile moved");

    }


    void OnCollisionEnter(Collision target) {
        if(target.gameObject.tag.Equals("enemy") == true ) {
            Destroy(gameObject);
            print("collision - projectile destroyed!");
        }
    }

    public ProjectileScript.Affix getAffix() {
        return affix;
    }

    public void setAffix(Affix affix) {
        this.affix = affix;
    }

    public void setSpeed(float speed) {
        this.speed = speed;
    }
}
