using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FruitManager : MonoBehaviour
{

    [Header("Element")]
    [SerializeField] GameObject fruitObject;

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
        Vector2 touchPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        Debug.Log(worldPosition);
        GameObject fruit =  ObjectPool.instance.GetPoolObject();
        fruit.transform.position = worldPosition;
        fruit.transform.rotation = Quaternion.identity;
        fruit.SetActive(true);
    }
}
