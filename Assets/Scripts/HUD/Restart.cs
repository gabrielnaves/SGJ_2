using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Restart : MonoBehaviour {

    void Update() {
        if (Input.GetKeyDown(KeyCode.R)) {
            enabled = false;
            GameManager.instance.RestartGame();
        }
    }
}
