using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverManager : MonoBehaviour
{
    private void OnEnable()
    {
        Messenger.AddListener(EventKey.ONGAMEOVER, OnGameover);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.ONGAMEOVER, OnGameover);
    }

    private void OnGameover()
    {
        Debug.Log("Gameover!!!!!");
    }
}
