using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStarter : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
        else if (Input.anyKeyDown) {
            GetComponent<LevelChanger>().StartGame();
        }
    }
}
