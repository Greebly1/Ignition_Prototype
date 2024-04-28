using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The main menu works like a state machine
/// Switching between different menu states [main, level select, options, etc]
/// </summary>
public class MainMenuManager : MonoBehaviour
{
    #region static memory
    static MainMenuManager _Instance;
    public static MainMenuManager Instance
    {
        get
        {
            if (_Instance == null)
            {
                Debug.LogWarning("Cannot find Main Menu Manager, MainMenuManager._Instance is null");
            }

            return _Instance;
        }
    }
    #endregion

    #region Editor Variables
    [SerializeField] GameObject MainMenuGameobject = null;
    [SerializeField] MainMenuState StartupState = MainMenuState.Main;
    #endregion


    MainMenuState state = MainMenuState.Main;

    #region Monobehavior Callbacks
    private void Awake()
    {
        if (_Instance != null)
        {
            Destroy(this.gameObject); //enforce singleton
        }
        else
        {
            _Instance = this;
            //Pre Start initializing here
        }
    }

    private void Start()
    {
        SetNewState(StartupState);
    }
    #endregion

    public void SetNewState(MainMenuState newState)
    {
        if (state != newState)
        {
            EndState(state);
            state = newState;
            BeginState(state);
        }
    }

    //State.OnBegin() functions
    #region Loading States 
    void BeginState(MainMenuState newState)
    {
        switch (newState)
        {
            case MainMenuState.Main:
                LoadMainState();
                return;
            case MainMenuState.LevelSelect:
                LoadLevelSelectState();
                return;
            case MainMenuState.Options: return; //<-- TODO: implement options menu
        }
    }

    void LoadMainState()
    {
        MainMenuGameobject?.SetActive(true);
    }

    void LoadLevelSelectState()
    {
        GameManager.Instance.LoadLevelSelect();
    }

    #endregion

    //State.OnEnd() functions
    #region Unloading States
    void EndState(MainMenuState currState)
    {
        switch (currState)
        {
            case MainMenuState.Main:
                UnloadMainState();
                return;
            case MainMenuState.LevelSelect:
                UnloadLevelSelectState();
                return;
            case MainMenuState.Options: return; //<-- TODO: implement options menu
        }
    }

    void UnloadMainState()
    {
        MainMenuGameobject?.SetActive(false);
    }

    void UnloadLevelSelectState()
    {
        GameManager.Instance.UnloadLevelSelect();
    }
    #endregion

    public enum MainMenuState
    {
        Main,
        LevelSelect,
        Options
        
    }


    #region Button Event Responders
    public void LoadLevel(SceneData levelToLoad)
    {
        SetNewState(MainMenuState.Main);
        GameManager.Instance.LoadLevel(levelToLoad);  
    }

    #endregion
}
