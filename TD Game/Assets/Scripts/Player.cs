using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Game currency
    private int credit;
    
    // Start is called before the first frame update
    void Start()
    {
        credit = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addCredit(int collectedCredit) {
        credit += collectedCredit;
    }

    public int getCredit() {
        return credit;
    }
}
