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
            if (inCluster)
                BoxCluster.instance.AddBox(otherBox);

            if (type == BoxType.DEFAULT)
                UpdateType(otherBox.type);
            else if (type == BoxType.GREEN && otherBox.type == BoxType.RED)
                Destroy(gameObject);
            else if (type == BoxType.BLUE && otherBox.type == BoxType.GREEN)
                Destroy(gameObject);
            else if (type == BoxType.RED && otherBox.type == BoxType.BLUE)
                Destroy(gameObject);
            else if (type != otherBox.type)
                otherBox.UpdateType(type);
        }
    }

    void OnDestroy() {
        if (BoxCluster.instance)
            BoxCluster.instance.RemoveBox(this);
    }

    public void UpdateType(BoxType newType) {
        animator.SetTrigger(triggers[newType]);
        type = newType;
    }
}
