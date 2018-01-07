using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public string baseLevelKey = "level";
    public string menuScene = "menu";
    public string levelScene = "level";
    public ScreenFader screenFader;

    public void StartGame() {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        screenFader.RequestFadeOut();
        Invoke("RestartCurrentLevel", screenFader.fadeTime);
    }

    public void RestartCurrentLevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(levelScene);
    }

    public void GoToNextLevel() {
        int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        string nextJSONFile = baseLevelKey + nextLevel;
        if (Resources.Load(nextJSONFile) as TextAsset != null) {
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            Invoke("RestartCurrentLevel", screenFader.fadeTime);
        }
        else {
            Invoke("LoadMenuScene", screenFader.fadeTime);
        }
        screenFader.RequestFadeOut();
    }

    public void LoadMenuScene() {
        Time.timeScale = 1;
        SceneManager.LoadScene(menuScene);
    }
}
