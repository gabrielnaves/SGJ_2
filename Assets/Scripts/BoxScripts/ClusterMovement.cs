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
        Vector2 inputForce = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputForce.x *= speed;
        inputForce.y *= speed / 2f;
        foreach (var box in cluster.connectedBoxes)
            box.GetComponent<Rigidbody2D>().AddForce(inputForce);
    }
}
