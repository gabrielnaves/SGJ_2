﻿using System.Collections;
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
        if (BoxCount() == 0)
            return Camera.main.transform.position;
        Vector2 result = Vector2.zero;
        float minHeight = 999999;
        int count = 1;
        foreach (var box in connectedBoxes) {
            if (Mathf.Abs(box.transform.position.y - minHeight) < 0.2f) {
                result += (Vector2)box.transform.position;
                count++;
            }
            else if (box.transform.position.y < minHeight) {
                minHeight = box.transform.position.y;
                result = box.transform.position;
                count = 1;
            }
        }
        return result / count;
    }

    public void AddBox(Box box) {
        if (!connectedBoxes.Contains(box) && box != null) {
            connectedBoxes.Add(box);
            box.transform.parent = transform;
            box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            box.inCluster = true;
            box.UpdateType(BoxType.BLUE);
            GameManager.instance.data.amountOnCluster++;
        }
    }

    public void RemoveBox(Box box, bool updateType = true) {
        if (connectedBoxes.Contains(box)) {
            connectedBoxes.Remove(box);
            if (BoxesTouched.instance)
                BoxesTouched.instance.Add(box);
            box.inCluster = false;
            if (updateType)
                box.UpdateType(BoxType.WHITE);
            GameManager.instance.data.amountOnCluster--;
        }
    }

    public Vector2 CenterPosition() {
        Vector2 result = Vector2.zero;
        foreach(var box in connectedBoxes)
            result += (Vector2)box.transform.position;
        return result / (float)connectedBoxes.Count;
    }

    public int BoxCount() {
        int result = 0;
        for(int i = 0; i < connectedBoxes.Count; ++i) {
            if (connectedBoxes[i] == null)
                connectedBoxes.RemoveAt(i--);
            else
                result++;
        }
        return result;
    }
}
