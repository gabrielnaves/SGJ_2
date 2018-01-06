using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLoading : MonoBehaviour {

    public GameObject[] boxPrefabs;
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
        int index = 0;
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                index = i * width + j;
                if (mapLoader.data.data[index] > 1)
                    InstantiateBox(i, j, boxPrefabs[mapLoader.data.data[index] - 2]);
            }
        }
    }

    void InstantiateBox(int i, int j, GameObject prefab) {
        var box = Instantiate(prefab);
        box.transform.parent = targetContainer;
        box.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
        box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
