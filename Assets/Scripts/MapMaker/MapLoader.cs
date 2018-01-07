using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData {
    public int width;
    public int height;
    public int tilewidth;
    public int tileheight;
    public LayerData[] layers;
}

[Serializable]
public class LayerData
{
    public int width;
    public int height;
    public int[] data;
    public string name;
}

public class MapLoader : MonoBehaviour {

    public string jsonFile;

    public LayerData data { get; private set; }
    MapData rawData;

    void Awake() {
        rawData = JsonUtility.FromJson<MapData>((Resources.Load(jsonFile) as TextAsset).text);
        data = rawData.layers[0];
    }
}
