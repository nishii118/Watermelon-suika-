using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MergeManager : Singleton<MergeManager>
{
    private Fruit lastSenderFruit;
    private void OnEnable()
    {
        Messenger.AddListener<Fruit, Fruit>(EventKey.MERGEFRUIT, CollisionBetweenFruitsCallback);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<Fruit, Fruit>(EventKey.MERGEFRUIT, CollisionBetweenFruitsCallback);
    }
    private void CollisionBetweenFruitsCallback(Fruit sender, Fruit otherFruit)
    {
        if (lastSenderFruit != null)
        {
            return;
        }

        lastSenderFruit = sender;

        ProcessMergeFruit(sender, otherFruit);
        Debug.Log("Collision detected by: " + sender.name);
    }
    
    private void ProcessMergeFruit(Fruit sender, Fruit otherFruit)
    {
        FruitType mergedFruitType = sender.GetFruitType() + 1;
        Vector2 positionMergedFruit = (sender.transform.position + otherFruit.transform.position) / 2;
        sender.gameObject.SetActive(false);
        otherFruit.gameObject.SetActive(false);

        StartCoroutine(ResetLastSenderFruit());

        Messenger.Broadcast<FruitType, Vector2>(EventKey.SPAWNMERGEFRUIT, mergedFruitType, positionMergedFruit);
    }

    IEnumerator ResetLastSenderFruit()
    {
        yield return new WaitForEndOfFrame();
        lastSenderFruit = null;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
