using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/// <summary>
/// This class contains the initial conditions of each wave of enemies in the game.
/// </summary>
public class Wave
{
    public enum TYPE {
        Standard,
        Magic,
        Brute
    }

    private string name;
    private int health;
    private float speed;
    private TYPE type;
    private int size;
    private GameObject go;

    // unspecified Wave type constructor
    public Wave(string name, int health, float speed, int size, GameObject go) {
        this.name = name;
        this.health = health;
        this.speed = speed;
        this.size = size;
        this.type = TYPE.Standard;
        this.go = go;
    }

    public Wave(string name, int health, float speed, int size, TYPE type) {
        this.name = name;
        this.health = health;
        this.speed = speed;
        this.size = size;
        this.type = type;
    }
    
    public int getHealth() {
        return health;
    }
    public int getSize() {
        return size;
    }

    public float getSpeed() {
        return speed;
    }

    public TYPE getType() {
        return type;
    }

    public string getName() {
        return name;
    }

    
}
