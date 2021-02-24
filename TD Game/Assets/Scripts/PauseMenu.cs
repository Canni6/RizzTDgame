using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public Button resumeButton;
    public Button optionsButton;
    public Button exitButton;
    public GameManager gameManager;

    
    // Start is called before the first frame update
    void Start()
    {
        pauseMenu = GameObject.Find("PauseMenu");
        pauseMenu.SetActive(false);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        resumeButton.onClick.AddListener(pauseGame);
        exitButton.onClick.AddListener(gameManager.exitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseGame() {
        // pause
        if (!gameManager.getPaused()) {
            Time.timeScale = 0;
            gameManager.setPaused(true);
            pauseMenu.SetActive(true);
        }
        // un-pause
        else {
            Time.timeScale = 1;
            gameManager.setPaused(false);
            pauseMenu.SetActive(false);
        }
    }
}
