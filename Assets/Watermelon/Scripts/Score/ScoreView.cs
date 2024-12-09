
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI highscoreTxt;
    private void OnEnable()
    {
        if (scoreTxt != null)
        {
            OnChangeScore();
        }
        if (highscoreTxt != null)
        {
            OnChangeHighScore();
        }
        Messenger.AddListener(EventKey.OnChangeScore, OnChangeScore);
        Messenger.AddListener(EventKey.OnChangeHighScore, OnChangeHighScore);
    }

    private void OnChangeHighScore()
    {
        if (highscoreTxt != null)
        {
            highscoreTxt.SetText(ScoreManager.Instance.GetHighScore().ToString());
        }
    }
    private void OnChangeScore()
    {
        Debug.Log("score manager: " + ScoreManager.Instance.GetCurrentScore());

        scoreTxt.SetText(ScoreManager.Instance.GetCurrentScore().ToString());
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.OnChangeScore, OnChangeScore);
        Messenger.RemoveListener(EventKey.OnChangeHighScore, OnChangeHighScore);
    }

}
