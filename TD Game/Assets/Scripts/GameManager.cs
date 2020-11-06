using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // reference to player 1 game object
    public GameObject player1GO = null;
    //// reference to player 2 game object
    //public GameObject player2GO = null;

    // references to player scripts on the game objects
    Player player1Ref;
    //Player player2ref;
    string playerCreditString;

    // Start is called before the first frame update
    void Start() {
        // instantiate players and add Player scripts to them
        player1GO = GameObject.Find("Player");
        player1Ref = player1GO.GetComponent<Player>();
        // Add some credit using player script reference
        player1Ref.addCredit(5);
        playerCreditString = "Bloka's cash: " + player1Ref.getCredit();

        //player2GO = Instantiate(player2GO);
        //player2GO.AddComponent<Player>();
        //player2ref = player2GO.GetComponent<Player>();

        //player2ref.addCredit(10);

        print("Player 1's credit: " + player1Ref.getCredit());

    }

    // Update is called once per frame
    void Update() {
        
    }

    public int getPlayerCredit() {
        return player1Ref.getCredit();
    }

    public void addPlayerCredit(int credit) {
        player1Ref.addCredit(credit);
        playerCreditString = "Bloka's cash: " + player1Ref.getCredit();
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2 + Screen.width / 4, Screen.height / 2 + Screen.height / 3, 1000, 200), playerCreditString);
    }
}
