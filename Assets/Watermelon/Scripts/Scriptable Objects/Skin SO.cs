using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/SkinSO", order = 1)]
public class SkinSO : ScriptableObject
{
    [SerializeField] private FruitPool[] fruitPools;
    [SerializeField] private string skinName;
    public FruitPool[] GetFruitPools()
    {
        return fruitPools;
    }
}
