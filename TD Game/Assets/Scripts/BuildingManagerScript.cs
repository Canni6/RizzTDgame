using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManagerScript : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public Renderer boxRend;
    string build = "--Build State--";
    string play = "--Play State--";
    string buildStateString;
    public bool buildState = false;
    public bool buildableArea = true;
    public GameObject tempTower = null; // placement phase
    public GameObject basicTower;
    public GameObject frostTower;
    public GameObject rapidTower;
    public GameObject builtTower = null; // built phase
    public GameManager gameManagerRef;
    bool creditWarning;
    public float timer;
    string creditWarningString;
    private GUIStyle guiStyle = new GUIStyle();
    public enum SELECTION {
        Invalid,
        Basic,
        Frost,
        Rapid
    }

    public SELECTION selection;

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
        boxRend = GetComponent<Renderer>();
        basicTower = (GameObject)Resources.Load("Prefabs/Tower_Basic");
        print("Basic tower assigned to: " + basicTower);
        frostTower = (GameObject)Resources.Load("Prefabs/Tower_Frost");
        print("Frost tower assigned to: " + frostTower);
        rapidTower = (GameObject)Resources.Load("Prefabs/Tower_Rapid");
        print("Rapid tower assigned to: " + rapidTower);
        creditWarning = false;
        timer = 0;
        creditWarningString = "We need more gold!";
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 20;
        selection = SELECTION.Invalid;
    }

    void OnMouseEnter()
    {
        if (buildState == true && selection != SELECTION.Invalid) {
            if(buildableArea) {
                boxRend.material.color = Color.green;
                print(boxRend.material.color);
                // instantiate temp tower - type based on selection
                if (selection == SELECTION.Basic) {
                    tempTower = Instantiate(basicTower, boxRend.transform.position, boxRend.transform.rotation);
                } else if (selection == SELECTION.Frost) {
                    tempTower = Instantiate(frostTower, boxRend.transform.position, boxRend.transform.rotation);
                } else if (selection == SELECTION.Rapid) {
                    tempTower = Instantiate(rapidTower, boxRend.transform.position, boxRend.transform.rotation);
                }
            }
            else {
                boxRend.material.color = Color.red;
            }
        }
    }

    void OnMouseDown() {
        if (buildState == true && buildableArea) {

            
            boxRend.material.color = Color.yellow; // set to green temporarily to 
            print(boxRend.material.color);
            Destroy(tempTower); // destroy the temp
            // instantiate new tower
            if(selection == SELECTION.Basic) {
                builtTower = Instantiate(basicTower, boxRend.transform.position, boxRend.transform.rotation);
                // access tower script and toggle built state
                builtTower.GetComponentInChildren<TowerScript>().setBuilt();
                builtTower.GetComponentInChildren<TowerScript>().setFireRate(0.5f);
                // spend 1 credit
                updatePlayerCredit(-1);
                buildState = false;
            } else if (selection == SELECTION.Frost) {
                builtTower = Instantiate(frostTower, boxRend.transform.position, boxRend.transform.rotation);
                // access tower script and toggle built state
                builtTower.GetComponentInChildren<TowerScript>().setBuilt();
                builtTower.GetComponentInChildren<TowerScript>().setFireRate(1f);
                // spend 2 credits
                updatePlayerCredit(-2);
                buildState = false;
            } else if (selection == SELECTION.Rapid) {
                builtTower = Instantiate(rapidTower, boxRend.transform.position, boxRend.transform.rotation);
                // access tower script and toggle built state
                builtTower.GetComponentInChildren<TowerScript>().setBuilt();
                builtTower.GetComponentInChildren<TowerScript>().setFireRate(2f);
                // spend 3 credits
                updatePlayerCredit(-3);
                buildState = false;
            }

            else {
                print("We need more gold!");
                creditWarning = true;
            }
            
        }
    }

    void OnMouseExit() {
        boxRend.material.color = Color.white;
        Destroy(tempTower);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 1000, 200), buildStateString);
        if(creditWarning) {
            GUI.Label(new Rect(Screen.width / 3, Screen.height / 2 + 2*(Screen.height / 5), 500, 100), creditWarningString, guiStyle);
        }
    }

    void changeBuildState()
    {
        if (buildState == true)
        {
            buildStateString = build;
        } else {
            buildStateString = play;
        }
    }


    // Update is called once per frame
    void Update () {
        changeBuildState();

        if(creditWarning) {
            timer += Time.deltaTime;
            if (timer > 3) {
                creditWarning = false;
                timer = 0;
            }
        }

        // B - build state, basic tower selection
        if (Input.GetKeyDown(KeyCode.B)) {
            // when already in build state
            if(buildState == true) {
                // basic tower selected
                if(gameManagerRef.getPlayerCredit() >= 1) {
                    selection = SELECTION.Basic;
                    print("Basic tower selected");
                }
            } 
            // entering build state
            else {
                buildState = true;
                print("State changed to build");
                print("Player 1 credit report from building manager w/ gameManager ref: " + gameManagerRef.getPlayerCredit());
            }
            
        }

        // F - frost tower selection
        if (Input.GetKeyDown(KeyCode.F)) {
            if (buildState == true) {
                // frost tower selected
                if (gameManagerRef.getPlayerCredit() >= 2) {
                    selection = SELECTION.Frost;
                    print("Frost tower selected");
                }
            }
        }

        // R - rapid tower selection
        if (Input.GetKeyDown(KeyCode.R)) {
            if (buildState == true) {
                // rapid tower selected
                if (gameManagerRef.getPlayerCredit() >= 3) {
                    selection = SELECTION.Rapid;
                    print("Frost tower selected");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            buildState = false;
            print("State changed to play");
        }
	}

    void updatePlayerCredit(int credit) {
        gameManagerRef.addPlayerCredit(credit);
    }
}
