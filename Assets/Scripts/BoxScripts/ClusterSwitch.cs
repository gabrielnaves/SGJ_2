using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterSwitch : MonoBehaviour {

    public float changeCooldown = 1f;

    BoxCluster cluster;
    bool canChange = true;
    float elapsedTime;

    void Awake() {
        cluster = GetComponent<BoxCluster>();
    }

    void Update() {
        if (GameManager.instance.data.amountOnCluster == 0 &&
                GameManager.instance.data.touchedBoxes > 0) {
            var target = BoxesTouched.instance.FirstBox();
            cluster.AddBox(target);
            canChange = false;
            elapsedTime = 0;
            target.ignorePositioning = true;
            target.Invoke("ResetPositioningFlag", 2f);
        }
        if (canChange && BoxesTouched.instance.Amount() > 0) {
            bool changeCluster = false;
            Box target = null;
            if (Input.GetKeyDown(KeyCode.Q)) {
                changeCluster = true;
                target = BoxesTouched.instance.LastBox();
            }
            else if (Input.GetKeyDown(KeyCode.E)) {
                changeCluster = true;
                target = BoxesTouched.instance.FirstBox();
            }
            if (changeCluster) {
                while (cluster.connectedBoxes.Count > 0)
                    cluster.RemoveBox(cluster.connectedBoxes[0]);
                cluster.AddBox(target);
                canChange = false;
                target.ignorePositioning = true;
                target.Invoke("ResetPositioningFlag", 2f);
            }
        }
        else {
            elapsedTime += Time.deltaTime;
            if (elapsedTime > changeCooldown) {
                elapsedTime = 0;
                canChange = true;
            }
        }
    }
}
