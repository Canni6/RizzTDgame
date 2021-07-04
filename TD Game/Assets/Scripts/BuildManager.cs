using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public enum SELECTION {
        Invalid,
        Basic,
        Frost,
        Rapid
    }

    bool creditWarning;
    public float timer;
    string creditWarningString;
    private GUIStyle guiStyle = new GUIStyle();
    string build = "--Build State--";
    string play = "--Play State--";
    // string sell = "--Sell State--";
    string buildStateString;
    public SELECTION buildSelection;
    public bool buildState;
    public bool sellState;
    public GameManager gameManager;
    public SoundManager soundManager;
    public PauseMenu pauseMenu;
    public UserInterface ui;

    // tower construction value
    public const int value_basic = 2;
    public const int value_frost = 4;
    public const int value_rapid = 8;

    public const float range_basic = 10f;
    public const float range_frost = 12.5f;
    public const float range_rapid = 15f;

    public const float rate_basic = 1f;
    public const float rate_frost = 0.5f;
    public const float rate_rapid = 2f;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.Find("GameManager").GetComponent<SoundManager>();
        pauseMenu = GameObject.Find("GameManager").GetComponent<PauseMenu>();
        ui = GameObject.Find("GameManager").GetComponent<UserInterface>();
        // build variable state
        buildSelection = SELECTION.Invalid;
        buildState = false;
        // UI state
        creditWarning = false;
        timer = 0;
        creditWarningString = "We need more gold!";
        guiStyle.normal.textColor = Color.red;
        guiStyle.fontSize = 20;
    }

    // Update is called once per frame
    void Update()
    {
        updateBuildString();

        if (creditWarning) {
            timer += Time.deltaTime;
            if (timer > 3) {
                creditWarning = false;
                timer = 0;
            }
        }

        // B - build state, basic tower selection
        if (Input.GetKeyDown(KeyCode.B)) {
            // when already in build state
            if (buildState == true) {
                // attempt to build basic tower
                setSelection(SELECTION.Basic);
                planTower(SELECTION.Basic);
            }
            // entering build state
            else {
                enterBuild();
                soundManager.playSound(soundManager.audioButtonBlip);
            }

        }
        // F - frost tower selection
        if (Input.GetKeyDown(KeyCode.F)) {
            if (buildState == true) {
                setSelection(SELECTION.Frost);
                planTower(SELECTION.Frost);
            }
        }
        // R - rapid tower selection
        if (Input.GetKeyDown(KeyCode.R)) {
            if (buildState == true) {
                setSelection(SELECTION.Rapid);
                planTower(SELECTION.Rapid);
            }
        }
        // S - sell tower
        if (Input.GetKeyDown(KeyCode.S)) {
            sellTower();
        }

        // Esc - cancel existing menus/actions
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if(!buildState) {
                pauseMenu.pauseGame();
            } else {
                cancelBuildState();
            }
            //print("State changed to play");
        }
    }

    void OnGUI() {
        // GUI.Label(new Rect(Screen.width / 2 - Screen.width / 12, Screen.height - Screen.height / 12, 1000, 200), buildStateString);
        if (creditWarning) {
            GUI.Label(new Rect(Screen.width * 0.55f, Screen.height / 2 + 2 * (Screen.height / 4), 500, 100), creditWarningString, guiStyle);
        }
    }

    public SELECTION getSelection() {
        return buildSelection;
    }

    public void setSelection(SELECTION buildSelection) {
        this.buildSelection = buildSelection;
    }

    public bool getBuildState() {
        return buildState;
    }

    public void setBuildState(bool state) {
        buildState = state;
    }

    public void cancelBuildState() {
        setBuildState(false);
        setSelection(SELECTION.Invalid);
        ui.hideTowerMenu();
        // deselect the tower when in play state and cancel is called
        //if(getBuildState() == false) {
        //    gameManager.deselectTower();
        //}
        gameManager.deselectTower();
        ui.hideButton(ui.cancelMenuButton);
        //gameManager.setTowerPlannedState(false);
    }

    public void updateBuildString() {
        if (buildState == true) {
            buildStateString = build;
        }
        else {
            buildStateString = play;
        }
    }

    public void setCreditWarning(bool state) {
        creditWarning = state;
    }

    public void planTower(SELECTION buildSelection) {
        if(buildSelection == SELECTION.Basic && gameManager.getPlayerCredit() >= value_basic) {
            ui.selectButton(ui.basicTowerButton);
            //print("basic tower selected");
            soundManager.playSound(soundManager.audioButtonBlip);
            //gameManager.setTowerPlannedState(true);
        } else if(buildSelection == SELECTION.Frost && gameManager.getPlayerCredit() >= value_frost) {
            ui.selectButton(ui.frostTowerButton);
            //print("frost tower selected");
            soundManager.playSound(soundManager.audioButtonBlip);
            //gameManager.setTowerPlannedState(true);
        } else if(buildSelection == SELECTION.Rapid && gameManager.getPlayerCredit() >= value_rapid) {
            ui.selectButton(ui.rapidTowerButton);
            //print("rapid tower selected");
            soundManager.playSound(soundManager.audioButtonBlip);
            //gameManager.setTowerPlannedState(true);

        } else {
            setSelection(SELECTION.Invalid);
            setCreditWarning(true);
            //print("we need more gold");
            soundManager.playSound(soundManager.audioDeclined);
            ui.resetButtons();
            //gameManager.setTowerPlannedState(false);
        }
    }

    public void sellTower() {
        // check if a build node is selected for selling
        if (gameManager.getTowerSelectState()) {
            gameManager.sellTowerSelected();
            //print("Tower sold");
            soundManager.playSound(soundManager.audioDeathMech);
        }
        cancelBuildState();
    }

    public void enterBuild() {
        setBuildState(true);
        ui.selectButton(ui.buildMenuButton);
        ui.displayTowerMenu();
        ui.displayButton(ui.cancelMenuButton);
        gameManager.deselectTower();
        //print("State changed to build");
    }

}
