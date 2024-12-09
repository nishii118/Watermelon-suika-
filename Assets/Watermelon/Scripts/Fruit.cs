using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FruitType fruitType;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [Header("Element")]
    Rigidbody2D fruitRb;
    int triggerGameoverLineCount;
    [SerializeField] private int scoreFruit;
    
    private void Awake()
    {
        fruitRb = GetComponent<Rigidbody2D>();
        //Debug.Log(fruitRb);

        triggerGameoverLineCount = 0;
    }
  
    public void ResetTriggerCount()
    {
        triggerGameoverLineCount = 0;
    }
    public void SetActiveFruit(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
        transform.rotation = Quaternion.identity;
        fruitRb.bodyType = RigidbodyType2D.Kinematic;
       
    }

    public void SetRigibody2dDynamic()
    {
        fruitRb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetPositionBeforeSpawn(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    public FruitType GetFruitType()
    {
        return fruitType;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Fruit fruit))
        {
            
            if (fruit.GetFruitType() != fruitType)
            {
                return;
            } else
            {
                Messenger.Broadcast<Fruit, Fruit>(EventKey.MERGEFRUIT, fruit, this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Gameover Line"))
        {
            triggerGameoverLineCount++;
            if (triggerGameoverLineCount > 1)
            {
                Messenger.Broadcast(EventKey.INCREASEFRUITCOUNT);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Gameover Line"))
        {
            if (triggerGameoverLineCount > 1)
            {
                Messenger.Broadcast(EventKey.DECREASEFRUITCOUNT);
            }
        }
    }
    public SpriteRenderer GetSpriteRenderer()
    {
        return spriteRenderer;
    }
    public int GetScoreFruit()
    {
        return scoreFruit;
    }
}
