using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        boxRend = GetComponent<Renderer>();
        tempTower = (GameObject)Resources.Load("Prefabs/Tower");
}

    void OnMouseEnter()
    {
        if (buildState == true) {
            if(buildableArea) {
                boxRend.material.color = Color.green;
                print(boxRend.material.color);
                // instantiate temp tower
                tempTowerClone = Instantiate(tempTower, boxRend.transform.position, boxRend.transform.rotation) as GameObject;
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
            Destroy(tempTowerClone); // destroy the temp
            // instantiate new tower
            newTower = Instantiate(tempTower, boxRend.transform.position, boxRend.transform.rotation) as GameObject;
            // access tower script and toggle built state
            newTower.GetComponentInChildren<TowerScript>().setBuilt();
            buildableArea = false;
            //print("Setting tower script to active.");
            //newTower.GetComponentInChildren<TowerScript>().toggleActive();
            //print(newTower.GetComponentInChildren<TowerScript>().getActive());
            //newTower.gameObject.TowerScript.start();
            //newTower.gameObject.addComponent(new ProjectileScript());
            print("Construction complete.");
        }
    }

    void OnMouseExit()
    {
        boxRend.material.color = Color.white;
        Destroy(tempTowerClone);
    }

    void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 500, 100), buildStateString);
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

        //boxRend = hit.transform.gameObject.GetComponent<Renderer>();
        changeBuildState();
        // OnGUI();

        if (Input.GetKeyDown(KeyCode.B))
        {
            buildState = true;
            print("State changed to build");
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            buildState = false;
            print("State changed to play");
        }

        //if (buildState == true)
        //{

        //}




        // ray = Camera.main.ScreenPointToRay(Input.mousePosition);



        //if (Physics.Raycast(ray, out hit))
        //{
        //    print(hit.collider.name);
        //    Renderer boxRend = hit.transform.gameObject.GetComponent<Renderer>();
        //    boxRend.material.SetColor("_Color", Color.red);
        //}



        if (Input.GetKeyDown(KeyCode.B))
		{
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
}
