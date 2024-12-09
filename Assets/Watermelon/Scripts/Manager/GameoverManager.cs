using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    [Header("Element")]
    [SerializeField]private float durationThreshold;
    private float timer;
    private bool timerOn;

    private void Start()
    {
        timer = 0;
        timerOn = false;
    }
    private void Update()
    {
        if (timerOn)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);
            if (timer > durationThreshold)
            {
                OnGameover();
            }
        } 
    }
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.ONTIMERGAMEOVER, StartTimer);
        Messenger.AddListener(EventKey.OFFTIMERGAMEOVER, EndTimer);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONTIMERGAMEOVER, OnGameover);
        Messenger.RemoveListener(EventKey.OFFTIMERGAMEOVER, EndTimer);
    }

    private void OnGameover()
    {
        if (timerOn && timer > durationThreshold)
        {
            Debug.Log("Gameover!!!!!");

            Messenger.Broadcast(EventKey.UPDATEHIGHTSCORE);
        }
    }

    private void StartTimer()
    {

        timer = 0;
        timerOn = true;
    }

    private void EndTimer()
    {
        timer = 0;
        timerOn = false;

    }
}
