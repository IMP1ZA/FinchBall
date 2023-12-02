using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int score;  
    [SerializeField] private Text scoretext;

     void Update()
    {
        scoretext.text = "Score: " + score;
    }

    public int GetScore(int getscore) 
    {
        score += getscore;
        return score;
    }
}
