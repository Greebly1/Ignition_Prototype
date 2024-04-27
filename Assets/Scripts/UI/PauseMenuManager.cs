using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    Coroutine ContinueCoroutine = null;

    #region UI Button Event Observers
    public void OnContinue()
    {
        ContinueCoroutine = StartCoroutine("UnpauseDelay");
    }

    public void OnMainMenu()
    {
        if (GameManager.Instance.CurrentLevel != null)
        {
            GameManager.Instance.CurrentLevel.TogglePause();
        }
        else
        {
            //do nothing
        }
        GameManager.Instance.LoadMainMenu();
    }

    public void OnRestart()
    {
        if (GameManager.Instance.CurrentLevel != null)
        {
            GameManager.Instance.CurrentLevel.TogglePause();
            GameManager.Instance.CurrentLevel.RestartLevel();
        }
        else
        {
            //do nothing
        }
    }

    #endregion

    IEnumerator UnpauseDelay()
    {
        float unPauseDelay = 0.1f; //we add a delay to unpausing, because we dont want the mouse click on the button to also immediately fire a rocket when the game resumes
        float unPauseTimer = 0.1f;
        while (unPauseTimer > 0)
        {
            unPauseTimer--;
            yield return null;
        }

        UnPause();
    }

    void UnPause()
    {
        if (GameManager.Instance.CurrentLevel != null)
        {
            GameManager.Instance.CurrentLevel.TogglePause();
        }
        else
        {
            GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;
        }
    }
}
