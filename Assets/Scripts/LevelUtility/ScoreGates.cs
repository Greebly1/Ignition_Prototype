using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int[] gates
    {
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
