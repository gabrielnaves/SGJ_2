using System.Collections;
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
        animator.SetTrigger(triggers[type]);
    }

    void OnCollisionEnter2D(Collision2D other) {
        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox) {
            if (inCluster) {
                if (otherBox.type == BoxType.BLUE)
                    BoxCluster.instance.AddBox(otherBox);
            }
        }
    }

    void OnDestroy() {
        if (BoxCluster.instance && type == BoxType.BLUE)
            BoxCluster.instance.RemoveBox(this);
    }

    public void UpdateType(BoxType newType) {
        animator.SetTrigger(triggers[newType]);
        type = newType;
    }
}
