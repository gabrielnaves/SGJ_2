using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    void Update() {
        if (BoxCluster.instance)
            FollowPlayer();
    }

    void FollowPlayer() {
        MoveTo(BoxCluster.instance.followedPosition());
    }

    public void MoveTo(Vector3 position) {
        position.z = -10;
        transform.position = position;
    }
}
