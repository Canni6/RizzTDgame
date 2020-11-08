using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game currency
    private int credit;
    // Player lives
    private int life;
    
    // Start is called before the first frame update
    void Start()
    {
        credit = 0;
        life = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCredit(int creditToAdd) {
        credit += creditToAdd;
    }

    public void addLife(int lifeToAdd) {
        life += lifeToAdd;
    }

    public int getCredit() {
        return credit;
    }
    public int getLife() {
        return life;
    }
}
