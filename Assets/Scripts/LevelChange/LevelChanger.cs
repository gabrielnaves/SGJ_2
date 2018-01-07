using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public string baseLevelKey = "level";
    public string menuScene = "menu";
    public string levelScene = "level";

    public void StartGame() {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        SceneManager.LoadScene(levelScene);
    }

    public void RestartCurrentScene() {
        SceneManager.LoadScene(levelScene);
    }

    public void GoToNextLevel() {
        int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        string nextJSONFile = baseLevelKey + nextLevel;
        if (Resources.Load(nextJSONFile) as TextAsset != null) {
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            SceneManager.LoadScene(levelScene);
        }
        else {
            SceneManager.LoadScene(menuScene);
        }
    }
}
