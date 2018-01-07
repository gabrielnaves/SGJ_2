using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameData {
    public int blueAmountOnLevel;
    public int turnedBoxes;
    public int touchedBoxes;
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
        if (screenFader.IsIdle() && Time.timeScale == 0)
            Time.timeScale = 1;
    }
}
