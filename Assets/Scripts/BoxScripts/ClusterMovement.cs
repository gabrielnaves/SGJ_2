using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClusterMovement : MonoBehaviour {

    public float speed;

    BoxCluster cluster;

    void Awake() {
        cluster = GetComponent<BoxCluster>();
    }

    void FixedUpdate() {
        if (!UpdateJoiningMovement())
            UpdateHorizontalMovement();
    }

    void UpdateHorizontalMovement() {
        Vector2 inputForce = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputForce.x *= speed;
        inputForce.y *= speed / 2f;
        foreach (var box in cluster.connectedBoxes)
            box.GetComponent<Rigidbody2D>().AddForce(inputForce);
    }

    bool UpdateJoiningMovement() {
        if (Input.GetKey(KeyCode.Space)) {
            var center = cluster.CenterPosition();
            Vector2 force = Vector2.zero;
            foreach(var box in cluster.connectedBoxes) {
                force = (center - (Vector2)box.transform.position).normalized;
                force.x *= speed * 5;
                box.GetComponent<Rigidbody2D>().AddForce(force);
            }
            return true;
        }
        else
            return false;
    }
}
