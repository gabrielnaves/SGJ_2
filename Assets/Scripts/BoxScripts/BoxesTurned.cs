using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesTurned : MonoBehaviour {

    static public BoxesTurned instance { get; private set; }

    List<Box> boxList = new List<Box>();

    void Awake() {
        instance = this;
    }

    public void Add(Box box) {
        if (!boxList.Contains(box)) {
            boxList.Add(box);
            box.transform.parent = transform;
            GameManager.instance.data.turnedBoxes++;
            GameManager.instance.data.blueAmountOnLevel--;
        }
    }
}
