using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;

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
        MoveTo(player.transform.position);
    }

    public void MoveTo(Vector3 position) {
        position.z = -10;
        transform.position = position;
    }
}
