using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxContainer : MonoBehaviour {

    static public BoxContainer instance { get; private set; }

    void Awake() {
        instance = this;
    }
}
