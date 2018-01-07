﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType {
    GREEN,
    BLUE,
    RED,
    DEFAULT
}

public class Box : MonoBehaviour {

    public BoxType type;
    public bool inCluster;

    Animator animator;
    Dictionary<BoxType, string> triggers = new Dictionary<BoxType, string>()
    {
        { BoxType.DEFAULT, "toDefault" },
        { BoxType.RED, "toRed" },
        { BoxType.GREEN, "toGreen" },
        { BoxType.BLUE, "toBlue" }
    };

    void Start() {
        animator = GetComponent<Animator>();
        UpdateType(type);
    }

    void OnCollisionEnter2D(Collision2D other) {
        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox) {
            if (inCluster) {
                if (otherBox.type == BoxType.BLUE)
                    BoxCluster.instance.AddBox(otherBox);
            }
            else if (type == BoxType.RED && otherBox.type == BoxType.BLUE) {
                Destroy(otherBox.gameObject);
                Destroy(gameObject);
            }
        }
    }

    void OnDestroy() {
        if (inCluster)
            BoxCluster.instance.RemoveBox(this);
    }

    public void UpdateType(BoxType newType) {
        animator.SetTrigger(triggers[newType]);
        transform.GetChild((int)type).gameObject.SetActive(false);
        transform.GetChild((int)newType).gameObject.SetActive(true);
        type = newType;
    }

    void LateUpdate() {
        if (inCluster) {
            if ((transform.position - Camera.main.transform.position).magnitude > 15f)
                BoxCluster.instance.RemoveBox(this);
        }
    }
}
