﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.NetworkInformation;

public class TowerScript : MonoBehaviour
{
    private bool active;
	public Material highlightTarget;
    // try max range only
    public float maxRange = 10.0f;
    public float timeBetweenShots;
    public float fireRate;
    public GameObject projectile;
    public Transform projectileSpawn;
    // public GameObject projectileSpawn;
    Vector3 towerPosition;
    Vector3 enemyPosition;
    Vector3 targetPosition;
    public bool built = false;
    
    GameObject closestEnemy = null;
    public Transform target = null;
    GameObject projectileGO = null;
    public GameObject gameManager;
    public SoundManager soundManager;

    // store alternate initial and temporary material for mouseover
    public Material materialInit;
    public Material materialTemp;

    // Tower Affix to be applied to projectile as well
    public enum Affix {
        Basic,
        Frost,
        Rapid
    }

    public Affix affix;

    public void Start()
    {
        gameManager = GameObject.Find("GameManager");
        soundManager = gameManager.GetComponent<SoundManager>();
        // get the tower base's material
        materialInit = gameObject.transform.GetChild(0).GetComponent<Renderer>().material;
        materialTemp = (Material)Resources.Load("Materials/Tower_Highlight");
        // locate tower
        Vector3 towerOffset = new Vector3(0, 1.5f, 0);
        towerPosition = this.transform.position + towerOffset;
        projectileSpawn = this.gameObject.transform.GetChild(2); // hard-coded attached object index
        timeBetweenShots = 1.0f / fireRate;
    }
    
    public void instantiateProjectile() {
        // create projectile
        projectileGO = Instantiate(projectile, projectileSpawn.position, this.projectileSpawn.rotation);
        // assign state for enemy AI interaction
        projectileGO.tag = "projectile";
        projectileGO.gameObject.AddComponent<ProjectileScript>();
        if(affix == Affix.Basic) {
            projectileGO.GetComponent<ProjectileScript>().setAffix(ProjectileScript.Affix.Basic);
        } else if (affix == Affix.Frost) {
            projectileGO.GetComponent<ProjectileScript>().setAffix(ProjectileScript.Affix.Frost);
        } else if (affix == Affix.Rapid) {
            projectileGO.GetComponent<ProjectileScript>().setAffix(ProjectileScript.Affix.Rapid);
        }
        // set projectile target
        projectileGO.GetComponent<ProjectileScript>().Seek(target);
    }


    // if target exists, fire projectile at it every x seconds
    public void Shoot()
    {
        if (timeBetweenShots <= 0)
        {
            instantiateProjectile();
            if (affix == Affix.Rapid) {
                soundManager.playSound(soundManager.audioShotRapid);
            }
            else {
                soundManager.playSound(soundManager.audioShotCannon);
            }
            // reset timer
            timeBetweenShots = 1.0f / fireRate;
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
                    //print("Found closest enemy" + closestEnemy);
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
                //print("No enemies found");
            }
        }
        return closestEnemy;
    }
        // updates every frame
    public void FixedUpdate()
    {
        if(built) {
            FindClosestEnemy(maxRange);
        }           
    }

    public void Update()
    {
        /* code references: https://answers.unity.com/questions/36255/lookat-to-only-rotate-on-y-axis-how.html
                            https://answers.unity.com/questions/950010/offset-lookat-rotation.html */

        // tower target to only consider y axis
        if (target) {
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
    
    public void setBuilt() {
        built = true;
    }

    public float getRange() {
        return maxRange;
    }

    public void setRange(float range) {
        this.maxRange = range;
    }

    // fire rate in projectiles per second
    public void setFireRate(float fireRate) {
        this.fireRate = fireRate;
    }

    public float getFireRate() {
        return fireRate;
    }

    public void setAffix(Affix affix) {
        this.affix = affix;
        if(affix == Affix.Basic) {
            setRange(BuildManager.range_basic);
            setFireRate(BuildManager.rate_basic);
        }
        if(affix == Affix.Frost) {
            setRange(BuildManager.range_frost);
            setFireRate(BuildManager.rate_frost);
        }
        if(affix == Affix.Rapid) {
            setRange(BuildManager.range_rapid);
            setFireRate(BuildManager.rate_rapid);
        }
    }

    public Affix getAffix() {
        return affix;
    }
    
    // Selectable Towers
    void OnMouseEnter() {
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material = materialTemp;
    }

    void OnMouseExit() {
        gameObject.transform.GetChild(0).GetComponent<Renderer>().material = materialInit;
    }
}
