using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/// <summary>
/// Build Node Script is applied to each build tile - used for mouseover and mouse down during build state to place tower.
/// - Responsible for constructing tower on build node and assigning tower properties
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
    // store alternate initial and temporary material for mouseover
    public Material materialInit;
    public Material materialTemp;

    public Vector3 towerPosition;

    public bool towerSelected = false;

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buildManager = GameObject.Find("GameManager").GetComponent<BuildManager>();
        boxRend = GetComponent<Renderer>();
        boxRend.enabled = false;
        materialInit = boxRend.material;
        materialTemp = (Material)Resources.Load("Materials/Tower_Highlight");
        basicTower = (GameObject)Resources.Load("Prefabs/Tower_Prototype_0");
        print("Basic tower assigned to: " + basicTower);
        frostTower = (GameObject)Resources.Load("Prefabs/Tower_Frost");
        print("Frost tower assigned to: " + frostTower);
        rapidTower = (GameObject)Resources.Load("Prefabs/Tower_Rapid");
        print("Rapid tower assigned to: " + rapidTower);
    }

    void OnMouseEnter()
    {
        if(towerSelected || !buildableArea) {
            boxRend.enabled = true;
        }
        if (getBuildState() == true && getBuildSelection() != BuildManager.SELECTION.Invalid) {
            if(buildableArea) {
                boxRend.enabled = true;
                boxRend.material.color = Color.green;
                towerPosition = boxRend.transform.position + new Vector3 (0, 1, 0); // account for box vertical height to place tower
                print(boxRend.material.color);
                // instantiate temp tower - type based on selection
                if (getBuildSelection() == BuildManager.SELECTION.Basic) {
                    tempTower = Instantiate(basicTower, towerPosition, boxRend.transform.rotation);
                } else if (getBuildSelection() == BuildManager.SELECTION.Frost) {
                    tempTower = Instantiate(frostTower, towerPosition, boxRend.transform.rotation);
                } else if (getBuildSelection() == BuildManager.SELECTION.Rapid) {
                    tempTower = Instantiate(rapidTower, towerPosition, boxRend.transform.rotation);
                }
            }
            else {
                boxRend.material.color = Color.red;
            }
        }
    }

    public void instantiateTower(GameObject towerType, float fireRate, int cost) {
        builtTower = Instantiate(towerType, towerPosition, boxRend.transform.rotation);
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
        buildableArea = false;
        print("buildable area set to false!");
    }

    void OnMouseDown() {
        if (getBuildState() == true && buildableArea) {
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
        } else if(!buildableArea) {
            selectTower();
        }
    }

    void OnMouseExit() {
        Destroy(tempTower);
        if(!towerSelected) {
            boxRend.enabled = false;
            boxRend.material.color = Color.white;
        }
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

    public void deselectTower() {
        towerSelected = false;
        boxRend.material.color = Color.white;
        boxRend.enabled = false;
        gameManager.deselectTower();
    }

    public void selectTower() {
        // 'Select' the node and tower and highlight it
        gameManager.setNodeTowerSelected(gameObject, builtTower);
        boxRend.material = (Material)Resources.Load("Materials/Tower_Highlight");
        towerSelected = true;
    }

    public void setTowerSelected(bool state) {
        towerSelected = state;
    }
}
