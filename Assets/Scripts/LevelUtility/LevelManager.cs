using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [HideInInspector] public CircularArray<List<ApplyableData>> TimeCapsule = new CircularArray<List<ApplyableData>>();
    [HideInInspector] public List<TimeTracker> timeTrackers = new List<TimeTracker>();
    public float timeSavePadding = 0.05f; //how long between each time save
    float timeSaveTimer = 0.05f;

    [HideInInspector] public GameObject Player;

    [SerializeField] int MaxTimeLimit = 1000;
    [SerializeField] public ScoreGates LevelScoreRanker;
    bool _playerWon = false;
    public bool playerwon
    {
        get { return _playerWon; }
        private set { _playerWon = value; }
    }

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

    public float TimeInLevel
    {
        get
        {
            return MaxTimeLimit - timeRemaining;
        }
    }

    #region monobehavior callbacks
    private void Awake()
    {
        timeRemaining = MaxTimeLimit;
        
    }
    private void Start()
    {
        GameManager.Instance.CurrentLevel = this;
        //instead of finding objects by type, we have them add themself to the list
        //timeTrackers = FindObjectsByType<TimeTracker>(FindObjectsSortMode.None).ToList(); //get a list of everything that needs its data tracked
    }

    bool lostAlready = false;

    private void Update()
    {
        timeRemaining -= Time.deltaTime; //TODO, make this work with time reversal
        if (HasLost() && !lostAlready) { 
            lostAlready = true;
            GameOver(); 
        } 

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            TogglePause();
        }

        if (!GameManager.Instance.IsPaused)
        {
            DataTimerTick(Time.deltaTime);
        } else
        { //THE GAME IS PAUSED
            //this is where the time manipulation occurs
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryToMoveTime(movingForwards: true);
            } else if (Input.GetKeyDown(KeyCode.Q))
            {
                TryToMoveTime(movingForwards: false);
            }

        }
    }

    private void OnDestroy()
    {
        GameManager.Instance.CurrentLevel = null;
    }
    #endregion

    #region Level Flow Logic
    public void TogglePause()
    {
        GameManager.Instance.IsPaused = !GameManager.Instance.IsPaused;
        if (GameManager.Instance.IsPaused)
        { //PAUSED
          
          Time.timeScale = 0f;
        } else
        { //Not Paused
            Time.timeScale = 1.0f;
            TimeCapsule.ClampArrayToCurrentIndex();
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
        GameManager.Instance.GameOver(!HasLost());
    }
    #endregion

    #region Time Data Handling
    void DataTimerTick(float deltaTime)
    {
       timeSaveTimer -= deltaTime;
        if (timeSaveTimer <= 0)
        {
            timeSaveTimer = timeSavePadding;
            SaveTimeData();
        }
    }

    /// <summary>
    /// Purpose: insert time data from this frame into the time capsule
    /// </summary>
    void SaveTimeData()
    {
        TimeCapsule.Insert(CollectTimeData());
    }

    /// <summary>
    /// Purpose: collect time data from all of the TimeTrackers
    /// </summary>
    /// <returns></returns>
    List<ApplyableData> CollectTimeData()
    {
        List<ApplyableData> dataPoll = new List<ApplyableData>();
        foreach(TimeTracker dataGenerator in timeTrackers) //time trackers are ApplyableData factories
        {
            dataPoll.Add(dataGenerator.GenerateTimeData());
        }
        return dataPoll;
    }


    void TryToMoveTime(bool movingForwards = false)
    {
        if (movingForwards)
        {
            TimeCapsule.currIndex += 1;
            ApplyTimeCapsule();
        } else
        { //moving time backwards
            TimeCapsule.currIndex -= 1;
            ApplyTimeCapsule();
        }
    }

    void ApplyTimeCapsule() //take the saved data and apply it
    {
        foreach (ApplyableData timeFrame in TimeCapsule.DataAtCurrIndex())
        {
            timeFrame.ApplyData();
        }
    }
    #endregion
}

//Applyable data is meant to be data, encapsulated with a reference to an object that can be a recipient of that data
public interface ApplyableData
{
    public void ApplyData();
}


/* this is pretty neat but i am not doing it right now
public class TimeTractedGameobject : ApplyableData
{
    GameObject gameObject;

    TimeTrackedTransform TransformData;

    public void ApplyData()
    {
        TransformData.ApplyData();
    }
} */

public class TimeTrackedTransform : ApplyableData
{
    Transform dataTarget = null; //reference to a class that will receive the data

    Vector3 position = Vector3.zero; //data to receive
    Quaternion rotation = Quaternion.identity;
    //we dont need to keep track of scale

    public void ApplyData()
    {
        dataTarget.position = this.position;
        dataTarget.rotation = this.rotation;
    }

    public TimeTrackedTransform(Transform receiver, Vector3 currPosition, Quaternion currRotation)
    {
        position = currPosition;
        rotation = currRotation;
        dataTarget = receiver;
    }
}

public class CustomTimeData : ApplyableData
{
    public List<ApplyableData> data = new List<ApplyableData>();

    public void ApplyData()
    {
        foreach (ApplyableData currData in data)
        {
            currData.ApplyData();
        }
    }

    public CustomTimeData(List<ApplyableData> data)
    {
        this.data = data;
    }
}
