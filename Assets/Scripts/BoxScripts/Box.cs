using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoxType {
    GREEN,
    BLUE,
    RED,
    DEFAULT,
    WHITE
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
        { BoxType.BLUE, "toBlue" },
        { BoxType.WHITE, "toWhite" }
    };

    void Awake() {
        animator = GetComponent<Animator>();
    }

    void Start() {
        UpdateType(type);
    }

    void OnCollisionEnter2D(Collision2D other) {
        var otherBox = other.gameObject.GetComponent<Box>();
        if (otherBox) {
            if (inCluster) {
                if (otherBox.type == BoxType.BLUE || otherBox.type == BoxType.WHITE)
                    BoxCluster.instance.AddBox(otherBox);
                if (otherBox.type == BoxType.WHITE)
                    otherBox.UpdateType(BoxType.BLUE);
            }
            if (type == BoxType.RED && otherBox.type == BoxType.BLUE) {
                Destroy(otherBox.gameObject);
                Destroy(gameObject);
            }
            if (type == BoxType.BLUE && otherBox.type == BoxType.GREEN) {
                if (inCluster)
                    BoxCluster.instance.RemoveBox(this, updateType:false);
                UpdateType(otherBox.type);
                GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            }
        }
    }

    void OnDestroy() {
        if (inCluster)
            BoxCluster.instance.RemoveBox(this, updateType:false);
        if (type == BoxType.BLUE)
            GameManager.instance.data.blueAmountOnLevel--;
    }

    public void UpdateType(BoxType newType) {
        animator.SetTrigger(triggers[newType]);
        transform.GetChild((int)type).gameObject.SetActive(false);
        transform.GetChild((int)newType).gameObject.SetActive(true);
        type = newType;
    }

    bool requestedExit;
    void LateUpdate() {
        if (inCluster) {
            float distance = (transform.position - Camera.main.transform.position).magnitude;
            if (distance > 15f && !requestedExit) {
                requestedExit = true;
                Invoke("ExitCluster", 5f);
            }
            else if (distance < 15f && requestedExit) {
                requestedExit = false;
                CancelInvoke("ExitCluster");
            }
            if (distance > 18f) {
                CancelInvoke();
                ExitCluster();
            }
        }
    }

    void ExitCluster() {
        BoxCluster.instance.RemoveBox(this);
    }
}
