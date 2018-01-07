using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
    public int blueAmountOnLevel;
    public int turnedBoxes;
    public int touchedBoxes;
    public int amountOnCluster;
}

public class GameManager : MonoBehaviour {

    public ScreenFader screenFader;
    LevelChanger levelChanger;

    static public GameManager instance { get; private set; }

    public GameData data = new GameData();

    void Awake() {
        instance = this;
        levelChanger = GetComponent<LevelChanger>();
    }

    void Start() {
        Time.timeScale = 0;
        screenFader.RequestFadeIn();
    }

    void Update() {
        if (!levelChanger.requestedLeave && Input.GetKeyDown(KeyCode.Escape))
            levelChanger.GoBackToMenu();
        if (screenFader.IsIdle() && Time.timeScale == 0 && !levelChanger.requestedLeave)
            Time.timeScale = 1;
    }

    public void RestartGame() {
        levelChanger.RestartLevel();
    }

    public void GoToNextLevel(bool showTarget=true) {
        if (showTarget) {
            Camera.main.GetComponent<CameraFollow>().gameEnded = true;
            Invoke("Hacks", 1f);
        }
        else
            Hacks();
    }

    void Hacks() {
        levelChanger.GoToNextLevel();
    }
}
