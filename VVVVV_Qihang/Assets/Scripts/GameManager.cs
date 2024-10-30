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
    const string GameplayScene = "Level";

    public static GameManager manager;

    public void Awake()
    {
        if(manager != null && manager != this )
        {
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

        //Cargamos la escena de juego.
        SceneManager.LoadScene(TutorialScene);
    }

    public void Quit()
    {
        Debug.Log("Quit");

        //Cerramos la aplicación.
        Application.Quit();
    }

    public void Pause()
    {
        Debug.Log("Pause");
        Time.timeScale = 0f; //Pausamos el juego.
        SceneManager.LoadScene(PauseScene, LoadSceneMode.Additive);
    }
 
    public void Resume()
    {
        Debug.Log("Resume");
        SceneManager.UnloadSceneAsync(PauseScene);
        Time.timeScale = 1f; //Reanudamos el juego.
    }

    public void GameOver()
    {
        Debug.Log("GameOver");

        SceneManager.LoadScene(GameOverScene);
    }

    public void Home()
    {
        Debug.Log("Home");

        //Cargamos la escena del inicio.
        SceneManager.LoadScene(MainScene);
    }


}
