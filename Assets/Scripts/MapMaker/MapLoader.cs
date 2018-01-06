using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MapData
{
    public int width;
    public int height;
    public int[] data;
}

public class MapLoader : MonoBehaviour {

    public string jsonFile;

    public MapData data { get; private set; }

    void Awake() {
        data = JsonUtility.FromJson<MapData>((Resources.Load(jsonFile) as TextAsset).text);
    }
}
