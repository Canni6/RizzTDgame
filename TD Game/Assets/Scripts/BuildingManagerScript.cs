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
    public GameObject tempTower = null; // prefab declaration
    public GameObject tempTowerClone; // placement phase
    public GameObject newTower; // built phase
    public GameManager gameManagerRef;
    bool creditWarning;
    public float timer;
    string creditWarningString;
    private GUIStyle guiStyle = new GUIStyle();

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        boxRend = GetComponent<Renderer>();
        tempTower = (GameObject)Resources.Load("Prefabs/Tower");
        gameManagerRef = GameObject.Find("GameManager").GetComponent<GameManager>();
        creditWarning = false;
        timer = 0;
        creditWarningString = "We need more gold!";
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 20;
    }

    void OnMouseEnter()
    {
        if (buildState == true) {
            if(buildableArea && gameManagerRef.getPlayerCredit() > 0) {
                boxRend.material.color = Color.green;
                print(boxRend.material.color);
                // instantiate temp tower
                tempTowerClone = Instantiate(tempTower, boxRend.transform.position, boxRend.transform.rotation);
            }
            else {
                boxRend.material.color = Color.red;
            }
        }
    }

    void OnMouseDown() {
        if (buildState == true && buildableArea) {

            if (gameManagerRef.getPlayerCredit() > 0) {
                boxRend.material.color = Color.yellow; // set to green temporarily to 
                print(boxRend.material.color);
                Destroy(tempTowerClone); // destroy the temp
                                         // instantiate new tower
                newTower = Instantiate(tempTower, boxRend.transform.position, boxRend.transform.rotation);
                // access tower script and toggle built state
                newTower.GetComponentInChildren<TowerScript>().setBuilt();
                // spend 1 credit
                updatePlayerCredit(-1);


                buildableArea = false;
                print("Construction complete.");
            } else {
                print("We need more gold!");
                creditWarning = true;
            }
            
        }
    }

    void OnMouseExit()
    {
        boxRend.material.color = Color.white;
        Destroy(tempTowerClone);
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

        }

        if (buildState == false)
        {
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

        if (Input.GetKeyDown(KeyCode.B))
        {
            buildState = true;
            print("State changed to build");
            print("Player 1 credit report from building manager w/ gameManager ref: " + gameManagerRef.getPlayerCredit());
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            buildState = false;
            print("State changed to play");
        }

        if (Input.GetKeyDown(KeyCode.B)) {
		Debug.Log("pressed B");
//		// TO DO:
//		// Open Build Menu -> implement some UI stuff/icon/description/cost
//			if(Input.GetKeyDown(KeyCode.T)
//			{
//			//Build basic tower
//				//Instantiate(tower) on mouse cursor and follow mouse cursor around
//				// - whilst following mouse cursor, highlight closest buildable block within radius x of mouse cursor
//				// - on left click down: PlaceBuilding();
//				// void PlaceBuilding();
//				// {
//				// Instantiate(Tower) at centre of currently highlighted block
//				// DeductCurrencyFromPlayer;
//				// ReadyToFireTurret;
//				// 
//
//			}
		}
	}

    void DetectMouseOver()
    {
        
    }

    void updatePlayerCredit(int credit) {
        gameManagerRef.addPlayerCredit(credit);
    }
}
