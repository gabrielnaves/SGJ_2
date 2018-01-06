using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    GameObject player;

    void Update() {
        if (!player)
            LookForPlayer();
        else
            FollowPlayer();
    }

    void LookForPlayer() {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FollowPlayer() {
        var position = player.transform.position;
        position.z = -10;
        transform.position = position;
    }
}
