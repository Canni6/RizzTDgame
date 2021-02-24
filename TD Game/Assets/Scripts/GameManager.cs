using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    // reference to player 1 game object
    public GameObject player1GO;
    // references to player scripts on the game objects
    string playerCreditString;
    string playerLifeString;
    string gameOverString;
    string gameWonString;
    string currentWaveString;
    bool gameOver;
    bool gameWon;
    private GUIStyle guiStyle = new GUIStyle();
    private GUIStyle mainDisplayStyle = new GUIStyle();
    private GUIStyle towerSelectStyle = new GUIStyle();
    public UserInterface ui;
    public SpawnerScript spawner;
    public GameObject towerGOSelected;
    public GameObject nodeGOSelected;
    public bool towerSelectState = false;
    public string towerSelectedString;
    public GameObject sellMenuButton;
    public Material materialNodeDefault;
    private float scale;
    public GameObject mainCamera;
    public Vector3 camPos;
    public bool paused = false;

    // Start is called before the first frame update
    void Start() {
        // instantiate players and add Player scripts to them
        player1GO = GameObject.Find("Player1");
        ui = GetComponent<UserInterface>();
        spawner = GetComponent<SpawnerScript>();
        player1GO.GetComponent<Player>().addCredit(10);
        player1GO.GetComponent<Player>().addLife(5);
        playerCreditString = "Credit: " + player1GO.GetComponent<Player>().getCredit();
        playerLifeString = "Life: " + player1GO.GetComponent<Player>().getLife().ToString();
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 30;
        mainDisplayStyle.normal.textColor = Color.green;
        mainDisplayStyle.fontSize = 20;
        towerSelectStyle.normal.textColor = Color.yellow;
        towerSelectStyle.fontSize = 20;
        gameOverString = "GAME OVER!";
        gameWonString = "YOU WIN!";
        gameOver = false;
        gameWon = false;
        materialNodeDefault = (Material)Resources.Load("Materials/Basic");
        sellMenuButton = GameObject.Find("SellMenuButton");
    }

    public int getPlayerCredit() {
        return player1GO.GetComponent<Player>().getCredit();
    }

    public void addPlayerCredit(int credit) {
        player1GO.GetComponent<Player>().addCredit(credit);
        playerCreditString = "Credit: " + player1GO.GetComponent<Player>().getCredit();
    }

    public void addPlayerLife(int life) {
        player1GO.GetComponent<Player>().addLife(life);
        playerLifeString = "Life: " + player1GO.GetComponent<Player>().getLife().ToString();
        if(player1GO.GetComponent<Player>().getLife() < 1) {
            setGameOver();
            ui.displayButton(ui.restartButton);
        }
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width / 24, Screen.height / 24, 1000, 200), 
            currentWaveString, mainDisplayStyle);
        GUI.Label(new Rect(Screen.width / 24, Screen.height / 24 + 25, 1000, 200), 
            playerLifeString, mainDisplayStyle);
        GUI.Label(new Rect(Screen.width / 24, Screen.height / 24 + 50, 1000, 200), 
            playerCreditString, mainDisplayStyle);
        
        // display selected tower info
        if(towerSelectState && towerGOSelected != null) {
            towerSelectedString = "Selected tower: " + 
                towerGOSelected.GetComponentInChildren<TowerScript>().getAffix()
                + "\n" + "Range: " + towerGOSelected.GetComponentInChildren<TowerScript>().getRange()
                + "\n" + "Fire-rate: " + towerGOSelected.GetComponentInChildren<TowerScript>().getFireRate();
            GUI.Label(new Rect(Screen.width / 24, Screen.height / 24 + 100, 1000, 200), 
                        towerSelectedString, towerSelectStyle);
        }
        
        if (gameOver) {
            guiStyle.normal.textColor = Color.red;
            guiStyle.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - Screen.width / 24, 
                        Screen.height / 2 - Screen.height / 6, 1000, 200), gameOverString, guiStyle);
        }
        else if (gameWon) {
            guiStyle.normal.textColor = Color.green;
            guiStyle.fontSize = 30;
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 - 
                        Screen.height / 10 - 50, 1000, 200), gameWonString, guiStyle);
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

    public void updatePlayerCreditString() {
        playerCreditString = "Credit: " + player1GO.GetComponent<Player>().getCredit();
    }

    public GameObject getTowerSelected() {
        return towerGOSelected;
    }

    public void setNodeTowerSelected(GameObject node, GameObject tower) {
        // deselect previously selected node
        if(nodeGOSelected != null) {
            nodeGOSelected.gameObject.GetComponent<Renderer>().material = materialNodeDefault;
            nodeGOSelected.gameObject.GetComponent<BuildNodeScript>().deselectTower();
        }
        towerGOSelected = tower;
        nodeGOSelected = node;
        towerSelectState = true;
        towerSelectedString = "Tower type selected: " + towerGOSelected.GetComponentInChildren<TowerScript>().getAffix();
        sellMenuButton.SetActive(true);
    }

    public void deselectTower() {
        if(nodeGOSelected != null) {
            nodeGOSelected.gameObject.GetComponent<Renderer>().material.color = Color.white;
            nodeGOSelected.gameObject.GetComponent<Renderer>().enabled = false;
            nodeGOSelected.gameObject.GetComponent<BuildNodeScript>().setTowerSelected(false);
            nodeGOSelected.gameObject.GetComponent<BuildNodeScript>().hideIndicator();
        }
        towerSelectState = false;
        nodeGOSelected = null;
        towerGOSelected = null;
        sellMenuButton.SetActive(false);
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
            player1GO.GetComponent<Player>().addCredit(+1 * convertAffixToSellValue(towerGOSelected.GetComponentInChildren<TowerScript>().getAffix()));
            updatePlayerCreditString();
        }
        // make buildable again
        nodeGOSelected.GetComponentInChildren<BuildNodeScript>().buildableArea = true;
        // destroy GO
        Destroy(towerGOSelected);
        sellMenuButton.SetActive(false);
        
    }

    public int convertAffixToSellValue(TowerScript.Affix affix) {
        int value = 0;
        if(affix == TowerScript.Affix.Basic) {
            value = BuildManager.value_basic / 2;
        } else if (affix == TowerScript.Affix.Frost) {
            value = BuildManager.value_frost / 2;
        } else if (affix == TowerScript.Affix.Rapid) {
            value = BuildManager.value_rapid / 2;
        }
        return value;
    }

    public bool getPaused() {
        return paused;
    }

    public void setPaused(bool state) {
        paused = state;
    }

    public void exitGame() {
        Application.Quit();
    }
}
