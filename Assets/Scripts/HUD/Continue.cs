using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Continue : MonoBehaviour {

    string textStr;
    Text text;

    bool active = false;

    void Awake() {
        text = GetComponent<Text>();
        textStr = text.text;
        text.text = "";
    }

    void Update() {
        if (GameManager.instance.data.turnedBoxes > 0 && !active) {
            text.text = textStr;
            active = true;
        }
        if (Input.GetKeyDown(KeyCode.P) && active) {
            enabled = false;
            GameManager.instance.GoToNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.O)) {
            enabled = false;
            GameManager.instance.GoToNextLevel(showTarget:false);
        }
    }
}
