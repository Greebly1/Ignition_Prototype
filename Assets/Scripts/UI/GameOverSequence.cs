using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// handles game over screen
/// </summary>
public class GameOverSequence : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreHolder;
    [SerializeField] TextMeshProUGUI RankHolder;
    [SerializeField] TextMeshProUGUI WinLoseHolder;
    [SerializeField] TextMeshProUGUI TimeHolder;


    LevelManager currentLevel;
    private void Start()
    {
        //Debug.Log("I am gere");
        currentLevel = GameManager.Instance.CurrentLevel;

        SetResultData();
    }

    void SetResultData()
    {
        if (currentLevel == null)
        {
            return; //early out
        }
        int score = (currentLevel.CalculateScore());
        ScoreHolder.text = (score * 100).ToString();
        RankHolder.text = currentLevel.LevelScoreRanker.EvaluateScore(score).ToString();
        TimeHolder.text = currentLevel.TimeInLevel.ToString();
        if (currentLevel.HasLost())
        {
            WinLoseHolder.text = "You Failed!";  
        } else
        {
            WinLoseHolder.text = "You Won!";
        }
    }

    //-----Button Event Handlers-----
    public void OnMainMenu()
    {
        GameManager.Instance.LoadMainMenu();
    }

    public void OnRetry()
    {
        
        currentLevel.RestartLevel();
        GameManager.Instance.UnloadGameOverScreen();
    }
}
