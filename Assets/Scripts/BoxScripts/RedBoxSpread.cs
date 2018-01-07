using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoxSpread : MonoBehaviour {

    public GameObject boxPrefab;
    public Transform targetContainer;
    public MapLoader mapLoader;
    public float delay = 5f;

    static public RedBoxSpread instance { get; private set; }

    int[] map;
    Dictionary<int, GameObject> fireDict = new Dictionary<int, GameObject>();
    int width;
    int height;
    float[] offset = new float[2];

    void Awake() {
        instance = this;
        InvokeRepeating("SpreadFire", delay, delay);
    }

    void SpreadFire() {
        for (int i = 0; i < map.Length; ++i) {
            if (fireDict.ContainsKey(i) && fireDict[i] == null) {
                fireDict.Remove(i);
                map[i] = 0;
            }
            else if (map[i] == 3)
                map[i] = 2;
        }
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                if (map[ToIndex(i, j)] == 2) {
                    if (map[ToIndex(i+1, j)] == 0)
                        MakeFire(i+1, j);
                    if (map[ToIndex(i-1, j)] == 0)
                        MakeFire(i-1, j);
                    if (map[ToIndex(i, j-1)] == 0)
                        MakeFire(i, j-1);
                    if (map[ToIndex(i, j+1)] == 0)
                        MakeFire(i, j+1);
                }
            }
        }
    }

    void MakeFire(int i, int j) {
        var box = Instantiate(boxPrefab);
        box.transform.parent = targetContainer;
        box.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
        box.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        box.GetComponent<Box>().type = BoxType.RED;
        map[ToIndex(i, j)] = 3;
        fireDict[ToIndex(i, j)] = box;
    }

    public void AddToList(int i, int j, GameObject box) {
        if (map == null)
            CreateMap();
        map[ToIndex(i, j)] = 2;
        fireDict[ToIndex(i, j)] = box;
    }

    void CreateMap() {
        width = mapLoader.data.width;
        height = mapLoader.data.height;
        offset[0] = -((float)width)/2f;
        offset[1] = ((float)height)/2f;
        map = new int[width * height];
        for (int i = 0; i < map.Length; ++i)
            if (mapLoader.data.data[i] == 1)
                map[i] = 1;
    }

    int ToIndex(int i, int j) {
        return i * width + j;
    }
}
