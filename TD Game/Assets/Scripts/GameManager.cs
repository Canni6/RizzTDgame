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
    //private GUIStyle towerPlannedStyle = new GUIStyle();
    public UserInterface ui;
    public SpawnerScript spawner;
    public GameObject towerGOSelected;
    //public GameObject towerGOPlanned;
    public GameObject nodeGOSelected;
    public bool towerSelectState = false;
    //public bool towerPlannedState = false;
    public string towerSelectedString;
    //public string towerPlannedString;
    public GameObject sellMenuButton;
    public Material materialNodeDefault;
    private float scale;
    public GameObject mainCamera;
    public Vector3 camPos;
    //public AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        // instantiate players and add Player scripts to them
        player1GO = GameObject.Find("Player1");
        ui = GetComponent<UserInterface>();
        spawner = GetComponent<SpawnerScript>();
        player1GO.GetComponent<Player>().addCredit(5);
        player1GO.GetComponent<Player>().addLife(5);
        playerCreditString = "Credit: " + player1GO.GetComponent<Player>().getCredit();
        playerLifeString = "Life: " + player1GO.GetComponent<Player>().getLife().ToString();
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 30;
        mainDisplayStyle.normal.textColor = Color.green;
        mainDisplayStyle.fontSize = 20;
        towerSelectStyle.normal.textColor = Color.yellow;
        towerSelectStyle.fontSize = 20;
        //towerPlannedStyle.normal.textColor = Color.cyan;
        //towerPlannedStyle.fontSize = 20;
        gameOverString = "GAME OVER!";
        gameWonString = "YOU WIN!";
        gameOver = false;
        gameWon = false;
        materialNodeDefault = (Material)Resources.Load("Materials/Basic");
        sellMenuButton = GameObject.Find("SellMenuButton");
        //scale = 10f;
        //mainCamera = GameObject.Find("Main Camera");
        //Camera.main.clearFlags = CameraClearFlags.SolidColor;
        //camPos = mainCamera.transform.position;
        //audioSource.clip = (AudioClip)Resources.Load("Sounds/recording_68_trim");
        //audioSource.Play();
    }

    // Update is called once per frame
    //void Update() {
    //    camPos.y += Input.mouseScrollDelta.y * scale;
    //    Camera.main.transform.position.Set(camPos.x, camPos.y, camPos.z);
    //}

    // TEST CODE BEGIN
    void Awake() {
        //GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //sphere = go.transform;

        //// create a yellow quad
        //go = GameObject.CreatePrimitive(PrimitiveType.Quad);
        //go.transform.Rotate(new Vector3(90.0f, 0.0f, 0.0f));
        //go.transform.localScale = new Vector3(4.0f, 4.0f, 4.0f);
        //go.GetComponent<Renderer>().material.color = new Color(0.75f, 0.75f, 0.0f, 0.5f);

        //// change the camera color and position
        //Camera.main.clearFlags = CameraClearFlags.SolidColor;
        //Camera.main.transform.position = new Vector3(2, 1, 5);
        //Camera.main.transform.Rotate(0, -160, 0);

        
    }

    // TEST CODE END

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
        //// display planned tower info
        //if(towerPlannedState && towerGOPlanned != null) {
        //    towerPlannedString = "Planned tower: " +
        //        towerGOPlanned.GetComponentInChildren<TowerScript>().getAffix()
        //        + "\n" + "Range: " + towerGOPlanned.GetComponentInChildren<TowerScript>().getRange()
        //        + "\n" + "Fire-rate: " + towerGOPlanned.GetComponentInChildren<TowerScript>().getFireRate();
        //    GUI.Label(new Rect(Screen.width / 24, Screen.height / 24 + 100, 1000, 200),
        //                towerPlannedString, towerPlannedStyle);
        //}
        
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
        
        

        //// TEST CODE BEGIN
        //Vector3 pos = sphere.position;
        //pos.y += Input.mouseScrollDelta.y * scale;
        //sphere.position = pos;
        //// TEST CODE END
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

    //public void updateTowerSelectedString() {
    //    towerSelectedString = "Tower type selected: " + towerGOSelected.GetComponentInChildren<TowerScript>().getAffix();
    //}

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

    //public void setTowerPlanned(GameObject tower) {
    //    towerGOPlanned = tower;
    //}

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

    //public void setTowerPlannedState(bool state) {
    //    towerPlannedState = state;
    //}

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
}
