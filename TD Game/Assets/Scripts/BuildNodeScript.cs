using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Build Node Script is applied to each build tile - used for mouseover and mouse down during build state to place tower.
/// </summary>
public class BuildNodeScript : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public Renderer boxRend;
    public bool buildableArea = true;
    public GameObject tempTower = null; // placement phase
    public GameObject basicTower;
    public GameObject frostTower;
    public GameObject rapidTower;
    public GameObject builtTower = null; // built phase
    public GameManager gameManager;
    public BuildManager buildManager;

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buildManager = GameObject.Find("GameManager").GetComponent<BuildManager>();
        boxRend = GetComponent<Renderer>();
        basicTower = (GameObject)Resources.Load("Prefabs/Tower_Basic");
        print("Basic tower assigned to: " + basicTower);
        frostTower = (GameObject)Resources.Load("Prefabs/Tower_Frost");
        print("Frost tower assigned to: " + frostTower);
        rapidTower = (GameObject)Resources.Load("Prefabs/Tower_Rapid");
        print("Rapid tower assigned to: " + rapidTower);
    }

    void OnMouseEnter()
    {
        if (getBuildState() == true && getBuildSelection() != BuildManager.SELECTION.Invalid) {
            if(buildableArea) {
                boxRend.material.color = Color.green;
                print(boxRend.material.color);
                // instantiate temp tower - type based on selection
                if (getBuildSelection() == BuildManager.SELECTION.Basic) {
                    tempTower = Instantiate(basicTower, boxRend.transform.position, boxRend.transform.rotation);
                } else if (getBuildSelection() == BuildManager.SELECTION.Frost) {
                    tempTower = Instantiate(frostTower, boxRend.transform.position, boxRend.transform.rotation);
                } else if (getBuildSelection() == BuildManager.SELECTION.Rapid) {
                    tempTower = Instantiate(rapidTower, boxRend.transform.position, boxRend.transform.rotation);
                }
            }
            else {
                boxRend.material.color = Color.red;
            }
        }
    }

    public void instantiateTower(GameObject towerType, float fireRate, int cost) {
        builtTower = Instantiate(towerType, boxRend.transform.position, boxRend.transform.rotation);
        if(towerType == basicTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Basic);
        } else if(towerType == frostTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Frost);
        } else if (towerType == rapidTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Rapid);
        }
        builtTower.GetComponentInChildren<TowerScript>().setBuilt();
        builtTower.GetComponentInChildren<TowerScript>().setFireRate(fireRate);
        updatePlayerCredit(-cost);
        cancelBuildState();
    }

    void OnMouseDown() {
        if (getBuildState() == true && buildableArea) {
            boxRend.material.color = Color.yellow; // set to green temporarily to 
            print(boxRend.material.color);
            Destroy(tempTower); // destroy the temp

            // instantiate new tower
            if(getBuildSelection() == BuildManager.SELECTION.Basic) {
                instantiateTower(basicTower, 0.5f, 1);

            } else if (getBuildSelection() == BuildManager.SELECTION.Frost) {
                instantiateTower(frostTower, 1.0f, 2);

            } else if (getBuildSelection() == BuildManager.SELECTION.Rapid) {
                instantiateTower(rapidTower, 2.0f, 3);
            }

            else {
                print("We need more gold!");
                buildManager.setCreditWarning(true);
            }
            
        }
    }

    void OnMouseExit() {
        boxRend.material.color = Color.white;
        Destroy(tempTower);
    }

    public void cancelBuildState() {
        buildManager.cancelBuildState();
    }

    public bool getBuildState() {
        return buildManager.getBuildState();
    }

    public BuildManager.SELECTION getBuildSelection() {
        return buildManager.getSelection();
    }

    void updatePlayerCredit(int credit) {
        gameManager.addPlayerCredit(credit);
    }
}
