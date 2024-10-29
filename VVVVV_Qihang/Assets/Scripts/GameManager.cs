using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Todo el tema de ajustes del juego.
public class GameManager : MonoBehaviour
{
    const string TutorialScene = "LevelTutorial";
    public static GameManager manager;

    public void Awake()
    {
        if(manager != null && manager != this )
            Destroy(this.gameObject);

        manager = this;
    }

    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene(TutorialScene);
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Pause()
    {
        Debug.Log("Pause");
    }

}
