using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Button restartButton;
    public Button buildMenuButton;
    public GameObject buildMenu;
    public GameObject towerMenu;
    public Button basicTowerButton;
    public Button frostTowerButton;
    public Button rapidTowerButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        buildMenu = GameObject.Find("BuildMenu");
        buildMenu.SetActive(true);
        towerMenu = GameObject.Find("TowerMenu");
        towerMenu.SetActive(false);
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
    }

    public void selectButton(Button button) {
        button.Select();
        print("Button selected: " + button.name);
        if(button.name.Equals("BuildMenuButton")) {
            displayTowerMenu();
        }
    }
    public void OnSelect() {
        this.gameObject.GetComponent<Image>().color = Color.white;
    }

}
