using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxCluster : MonoBehaviour {

    static public BoxCluster instance { get; private set; }

    public GameObject boxPrefab;
    public List<Box> connectedBoxes = new List<Box>();
    public Vector2 startingPos { get; set; }

    void Awake() {
        if (instance)
            Debug.LogError("Multiple instances of box cluster");
        instance = this;
    }

    void Start() {
        StartCluster();
    }

    void StartCluster() {
        var firstBox = Instantiate(boxPrefab);
        firstBox.transform.position = startingPos;
        AddBox(firstBox.GetComponent<Box>());
    }

    public Vector3 followedPosition() {
        return connectedBoxes[0].transform.position;
    }

    public void AddBox(Box box) {
        if (!connectedBoxes.Contains(box)) {
            connectedBoxes.Add(box);
            box.transform.parent = transform;
            box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            box.inCluster = true;
        }
    }

    public void RemoveBox(Box box) {
        if (connectedBoxes.Contains(box)) {
            connectedBoxes.Remove(box);
            box.transform.parent = null;
            box.inCluster = false;
        }
    }
}
