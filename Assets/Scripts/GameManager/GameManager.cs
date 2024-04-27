using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;


/// <summary>
/// Singleton
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Static Memory
    static GameManager _Instance;
    public static GameManager Instance
    {
        get { if (_Instance == null)
            {
                Debug.LogWarning("Cannot find Game Manager, GameManager._Instance is null");
            }

            return _Instance;
        }
    }
    static bool _isPaused = false;
    public bool IsPaused
    {
        get => _isPaused;
        set { 
            if (_isPaused != value)
            { //code block only executes when new value is different
                if (syncPauseMenu)
                {
                    TogglePauseMenu(value);
                }

                _isPaused = value;
            } //code block skips if value is the same
        }
    }
    bool syncPauseMenu = true;
    #endregion

    #region shorthand getters 
    //getters that help shorten code and improve readability
    public SceneData GetSceneByType(SceneType sceneType)
    {
        return sceneFlow.sceneDict[sceneType];
    }

    #endregion

    [SerializeField] SceneBindings sceneFlow; //this is used to set the 'static SceneBindings Scenes' from within the inspector
    public LevelManager CurrentLevel;

    #region Monobehavior Callbacks
    private void Awake()
    {
        //enforce the singleton principle
        if (_Instance != null)
        {
            Destroy(_Instance.gameObject);
            _Instance = this; //we actually overwrite the previous gamemanager, because if we destroy this gamemanager it makes the inspector unity events throw null errors if they want to interface with the gamemanager
        } else
        {
            _Instance = this;    
        }

        //Pre Start initializing here
        DontDestroyOnLoad(this.gameObject);
    }

    #endregion

    #region Scene Loading
    //Most of the following are one line functions, this script is the only script that will ever deal with the SceneManager
    //If other scripts want to initiate scene manipulation they must go through this script's defined interface (there is no actual c# interface though)

    public void QuitGame()
    {
#if UNITY_EDITOR
        // Quit the game
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game
        Application.Quit();
#endif
    }

    public void LoadTitle()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        IsPaused = false;
        SceneManager.LoadScene(GetSceneByType(SceneType.Title).name);
    }

    public void LoadSandBox()
    {
        IsPaused = false;
        SceneManager.LoadScene(GetSceneByType(SceneType.Sandbox).name);
    }

    public void LoadLevel(SceneData newLevel)
    {
        IsPaused = false;
        SceneManager.LoadScene(newLevel.name);
    }

    #region MainMenu
    public void LoadMainMenu()
    {
        IsPaused = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        SceneManager.LoadScene(GetSceneByType(SceneType.MainMenu).name);
        //the main menu will itself, call additive loading and unloading of its menu scenes
        //The main menu is a finite state machine for loading menu scenes additively
    }

    //Called by MainMenuManager
    public void LoadLevelSelect() { SceneManager.LoadSceneAsync(GetSceneByType(SceneType.LevelMenu).name, LoadSceneMode.Additive); }
    public void UnloadLevelSelect() { SceneManager.UnloadSceneAsync(GetSceneByType(SceneType.LevelMenu).name); }



    #endregion
    #region Pausing
    /// <summary>
    /// Pausing is nearly entirely handled by isPaused (public static getter / setter)
    /// </summary>
   
    //Static because ispaused is a static function
    private void TogglePauseMenu(bool pauseState)
    {
        switch (pauseState)
        {
            case true: //paused
                SceneManager.LoadSceneAsync(GetSceneByType(SceneType.PauseMenu).name, LoadSceneMode.Additive);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                break;
            case false: //unpaused
                SceneManager.UnloadSceneAsync(GetSceneByType(SceneType.PauseMenu).name);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                break;
        }
    }
    #endregion

    //This function handles the entire game over sequence
    public void GameOver(bool won) //TODO: turn parameter into GameOverData struct
    {
        //TODO, I think gameover will be called by a levelManager
        syncPauseMenu = false;
        IsPaused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;

        SceneManager.LoadSceneAsync(GetSceneByType(SceneType.GameOver).name, LoadSceneMode.Additive);
        
    }

    public void UnloadGameOverScreen()
    {
        IsPaused = false;
        syncPauseMenu = true;
        Time.timeScale = 1.0f;
        SceneManager.UnloadSceneAsync(GetSceneByType(SceneType.GameOver).name);
    }
    #endregion
}







//DO NOW
//using player input sendmessages, set up the loading to next scene

//then get to work on the main menu system
//finite state machine for sub menuing
//level select menu