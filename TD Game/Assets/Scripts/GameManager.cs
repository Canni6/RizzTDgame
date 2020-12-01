using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // reference to player 1 game object
    public GameObject player1GO = null;
    // references to player scripts on the game objects
    Player player1Ref;
    string playerCreditString;
    string playerLifeString;
    string gameOverString;
    string gameWonString;
    string currentWaveString;
    bool gameOver;
    bool gameWon;
    private GUIStyle guiStyle = new GUIStyle();
    public UserInterface ui;
    public SpawnerScript spawner;
    public GameObject towerGOSelected;
    public GameObject nodeGOSelected;
    public bool towerSelectState = false;
    public string towerSelectedString;

    public Material materialNodeDefault;

    // tower construction value
    const int value_basic = 2;
    const int value_frost = 4;
    const int value_rapid = 8;

    // Start is called before the first frame update
    void Start() {
        // instantiate players and add Player scripts to them
        player1GO = GameObject.Find("Player");
        player1Ref = player1GO.GetComponent<Player>();
        ui = GetComponent<UserInterface>();
        spawner = GetComponent<SpawnerScript>();
        // Add some credit using player script reference
        player1Ref.addCredit(5);
        playerCreditString = "Credit: " + player1Ref.getCredit();
        playerLifeString = "Life: " + player1Ref.getLife().ToString();
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 30;
        gameOverString = "GAME OVER!";
        gameWonString = "YOU WIN!";
        gameOver = false;
        gameWon = false;
        materialNodeDefault = (Material)Resources.Load("Materials/Basic");
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
            setGameOver();
            ui.displayButton(ui.restartButton);
        }
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 - Screen.height / 3, 1000, 200), playerCreditString);
        GUI.Label(new Rect(Screen.width / 2 - Screen.width / 4, Screen.height / 2 - Screen.height / 3, 1000, 200), playerLifeString);
        GUI.Label(new Rect(Screen.width / 2 + 20, Screen.height / 2 - Screen.height / 3, 1000, 200), currentWaveString);
        if(towerSelectState && towerGOSelected != null) {
            towerSelectedString = "Tower type selected: " + towerGOSelected.GetComponentInChildren<TowerScript>().getAffix();
            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 4, 0 + Screen.height / 7, 1000, 200), towerSelectedString);
        }
        
        if (gameOver) {
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 - Screen.height / 12, 1000, 200), gameOverString, guiStyle);
        } else if(gameWon) {
            guiStyle.normal.textColor = Color.green;
            guiStyle.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 8, Screen.height / 2 - Screen.height / 12, 1000, 200), gameWonString, guiStyle);
            ui.displayButton(ui.restartButton);
        }
    }

    public void loadNextObjective() {
        spawner.loadNextWave();
    }

    public void setGameWon() {
        gameWon = true;
        spawner.setSpawnState(false);
        ui.restartButton.gameObject.SetActive(true);
    }

    public void setGameOver() {
        gameOver = true;
        spawner.setSpawnState(false);
        ui.restartButton.gameObject.SetActive(true);
    }

    public void updateWaveString() {
        currentWaveString = "Current wave: " + spawner.getCurrentWave().getName();
    }

    public void updateTowerSelectedString() {
        towerSelectedString = "Tower type selected: " + towerGOSelected.GetComponentInChildren<TowerScript>().getAffix();
    }

    public void updatePlayerCreditString() {
        playerCreditString = "Credit: " + player1Ref.getCredit();
    }

    public GameObject getTowerSelected() {
        return towerGOSelected;
    }

    public void setNodeTowerSelected(GameObject node, GameObject tower) {
        // deselect currently selected node
        if(nodeGOSelected != null) {
            nodeGOSelected.gameObject.GetComponent<Renderer>().material = materialNodeDefault;
            nodeGOSelected.gameObject.GetComponent<BuildNodeScript>().deselectTower();
        }
        towerGOSelected = tower;
        nodeGOSelected = node;
        towerSelectState = true;
        towerSelectedString = "Tower type selected: " + towerGOSelected.GetComponentInChildren<TowerScript>().getAffix();
    }

    public void deselectTower() {
        if(nodeGOSelected != null) {
            nodeGOSelected.gameObject.GetComponent<Renderer>().material.color = Color.white;
            nodeGOSelected.gameObject.GetComponent<Renderer>().enabled = false;
            nodeGOSelected.gameObject.GetComponent<BuildNodeScript>().setTowerSelected(false);
        }
        towerSelectState = false;
        nodeGOSelected = null;
        towerGOSelected = null;
    }

    public bool getTowerSelectState() {
        return towerSelectState;
    }

    public void setTowerSelectState(bool state) {
        towerSelectState = state;
    }

    public void sellTowerSelected() {
        // refund money
        if(towerGOSelected) {
            player1Ref.addCredit(+1 * convertAffixToValue(towerGOSelected.GetComponentInChildren<TowerScript>().getAffix()));
            updatePlayerCreditString();
        }
        // make buildable again
        nodeGOSelected.GetComponentInChildren<BuildNodeScript>().buildableArea = true;
        // destroy GO
        Destroy(towerGOSelected);
    }

    public int convertAffixToValue(TowerScript.Affix affix) {
        int value = 0;
        if(affix == TowerScript.Affix.Basic) {
            value = value_basic / 2;
        } else if (affix == TowerScript.Affix.Frost) {
            value = value_frost / 2;
        } else if (affix == TowerScript.Affix.Rapid) {
            value = value_rapid / 2;
        }
        return value;
    }
}
