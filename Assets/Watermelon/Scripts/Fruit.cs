using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] private FruitType fruitType;

    Rigidbody2D fruitRb;

    
    private void Awake()
    {
        fruitRb = GetComponent<Rigidbody2D>();
        //Debug.Log(fruitRb);


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
            Debug.Log(fruit.GetFruitType());
            Debug.Log(this.GetFruitType());
            if (fruit.GetFruitType() != fruitType)
            {
                return;
            } else
            {
                Messenger.Broadcast<Fruit, Fruit>(EventKey.MERGEFRUIT, fruit, this);
            }
        }
    }
}
