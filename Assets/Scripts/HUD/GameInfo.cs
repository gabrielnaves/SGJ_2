using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameInfo : MonoBehaviour {

    public Text waterCount;
    public Text iceCount;
    public Text score;

    string waterStr;
    string iceStr;
    string scoreStr;

    void Start() {
        waterStr = waterCount.text;
        iceStr = iceCount.text;
        scoreStr = score.text;
    }

    void LateUpdate() {
        waterCount.text = waterStr + GameManager.instance.data.amountOnCluster;
        iceCount.text = iceStr + GameManager.instance.data.touchedBoxes;
        score.text = scoreStr + GameManager.instance.data.turnedBoxes;
    }
}
