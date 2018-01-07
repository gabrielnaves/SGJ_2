using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour {

    public string baseLevelKey = "level";
    public string menuScene = "menu";
    public string levelScene = "level";
    public ScreenFader screenFader;

    public bool requestedLeave = false;

    public void StartGame() {
        PlayerPrefs.SetInt("CurrentLevel", 1);
        screenFader.RequestFadeOut();
        StartCoroutine(LoadLevelScene());
    }

    public void RestartLevel() {
        StartCoroutine(LoadLevelScene());
        requestedLeave = true;
    }

    public void GoToNextLevel() {
        int nextLevel = PlayerPrefs.GetInt("CurrentLevel") + 1;
        string nextJSONFile = baseLevelKey + nextLevel;
        if (Resources.Load(nextJSONFile) as TextAsset != null) {
            PlayerPrefs.SetInt("CurrentLevel", nextLevel);
            StartCoroutine(LoadLevelScene());
        }
        else {
            StartCoroutine(LoadMenuScene());
        }
        requestedLeave = true;
    }

    IEnumerator LoadMenuScene() {
        screenFader.RequestFadeOut();
        Time.timeScale = 0;
        float elapsedTime = 0f;
        while (elapsedTime < screenFader.fadeTime) {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(menuScene);
    }

    IEnumerator LoadLevelScene() {
        screenFader.RequestFadeOut();
        Time.timeScale = 0;
        float elapsedTime = 0f;
        while (elapsedTime < screenFader.fadeTime) {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1;
        SceneManager.LoadScene(levelScene);
    }
}
