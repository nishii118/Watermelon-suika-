using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameoverLineManager : MonoBehaviour
{
    private int fruitCountOnLine = 0;

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.INCREASEFRUITCOUNT, IncreaseFruitCount);
        Messenger.AddListener(EventKey.DECREASEFRUITCOUNT, DecreaseFruitCount);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.INCREASEFRUITCOUNT, IncreaseFruitCount);
        Messenger.RemoveListener(EventKey.DECREASEFRUITCOUNT, DecreaseFruitCount);
    }
    public void IncreaseFruitCount()
    {
        fruitCountOnLine++;
        Debug.Log("fruit count was increased: " + fruitCountOnLine);
        if (fruitCountOnLine >= 1)
        {
            Messenger.Broadcast(EventKey.ONTIMERGAMEOVER);
        }
    }

    public void DecreaseFruitCount()
    {
        fruitCountOnLine--;
        Debug.Log("fruit count: " + fruitCountOnLine);
        if (fruitCountOnLine == 0)
        {
            Messenger.Broadcast(EventKey.OFFTIMERGAMEOVER);
        }
    }

}
