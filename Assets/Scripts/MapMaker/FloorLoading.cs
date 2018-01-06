using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoading : MonoBehaviour {

    public GameObject floorUnitPrefab;
    public Transform targetContainer;

    MapLoader mapLoader;
    int width;
    int height;
    float[] offset = new float[2];

    void Awake() {
        mapLoader = GetComponent<MapLoader>();
    }

    void Start() {
        width = mapLoader.data.width;
        height = mapLoader.data.height;
        offset[0] = -((float)width)/2f;
        offset[1] = ((float)height)/2f;

        for (int i = 0; i < height; ++i)
            for (int j = 0; j < width; ++j)
                if (mapLoader.data.data[ToIndex(i, j)] == 1)
                    InstantiateBox(i, j);
    }

    void InstantiateBox(int i, int j) {
        var unit = Instantiate(floorUnitPrefab);
        unit.transform.parent = targetContainer;
        unit.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
        if (i == 0 || j == 0 || i == height-1 || j == width-1)
            DisableColliders(unit);
        else {
            int up = ToIndex(i-1, j);
            int down = ToIndex(i+1, j);
            int left = ToIndex(i, j-1);
            int right = ToIndex(i, j+1);
            if (mapLoader.data.data[up] == 1 && mapLoader.data.data[down] == 1 &&
                mapLoader.data.data[left] == 1 && mapLoader.data.data[right] == 1)
                DisableColliders(unit);
        }
    }

    int ToIndex(int i, int j) {
        return i * width + j;
    }

    void DisableColliders(GameObject unit) {
        Collider2D[] cols = unit.GetComponents<Collider2D>();
            foreach (var col in cols)
                col.enabled = false;
    }
 }
