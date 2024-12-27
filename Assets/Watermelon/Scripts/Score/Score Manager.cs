using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ScoreManager : Singleton<ScoreManager>
{
    [Header("Element")]
    private int currentScore;
    private int highScore;
    //[SerializeField] private TextMeshProUGUI scoreTxt;
    //[SerializeField] private TextMeshProUGUI bestcoreTxt;

    private void Start()
    {
        currentScore = 0;
        highScore = 0;
        Debug.Log("score manager in scoremanager script: " + this);
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(EventKey.ADDSCORE, AddScore);
        Messenger.AddListener(EventKey.UPDATEHIGHTSCORE, UpdateHighScore);

    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int>(EventKey.ADDSCORE, AddScore);
        Messenger.RemoveListener(EventKey.UPDATEHIGHTSCORE, UpdateHighScore);
    }

    private void AddScore(int score)
    {
        currentScore += score;
        Messenger.Broadcast(EventKey.OnChangeScore);

    }

    private void UpdateHighScore()
    {
        highScore = Mathf.Max(currentScore, highScore);
        Messenger.Broadcast(EventKey.OnChangeHighScore);

        //UpdateCurrentScore();
    }

    private void UpdateCurrentScore()
    {
        currentScore = 0;

        Messenger.Broadcast(EventKey.OnChangeScore);
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public int GetHighScore()
    {
        return highScore;
    }
}
