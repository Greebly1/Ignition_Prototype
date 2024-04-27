using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages game flow within a level
/// contains a timer, and keeps score
/// 
/// Also manages pausing, and reversing time
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] int MaxTimeLimit = 1000;
    [SerializeField] ScoreGates LevelScoreRanker;

    public UltEvent<int> remainingTimeChanged;
    float _timeRemaining = 1000f;
    public float timeRemaining
    {
        get { return _timeRemaining; }
        set { 
            if (_timeRemaining != value)
            {
                remainingTimeChanged.Invoke((int)value);
            }
            _timeRemaining = value; 
        }
    }

    #region monobehavior callbacks
    private void Awake()
    {
        timeRemaining = MaxTimeLimit;
        GameManager.Instance.CurrentLevel = this;
    }
    private void Update()
    {
        timeRemaining -= Time.deltaTime; //TODO, make this work with time reversal
        if (HasLost()) { GameOver(); } 

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevel = null;
    }
    #endregion

    public void TogglePause()
    {
        GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;
        if (GameManager.Instance.IsPaused)
        { //PAUSED
          
          Time.timeScale = 0f;
        } else
        { //Not Paused
            Time.timeScale = 1.0f;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Right now haslost only checks the time, but in the future we might add more lost conditions
    public bool HasLost()
    {
        return timeRemaining <= 0;
    }

    public int CalculateScore()
    {
        return (int)timeRemaining;
    }

    public void GameOver()
    {
        Debug.Log("gameover");
        Debug.Log(LevelScoreRanker.EvaluateScore(CalculateScore()).ToString() + "-Rank");
    }
}
