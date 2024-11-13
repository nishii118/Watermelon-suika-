using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
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
}
