using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxesTouched : MonoBehaviour {

    static public BoxesTouched instance { get; private set; }

    List<Box> boxList = new List<Box>();

    void Awake() {
        instance = this;
    }

    public void Add(Box box) {
        if (!boxList.Contains(box)) {
            boxList.Add(box);
            box.transform.parent = transform;
        }
    }

    public Box FirstBox() {
        if (boxList.Count == 0)
            return null;
        var first = boxList[0];
        boxList.RemoveAt(0);
        return first;
    }

    void LateUpdate() {
        for (int i = 0; i < boxList.Count; ++i) {
            if (boxList[i] == null)
                boxList.RemoveAt(i--);
            else if (boxList[i].type == BoxType.GREEN) {
                BoxesTurned.instance.Add(boxList[i]);
                boxList.RemoveAt(i--);
            }
            else if (boxList[i].inCluster) {
                boxList.RemoveAt(i--);
            }
        }
    }
}
