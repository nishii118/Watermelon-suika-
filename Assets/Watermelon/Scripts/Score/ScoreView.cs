
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI highscoreTxt;
    void Start()
    {
        Debug.Log("score txt: " + scoreTxt);

        if (scoreTxt != null)
        {
            OnChangeScore();
        }
        if (highscoreTxt != null)
        {
            OnChangeHighScore();
        }
    }
    private void OnEnable()
    {
        
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
        // Debug.Log("score mang")
        scoreTxt.SetText(ScoreManager.Instance.GetCurrentScore().ToString());
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.OnChangeScore, OnChangeScore);
        Messenger.RemoveListener(EventKey.OnChangeHighScore, OnChangeHighScore);
    }

}
