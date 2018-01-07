using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLoader : MonoBehaviour {

    public GameObject boxPrefab;
    public GameObject blueBox;
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
                if (mapLoader.data.data[index] == 3)
                    InstantiateBlueBox(i, j);
                else if (mapLoader.data.data[index] >= 2 && mapLoader.data.data[index] <= 4)
                    InstantiateBox(i, j, (BoxType)(mapLoader.data.data[index] - 2));
                else if (mapLoader.data.data[index] == 7)
                    InstantiateBox(i, j, BoxType.WHITE);
            }
        }
    }

    void InstantiateBox(int i, int j, BoxType type) {
        var box = Instantiate(boxPrefab);
        box.transform.parent = targetContainer;
        box.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
        box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        box.GetComponent<Box>().type = type;
        if (type == BoxType.RED)
            RedBoxSpread.instance.AddToList(i, j, box);
        else if (type == BoxType.WHITE)
            GameManager.instance.data.blueAmountOnLevel++;
    }

    void InstantiateBlueBox(int i, int j) {
        var box = Instantiate(blueBox);
        box.transform.parent = targetContainer;
        box.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
    }
}
