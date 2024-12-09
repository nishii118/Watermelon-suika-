using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class ScoreManager : MonoBehaviour
{
    [Header("Element")]
    private int currentScore;
    private int highScore;
    [SerializeField] private TextMeshProUGUI scoreTxt;
    [SerializeField] private TextMeshProUGUI bestcoreTxt;

    private void Start()
    {
        currentScore = 0;
        highScore = 0;
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
        scoreTxt.SetText(currentScore.ToString());

    }

    private void UpdateHighScore()
    {
        highScore = Mathf.Max(currentScore, highScore);
        bestcoreTxt.text = highScore.ToString();

        UpdateHighScore();
    }

    private void UpdateCurrentScore()
    {
        currentScore = 0;
        scoreTxt.text = currentScore.ToString();
    }
}
