using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleScript : MonoBehaviour {
    public Button playButton;
    public Button exitButton;
    public AudioSource lobbyMusic;
    public AudioSource buttonBlip;


    // Start is called before the first frame update
    void Start() {
        playButton.onClick.AddListener(startGame);
        exitButton.onClick.AddListener(exitGame);
        lobbyMusic.clip = (AudioClip)Resources.Load("Sounds/recording_68_trim");
        lobbyMusic.Play();
    }

    // Update is called once per frame
    void Update() {

    }

    public void startGame() {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(0);
    }

    public void exitGame() {
        // Application.Quit() does not work in the editor so
        // UnityEditor.EditorApplication.isPlaying needs to be set to false to end the game
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
