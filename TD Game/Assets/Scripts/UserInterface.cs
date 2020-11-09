using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour
{
    public Button restartButton;

    public GameObject buildMenu;
    public Button basicTowerButton;
    public Button frostTowerButton;
    public Button rapidTowerButton;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.onClick.AddListener(restartGame);
        buildMenu = GameObject.Find("BuildMenu");
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

    public void displayBuildMenu() {
        buildMenu.gameObject.SetActive(true);
    }

    public void hideBuildMenu() {
        buildMenu.gameObject.SetActive(false);
    }
}
