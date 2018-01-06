using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public BoxCluster player;

    void Update() {
        if (!player)
            LookForPlayer();
        else
            FollowPlayer();
    }

    void LookForPlayer() {
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj)
            player = playerObj.GetComponent<BoxCluster>();
    }

    void FollowPlayer() {
        MoveTo(player.followedPosition());
    }

    public void MoveTo(Vector3 position) {
        position.z = -10;
        transform.position = position;
    }
}
