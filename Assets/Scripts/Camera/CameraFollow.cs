using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float speed = 1f;

    void FixedUpdate() {
        if (BoxCluster.instance)
            FollowPlayer();
    }

    void FollowPlayer() {
        var target = BoxCluster.instance.followedPosition();
        Vector2 position = transform.position;
        position = Vector2.Lerp(transform.position, target, speed);
        transform.position = new Vector3(position.x, position.y, -10);
    }

    public void MoveTo(Vector3 position) {
        position.z = -10;
        transform.position = position;
    }
}
