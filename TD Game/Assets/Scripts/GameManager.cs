using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // reference to player 1 game object
    public GameObject player1GO = null;
    // references to player scripts on the game objects
    Player player1Ref;
    //Player player2ref;
    string playerCreditString;
    string playerLifeString;
    string gameOverString;
    bool gameOver;
    // 
    private GUIStyle guiStyle = new GUIStyle();

    // Start is called before the first frame update
    void Start() {
        // instantiate players and add Player scripts to them
        player1GO = GameObject.Find("Player");
        player1Ref = player1GO.GetComponent<Player>();
        // Add some credit using player script reference
        player1Ref.addCredit(5);
        playerCreditString = "Credit: " + player1Ref.getCredit();
        playerLifeString = "Life: " + player1Ref.getLife().ToString();
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 30;
        gameOverString = "GAME OVER!";
        gameOver = false;
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int getPlayerCredit() {
        return player1Ref.getCredit();
    }

    public void addPlayerCredit(int credit) {
        player1Ref.addCredit(credit);
        playerCreditString = "Credit: " + player1Ref.getCredit();
    }

    public void addPlayerLife(int life) {
        player1Ref.addLife(life);
        playerLifeString = "Life: " + player1Ref.getLife().ToString();
        if(player1Ref.getLife() < 1) {
            gameOver = true;
        }
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2 - Screen.height / 3, 1000, 200), playerCreditString);
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 4, Screen.height / 2 - Screen.height / 3, 1000, 200), playerLifeString);
        if(gameOver) {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 1000, 200), gameOverString, guiStyle);
        }
    }
}
