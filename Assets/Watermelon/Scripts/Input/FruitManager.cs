using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FruitManager : MonoBehaviour
{

    [Header("Element")]
    [SerializeField] GameObject fruitObject;

    [Header("Setting")]
    [SerializeField] private float spawnPositionY;

    [Header("Debug")]
    [SerializeField] private bool isGizmosEnable;

    void Start()
    {
        
    }

    void Update()
    {
        PlayerInput();
    }

    public void PlayerInput()
    {
        //if (!Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        //{
        //    GenerateFruit();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            GenerateFruit();
        }
    }
    public void GenerateFruit()
    {
        Vector2 spawnPostion = GetTouchPosition();
        spawnPostion.y = spawnPositionY;

        // generate fruit
        GameObject fruit =  ObjectPool.instance.GetPoolObject();
        fruit.transform.position = spawnPostion;
        fruit.transform.rotation = Quaternion.identity;
        fruit.SetActive(true);
    }

    public Vector2 GetTouchPosition()
    {
        Vector2 touchPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        return worldPosition;
    }

#if UNITY_EDITOR

    public void OnDrawGizmos()
    {
        if (!isGizmosEnable) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(new Vector3(-50, spawnPositionY, 0), new Vector3(50, spawnPositionY, 0));
    }
#endif
}
