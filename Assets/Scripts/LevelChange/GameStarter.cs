using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    public ScreenFader screenFader;

    void Start() {
        screenFader.RequestFadeIn();
    }

    void Update() {
    if (screenFader.IsIdle())
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if (Input.anyKeyDown) {
            GetComponent<LevelChanger>().StartGame();
        }
    }
}
