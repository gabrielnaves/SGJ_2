using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCluster : MonoBehaviour {

    public GameObject boxPrefab;
    public List<Box> connectedBoxes = new List<Box>();
    public Vector2 startingPos { get; set; }

    void Start() {
        StartCluster();
    }

    void StartCluster() {
        connectedBoxes.Add(Instantiate(boxPrefab).GetComponent<Box>());
        connectedBoxes[0].transform.parent = transform;
        connectedBoxes[0].transform.position = startingPos;
    }

    public Vector3 followedPosition() {
        return connectedBoxes[0].transform.position;
    }
}
