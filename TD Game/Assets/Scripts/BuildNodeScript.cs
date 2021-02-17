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
    public GameObject tempTower; // placement phase
    public GameObject basicTower;
    public GameObject frostTower;
    public GameObject rapidTower;
    public GameObject builtTower; // built phase
    public GameObject rangeIndicator;
    public GameObject tempIndicator;
    public GameObject builtIndicator;
    public GameManager gameManager;
    public SoundManager soundManager;
    public BuildManager buildManager;
    // store alternate initial and temporary material for mouseover
    public Material materialInit;
    public Material materialTemp;

    public Vector3 towerPosition;
    public bool towerSelected = false;
    public GUIStyle proposedTowerStyle = new GUIStyle();


    void OnGUI() {
        
    }

    // Use this for initialization
    void Start () {
        //boxRend = GetComponentInChildren<Renderer>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        buildManager = GameObject.Find("GameManager").GetComponent<BuildManager>();
        soundManager = gameManager.GetComponent<SoundManager>();
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
        rangeIndicator = (GameObject)Resources.Load("Prefabs/ring_unit");
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
                planTower();                
            }
            else {
                boxRend.material.color = Color.red;
            }
        }
    }

    public void placeTower(GameObject towerType, float fireRate, int cost) {
        builtTower = Instantiate(towerType, towerPosition, boxRend.transform.rotation);
        if(towerType == basicTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Basic);
        } else if(towerType == frostTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Frost);
        } else if (towerType == rapidTower) {
            builtTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Rapid);
        }
        builtTower.GetComponentInChildren<TowerScript>().setBuilt();
        addBuiltIndicator(builtTower);
        // hide indicator when built - only display during tower selection
        builtIndicator.SetActive(false);
        updatePlayerCredit(-cost);
        cancelBuildState();
        buildableArea = false;
        print("buildable area set to false!");
        soundManager.playSound(soundManager.audioBuild);
    }

    void OnMouseDown() {
        if (getBuildState() == true && buildableArea) {
            removeTempTower();
            // instantiate new tower
            if (getBuildSelection() == BuildManager.SELECTION.Basic) {
                placeTower(basicTower, BuildManager.rate_basic, BuildManager.value_basic);

            }
            else if (getBuildSelection() == BuildManager.SELECTION.Frost) {
                placeTower(frostTower, BuildManager.rate_frost, BuildManager.value_frost);

            }
            else if (getBuildSelection() == BuildManager.SELECTION.Rapid) {
                placeTower(rapidTower, BuildManager.rate_rapid, BuildManager.value_rapid);
            }
            else if (getBuildSelection() == BuildManager.SELECTION.Invalid) {
                print("Clicked on tiles without build type selected");
            }
            // else valid selection but insufficient credit
            else {
                print("We need more gold!");
                buildManager.setCreditWarning(true);
            }
        } else if(!buildableArea) {
            //// don't select tower during build phase
            //if(getBuildState() == false) {
                selectTower();
            //}
        }
    }

    void OnMouseExit() {
        removeTempTower();
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
        builtIndicator.SetActive(false);
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
        builtIndicator.SetActive(true);
    }

    public void setTowerSelected(bool state) {
        towerSelected = state;
    }

    public void removeTempTower() {
        Destroy(tempTower);
        Destroy(tempIndicator);
    }

    public void hideIndicator() {
        builtIndicator.SetActive(false);
    }

    public void addBuiltIndicator(GameObject tower) {
        builtIndicator = Instantiate(rangeIndicator, towerPosition, boxRend.transform.rotation);
        // resize range indicator on x:z axis (unit circle scale)
        builtIndicator.transform.localScale += 
            new Vector3(tower.GetComponentInChildren<TowerScript>().getRange(),
            0.0f, tower.GetComponentInChildren<TowerScript>().getRange());
    }

    public void addTempIndicator(GameObject tower) {
        tempIndicator = Instantiate(rangeIndicator, towerPosition, boxRend.transform.rotation);
        // resize range indicator on x:z axis (unit circle scale)
        tempIndicator.transform.localScale += 
            new Vector3(tower.GetComponentInChildren<TowerScript>().getRange(),
            0.0f, tower.GetComponentInChildren<TowerScript>().getRange());
    }

    public void planTower() {
        // instantiate temp tower - type based on selection
        if (getBuildSelection() == BuildManager.SELECTION.Basic) {
            tempTower = Instantiate(basicTower, towerPosition, boxRend.transform.rotation);
            tempTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Basic);
            addTempIndicator(tempTower);
        }
        else if (getBuildSelection() == BuildManager.SELECTION.Frost) {
            tempTower = Instantiate(frostTower, towerPosition, boxRend.transform.rotation);
            tempTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Frost);
            addTempIndicator(tempTower);
        }
        else if (getBuildSelection() == BuildManager.SELECTION.Rapid) {
            tempTower = Instantiate(rapidTower, towerPosition, boxRend.transform.rotation);
            tempTower.GetComponentInChildren<TowerScript>().setAffix(TowerScript.Affix.Rapid);
            addTempIndicator(tempTower);
        }
        //if (getBuildSelection() != BuildManager.SELECTION.Invalid) {
        //    gameManager.setTowerPlanned(tempTower);
        //}
    }
}
