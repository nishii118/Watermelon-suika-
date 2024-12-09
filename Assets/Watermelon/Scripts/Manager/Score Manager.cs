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

    private void Start()
    {
        currentScore = 0;
        highScore = 0;
    }
    private void OnEnable()
    {
        Messenger.AddListener<int>(EventKey.ADDSCORE, AddScore);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<int>(EventKey.ADDSCORE, AddScore);   
    }

    private void AddScore(int score)
    {
        currentScore += score;
        scoreTxt.SetText(currentScore.ToString());
    }
}
