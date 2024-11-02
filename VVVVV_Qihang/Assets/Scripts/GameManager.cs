using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Todo el tema de ajustes del juego.
public class GameManager : MonoBehaviour
{
    const string MainScene = "MainMenu", PauseScene = "PauseMenu", GameOverScene = "GameOverMenu", SettingScene = "SettingMenu";
    const string TutorialScene = "LevelTutorial", GameplayScene = "Level";
    const string EventSystemName = "EventSystem", MusicVolume = "MusicVolume";
    const int VolumePlus = 20;

    public static GameManager manager;
    public static EventSystem eventSystem; //Crearemos nuestro propio EventSystem para evitar problemas con el que ya existe en la escena.

    [SerializeField] private AudioMixer audioMixer;

    public void Awake()
    {
        if (manager != null && manager != this)
        {
            //Debug.Log("Destroying GameManager");
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //Solo se creara una vez al principio cuando no exista.
        if (eventSystem == null)
        {
            GameObject newEventSystem = new GameObject(EventSystemName);
            newEventSystem.AddComponent<EventSystem>();
            newEventSystem.AddComponent<StandaloneInputModule>();
            eventSystem = newEventSystem.GetComponent<EventSystem>();

            DontDestroyOnLoad(newEventSystem);
        }
    }

    private void Start()
    {
        //Ajustamos el volumen al valor guardado a la anterior vez que hemos entrado.
        SetVolumToAudioMixer();
    }

    private void Update()
    {
        //Si estamos en una escena de juego, podremos pausar el juego.
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name.Contains(GameplayScene))
            SwitchPause();

        //Cuando estemos en la escena de ajustes, actualizaremos el slider de volumen.
        if (SceneManager.GetSceneByName(SettingScene).isLoaded)
        {
            UpdateVolumeSlider();
            SetVolumToAudioMixer();
        }


        if (Input.GetKeyDown(KeyCode.Q))
            GameOver(); //TEST
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
        //Debug.Log("Play");
        SceneManager.LoadScene(TutorialScene);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        //Debug.Log("Quit");
        Application.Quit();
    }

    public void Pause()
    {
        //Debug.Log("Pause");
        Time.timeScale = 0f;
        SceneManager.LoadScene(PauseScene, LoadSceneMode.Additive);
    }
 
    public void Resume()
    {
        //Debug.Log("Resume");
        SceneManager.UnloadSceneAsync(PauseScene);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        //Debug.Log("GameOver");
        SceneManager.LoadScene(GameOverScene);
        Time.timeScale = 1f;
    }

    public void Home()
    {
        //Debug.Log("Home");
        SceneManager.LoadScene(MainScene);
        Time.timeScale = 1f;
    }

    public void Setting()
    {
        //Debug.Log("Setting");
        Time.timeScale = 0f;
        SceneManager.LoadScene(SettingScene, LoadSceneMode.Additive);
    }

    public void SettingToHome()
    {
        //Debug.Log("From Setting to Home");
        SceneManager.UnloadSceneAsync(SettingScene);
        Time.timeScale = 1f;
    }
    private void UpdateVolumeSlider()
    {
        Slider musicSlider = FindAnyObjectByType<Slider>();
        musicSlider.value = PlayerPrefs.GetFloat(MusicVolume, 1f);
        //Debug.Log("Music Volume: " + musicSlider.value);
    }

    public void SaveVolume(float sliderValue)
    {
        PlayerPrefs.SetFloat(MusicVolume, sliderValue);
    }

    private void SetVolumToAudioMixer()
    {
        audioMixer.SetFloat(MusicVolume, Mathf.Log10(PlayerPrefs.GetFloat(MusicVolume, 1f)) * VolumePlus);
    }
}
