using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FruitManager : MonoBehaviour
{

    [Header("Element")]
    //[SerializeField] GameObject  fruitObject;
    [SerializeField] private LineRenderer lineRenderer;
    private Fruit fruit;
    private Rigidbody2D fruitRb;

    [Header("Setting")]
    [SerializeField] private float spawnPositionY;
    private bool canControl;
    private bool isControlling;
    [Header("Debug")]
    [SerializeField] private bool isGizmosEnable;

    void Start()
    {
        HideSpawnLine();

        canControl = true;
        isControlling = false;
    }

    void Update()
    {
        if (canControl)
        {
            PlayerInput();
        }
    }

    public void PlayerInput()
    {
        //if (!Touchscreen.current.primaryTouch.press.wasReleasedThisFrame)
        //{
        //    GenerateFruit();
        //}

        if (Input.GetMouseButtonDown(0))
        {
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            if (isControlling)
            {
                MouseDragCallback();

            }
            else
            {
                MouseDownCallback();
            }
        }
        else if (Input.GetMouseButtonUp(0) ) 
        {
            MouseUpCallback();
        }
    }

    public void MouseDownCallback()
    {
        DisplaySpawnLine();
        SetSpawnFallingLinePosition();

        SpawnFruit();
        Debug.Log("Mouse down call back");
        isControlling = true;
        Debug.Log("mouse down, iscontrolling: " + isControlling);
    }
    public void MouseDragCallback()
    {
        // set position for spawn falling line 
        SetSpawnFallingLinePosition();

        fruit.SetPositionBeforeSpawn(GetSpawnPosition(GetTouchPosition()));
        Debug.Log(isControlling);

    }
    public void MouseUpCallback()
    {
        HideSpawnLine();

        // set physics for fruit
        fruit.SetRigibody2dDynamic();

        canControl = false;
        StartControllTimer();
        isControlling = false;

    }

    public void SetSpawnFallingLinePosition()
    {
        lineRenderer.SetPosition(0, GetSpawnPosition(GetTouchPosition()));
        lineRenderer.SetPosition(1, GetSpawnPosition(GetTouchPosition()) + Vector2.down * 15);
    }
    public void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition(GetTouchPosition());

        // generate fruit
        GameObject fruitObject = ObjectPool.instance.GetPoolObject();
        fruitObject.SetActive(true);
        //Debug.Log(fruitObject.name);
        fruit = fruitObject.GetComponent<Fruit>();
        
        fruit.SetActiveFruit(spawnPosition);
        
    }

    public Vector2 GetTouchPosition()
    {
        Vector2 touchPosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(touchPosition);
        return worldPosition;
    }

    public Vector2 GetSpawnPosition(Vector2 getTouchPosition)
    {
        Vector2 spawnPosition = getTouchPosition;
        spawnPosition.y = spawnPositionY;
        return spawnPosition;
    }

    public void HideSpawnLine()
    {
        lineRenderer.enabled = false;
        //Debug.Log("hide spawn line");
    }

    public void DisplaySpawnLine()
    {
        lineRenderer.enabled = true;
        //Debug.Log("display spawn line");
        //Debug.Log(lineRenderer.enabled);
    }

    public void StartControllTimer()
    {
        Invoke("StopControllTimer", 0.5f);
    }

    public void StopControllTimer()
    {
        canControl = true;
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