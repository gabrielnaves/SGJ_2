using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoLoader : MonoBehaviour {

    public GameObject playerPrefab;

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
                if (mapLoader.data.data[index] == 5)
                    InstantiatePlayer(i, j);
            }
        }
    }

    void InstantiatePlayer(int i, int j) {
        var player = Instantiate(playerPrefab);
        player.transform.position = new Vector2(Mathf.Lerp(offset[0], offset[0]+(float)width, (float)j/(float)width),
                                             Mathf.Lerp(offset[1], offset[1]-(float)height, (float)i/(float)height));
        Camera.main.GetComponent<CameraFollow>().MoveTo(player.transform.position);
    }
}
