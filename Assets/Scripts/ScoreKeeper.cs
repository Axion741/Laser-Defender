using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreKeeper : MonoBehaviour {

    public Text text;
    public int score =0;

    // Use this for initialization
    private void Start()
    {
        Reset();
    }

    public void Score(int points)
    {
        score += points;
        text.text = "Score = " + score.ToString();
    }

    public void Reset()
    {
        score = 0;
        text.text = "Score = " + score.ToString();
    }
}
