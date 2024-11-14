using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPool : ObjectPool
{
    [Header("Data")]
    [SerializeField] private FruitType fruitType;

    public FruitType GetFruitType()
    {
        return fruitType ;
    }
}
