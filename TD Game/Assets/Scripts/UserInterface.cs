using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UserInterface : MonoBehaviour
{
    public Button restartButton;
    public Button buildMenuButton;
    public GameObject buildMenu;
    public GameObject towerMenu;
    public Button basicTowerButton;
    public Button frostTowerButton;
    public Button rapidTowerButton;

    // Method to get texture from Prefab preview to use as icon for GUI
    public static Texture2D GetAssetPreview(Object asset) {
        return AssetPreview.GetAssetPreview(asset);
    }

    // Method to encode texture as png image for icon.
    public static byte[] EncodeToPNG(Texture2D tex) {
        return EncodeToPNG(tex);
    }

    // Method to write bytes to file
    public static void WriteAllBytes(string path, byte[] bytes) { }

    

    // Start is called before the first frame update
    void Start()
    {
        buildMenu = GameObject.Find("BuildMenu");
        buildMenu.SetActive(true);
        // switch on children buttons
        for(int i = 0; i < buildMenu.transform.childCount; ++i) {
            buildMenu.transform.GetChild(i).gameObject.SetActive(true);
            // switch on next level hierarchy children buttons - applies to current Tower Menu hierarchy
            if(buildMenu.transform.GetChild(i).transform.childCount > 0) {
                for(int j = 0; j < buildMenu.transform.GetChild(i).transform.childCount; ++j) {
                    buildMenu.transform.GetChild(i).transform.GetChild(j).gameObject.SetActive(true);
                }
            }
        }
        towerMenu = GameObject.Find("TowerMenu");
        towerMenu.SetActive(false);
        restartButton.onClick.AddListener(restartGame);

        // /start ** TESTING THIS CODE ** - may break other UI or game functionality

        // Write basic tower prefab preview to file for use as icon
        Object towerBasic_Prefab = Resources.Load("Assets/AResources/Prefabs/Tower_Prototype_0.prefab");
        Texture2D towerBasic_Texture = GetAssetPreview(towerBasic_Prefab);
        byte[] towerBasic_File = EncodeToPNG(towerBasic_Texture);
        // For testing purposes, also write to a file in the project folder
        WriteAllBytes(Application.dataPath + "/../towerBasic_Icon.png", towerBasic_File);

        // /end ** TESTING THIS CODE ** - may break other UI or game functionality
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void restartGame() {
        print("Game restarted");
        print("Active scene is: " + SceneManager.GetActiveScene().ToString());
        SceneManager.LoadScene(0);
    }

    public void displayButton(Button button) {
        button.gameObject.SetActive(true);
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
        print("Button selected: " + button.name);
        if(button.name.Equals("BuildMenuButton")) {
            displayTowerMenu();
        }
    }
    public void OnSelect() {
        //this.gameObject.GetComponent<Image>().color = Color.yellow;
    }

}
