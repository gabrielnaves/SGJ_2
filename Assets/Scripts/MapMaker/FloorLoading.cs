using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorLoading : MonoBehaviour {

    public GameObject floorUnitPrefab;
    public Transform targetContainer;

    MapLoader mapLoader;

    void Awake() {
        mapLoader = GetComponent<MapLoader>();
    }

    void Start() {
        int width = mapLoader.data.width;
        int height = mapLoader.data.height;
        float[] offset = { -((float)width)/2f, ((float)height)/2f };
        int index = 0;
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                index = i * width + j;
                if (mapLoader.data.data[index] == 1) {
                    var unit = Instantiate(floorUnitPrefab);
                    unit.transform.parent = targetContainer;
                    unit.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                                          Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
                }
            }
        }
    }
}
