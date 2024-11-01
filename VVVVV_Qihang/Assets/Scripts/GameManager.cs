using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

//Todo el tema de ajustes del juego.
public class GameManager : MonoBehaviour
{
    const string MainScene = "MainMenu";
    const string TutorialScene = "LevelTutorial";
    const string PauseScene = "PauseMenu";
    const string GameOverScene = "GameOverMenu";
    const string SettingScene = "SettingMenu";
    const string GameplayScene = "Level";

    public static GameManager manager;

    public void Awake()
    {
        if (manager != null && manager != this)
        {
            Debug.Log("Destroying GameManager");
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Contains(GameplayScene))
            SwitchPause();
        if (Input.GetKeyDown(KeyCode.Q))
            GameOver();
    }


    public void SwitchPause()
    {
        if(!SceneManager.GetSceneByName(PauseScene).isLoaded)
            Pause();
        else
            Resume();
    }

    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(TutorialScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0f;
        SceneManager.LoadScene(PauseScene, LoadSceneMode.Additive);
    }
 
    public void Resume()
    {
        Debug.Log("Resume");
        SceneManager.UnloadSceneAsync(PauseScene);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Debug.Log("GameOver");
        SceneManager.LoadScene(GameOverScene);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        Debug.Log("Home");
        SceneManager.LoadScene(MainScene);
        Time.timeScale = 1f;
    }

    public void Setting()
    {
        Debug.Log("Setting");
        Time.timeScale = 0f;
        SceneManager.LoadScene(SettingScene, LoadSceneMode.Additive);
    }

    public void SettingToHome()
    {
        Debug.Log("From Setting to Home");
        SceneManager.UnloadSceneAsync(SettingScene);
        Time.timeScale = 1f;
    }
}
