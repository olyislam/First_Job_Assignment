using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScore : MonoBehaviour
{
#pragma warning disable 649
    [SerializeField] private Text Scoretex;
#pragma warning restore 649
    [SerializeField] private int Score = 0;
    void Start()
    {
        AddScore(0);
    }


    public void AddScore(int score)
    {
        Score += this.Score <= 0 && score < 0 ? 0 : score;
        Scoretex.text = "Score : " + Score;
    }
}
