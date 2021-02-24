using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game currency
    public int credit;
    // Player lives
    public int life;
    
    // Start is called before the first frame update
    void Start()
    {
        credit = 0;
        life = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCredit(int creditToAdd) {
        credit = credit + creditToAdd;
    }

    public void addLife(int lifeToAdd) {
        life = life + lifeToAdd;
    }

    public int getCredit() {
        //print("current credit is: " + credit);
        return credit;
    }
    public int getLife() {
        return life;
    }
}
