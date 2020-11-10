﻿using System.Collections;
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
    string buildStateString;
    public SELECTION buildSelection;
    public bool buildState;
    public GameManager gameManager;
    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
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
                // basic tower selected
                if (gameManager.getPlayerCredit() >= 1) {
                    setSelection(BuildManager.SELECTION.Basic);
                    print("Basic tower selected");
                }
            }
            // entering build state
            else {
                setBuildState(true);
                print("State changed to build");
                print("Player 1 credit report from building manager w/ gameManager ref: " + gameManager.getPlayerCredit());
            }

        }

        // F - frost tower selection
        if (Input.GetKeyDown(KeyCode.F)) {
            if (buildState == true) {
                // frost tower selected
                if (gameManager.getPlayerCredit() >= 2) {
                    setSelection(BuildManager.SELECTION.Frost);
                    print("Frost tower selected");
                }
            }
        }

        // R - rapid tower selection
        if (Input.GetKeyDown(KeyCode.R)) {
            if (buildState == true) {
                // rapid tower selected
                if (gameManager.getPlayerCredit() >= 3) {
                    setSelection(BuildManager.SELECTION.Rapid);
                    print("Frost tower selected");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            cancelBuildState();
            print("State changed to play");
        }
    }

    void OnGUI() {
        GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 1000, 200), buildStateString);
        if (creditWarning) {
            GUI.Label(new Rect(Screen.width / 3, Screen.height / 2 + 2 * (Screen.height / 5), 500, 100), creditWarningString, guiStyle);
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
}