using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour
{
    //public GameObject sellMenuButton;
    public GameObject buildMenu;
    public GameObject towerMenu;
    public Button buildMenuButton;
    public Button sellMenuButton;
    public Button cancelMenuButton;
    public Button restartButton;
    public Button basicTowerButton;
    public Button frostTowerButton;
    public Button rapidTowerButton;
    public BuildManager buildManager;

    // Method to get texture from Prefab preview to use as icon for GUI
    //public static Texture2D GetAssetPreview(Object asset) {
    //    return AssetPreview.GetAssetPreview(asset);
    //}

    //// Method to encode texture as png image for icon.
    //public static byte[] EncodeToPNG(Texture2D tex) {
    //    return EncodeToPNG(tex);
    //}

    //// Method to write bytes to file
    //public static void WriteAllBytes(string path, byte[] bytes) { }

    

    // Start is called before the first frame update
    void Start()
    {
        buildManager = GameObject.Find("GameManager").GetComponent<BuildManager>();
        buildMenu = GameObject.Find("BuildMenu");
        buildMenu.SetActive(true);
        buildMenuButton = buildMenu.transform.GetChild(0).GetComponent<Button>();     
        buildMenuButton.onClick.AddListener(displayTowerMenu);
        // switch on children buttons
        for (int i = 0; i < buildMenu.transform.childCount; ++i) {
            buildMenu.transform.GetChild(i).gameObject.SetActive(true);
            // switch on next level hierarchy children buttons - applies to current Tower Menu hierarchy
            if(buildMenu.transform.GetChild(i).transform.childCount > 0) {
                for(int j = 0; j < buildMenu.transform.GetChild(i).transform.childCount; ++j) {
                    buildMenu.transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        towerMenu = GameObject.Find("TowerMenu");
        Button towersButton = towerMenu.transform.GetChild(0).GetComponent<Button>();
        towersButton.transform.GetChild(0).GetComponent<Text>().text = "Towers\t\t\t\t\t\t" +
                                                                        "| Cost\t| Range\t| Fire-Rate";
        Button basicTowerButton = towerMenu.transform.GetChild(1).GetComponent<Button>();
        basicTowerButton.transform.GetChild(0).GetComponent<Text>().text = "Basic Tower (B) \t\t\t| "
                                                    + BuildManager.value_basic + "\t\t| "
                                                    + BuildManager.range_basic + "\t\t\t| "
                                                    + BuildManager.rate_basic;
        Button frostTowerButton = towerMenu.transform.GetChild(2).GetComponent<Button>();
        frostTowerButton.transform.GetChild(0).GetComponent<Text>().text = "Frost Tower (F) \t\t\t| "
                                                    + BuildManager.value_frost + "\t\t| "
                                                    + BuildManager.range_frost + "\t\t\t| "
                                                    + BuildManager.rate_frost;
        Button rapidTowerButton = towerMenu.transform.GetChild(3).GetComponent<Button>();
        rapidTowerButton.transform.GetChild(0).GetComponent<Text>().text = "Rapid Tower (R) \t\t| "
                                                    + BuildManager.value_rapid + "\t\t| "
                                                    + BuildManager.range_rapid + "\t\t\t| "
                                                    + BuildManager.rate_rapid;
        towerMenu.SetActive(false);
        

        
        sellMenuButton = buildMenu.transform.GetChild(2).GetComponent<Button>();
        cancelMenuButton = buildMenu.transform.GetChild(3).GetComponent<Button>();
        // mouse support
        buildMenuButton.onClick.AddListener(buildManager.enterBuild);
        cancelMenuButton.onClick.AddListener(buildManager.cancelBuildState);
        sellMenuButton.onClick.AddListener(buildManager.sellTower);
        towerMenu.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(buildBasic);
        towerMenu.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(buildFrost);
        towerMenu.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(buildRapid);

        restartButton = GameObject.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(restartGame);
        // hide buttons until used
        hideButton(sellMenuButton);
        hideButton(cancelMenuButton);
        hideButton(restartButton);

        // /start ** TESTING THIS CODE ** - may break other UI or game functionality

        //// Write basic tower prefab preview to file for use as icon
        //Object towerBasic_Prefab = Resources.Load("Assets/AResources/Prefabs/Tower_Prototype_0.prefab");
        //Texture2D towerBasic_Texture = GetAssetPreview(towerBasic_Prefab);
        //byte[] towerBasic_File = EncodeToPNG(towerBasic_Texture);
        //// For testing purposes, also write to a file in the project folder
        //WriteAllBytes(Application.dataPath + "/../towerBasic_Icon.png", towerBasic_File);

        // /end ** TESTING THIS CODE ** - may break other UI or game functionality
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void restartGame() {
        //print("Game restarted");
        //print("Active scene is: " + SceneManager.GetActiveScene().ToString());
        SceneManager.LoadScene(1);
    }

    public void displayButton(Button button) {
        button.gameObject.SetActive(true);
    }

    public void hideButton(Button button) {
        button.gameObject.SetActive(false);
    }

    public void displayTowerMenu() {
        towerMenu.gameObject.SetActive(true);
    }

    public void hideTowerMenu() {
        towerMenu.gameObject.SetActive(false);
        buildMenuButton.gameObject.GetComponent<Image>().color = Color.white;
    }

    public void selectButton(Button button) {
        // reset all button colors
        for(int i = 0; i < towerMenu.transform.childCount; ++i) {
            towerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }
        button.gameObject.GetComponent<Image>().color = Color.yellow;
        //print("Button selected: " + button.name);
    }
    public void OnSelect() {
        //this.gameObject.GetComponent<Image>().color = Color.yellow;
    }

    public void buildBasic() {
        buildManager.setSelection(BuildManager.SELECTION.Basic);
        buildManager.planTower(BuildManager.SELECTION.Basic);
    }

    public void buildFrost() {
        buildManager.setSelection(BuildManager.SELECTION.Frost);
        buildManager.planTower(BuildManager.SELECTION.Frost);
    }

    public void buildRapid() {
        buildManager.setSelection(BuildManager.SELECTION.Rapid);
        buildManager.planTower(BuildManager.SELECTION.Rapid);
    }

    public void resetButtons() {
        // reset all button colors
        for (int i = 0; i < towerMenu.transform.childCount; ++i) {
            towerMenu.transform.GetChild(i).gameObject.GetComponent<Image>().color = Color.white;
        }
    }
}
