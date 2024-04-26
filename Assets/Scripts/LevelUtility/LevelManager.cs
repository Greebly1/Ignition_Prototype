using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

/// <summary>
/// Manages game flow within a level
/// contains a timer, and keeps score
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

    private void Awake()
    {
        timeRemaining = MaxTimeLimit;
    }
    private void Update()
    {
        timeRemaining -= Time.deltaTime; //TODO, make this work with time reversal
        if (HasLost()) { GameOver(); } 
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
        Debug.Log(RankString(LevelScoreRanker.EvaluateScore(CalculateScore())) + "Rank");
    }


    public string RankString(Rank rank)
    {
        switch (rank)
        {
            case Rank.S: return "S";
            case Rank.A: return "A";
            case Rank.B: return "B";
            case Rank.C: return "C";
            case Rank.D: return "D";
            case Rank.E: return "E";
            case Rank.F: return "F";
            default: return "F";
        }
    }
}

[System.Serializable]
public class ScoreGates
{
    //object representing what scores give what rankings  
    public int S = 700;   
    public int A = 600;   
    public int B = 500;   
    public int C = 400;
    public int D = 300;
    public int E = 200;
    public int F = 100;

    //Some shorthand getters
    public Dictionary<int, Rank> getRank
    {
        get
        {
            return new Dictionary<int, Rank> //this is just so I can retrieve a rank enum from the above int vars
            {
                { F, Rank.F },
                { E, Rank.E },
                { D, Rank.D },
                { C, Rank.C },
                { B, Rank.B },
                { A, Rank.A },
                { S, Rank.S }
            };
        }
    }
    public int[] gates {
        get
        {
            int[] ints = { F, E, D, C, B, A, S };
            return ints;
        }
    }

    public Rank EvaluateScore(int score)
    {
        //start from S rank and go back to F rank
        for (int index = gates.Length - 1; index >= 0; index--)
        {
            if (score >= gates[index]) { return getRank[gates[index]]; } //if the score is greater than it return that rank
        }

        return Rank.F; //the score is not even greater than F rank
    }
}

public enum Rank
{
    S,
    A,
    B,
    C,
    D,
    E,
    F
}
