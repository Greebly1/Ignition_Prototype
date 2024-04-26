using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Responds to player input, sendMessages()
    /// </summary>
    #region PlayerInput Responders
    public void OnPressAny()
    {
        //load the main menu screen
        GameManager.Instance.LoadMainMenu();
    }
    #endregion
}
