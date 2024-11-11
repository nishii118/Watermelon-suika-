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
    [SerializeField] private LineRenderer lineRenderer;

    [Header("Debug")]
    [SerializeField] private bool isGizmosEnable;

    void Start()
    {
        HideSpawnLine();
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
            MouseDownCallback();
        }
        else if (Input.GetMouseButton(0))
        {
            MouseCallback();
        }
        else if (Input.GetMouseButtonUp(0)) 
        {
            MouseUpCallback();
        }
    }

    public void MouseDownCallback()
    {
        DisplaySpawnLine();
    }
    public void MouseCallback()
    {
        lineRenderer.SetPosition(0, GetSpawnPosition(GetTouchPosition()));
        lineRenderer.SetPosition(1, GetSpawnPosition(GetTouchPosition()) + Vector2.down * 15);
    }
    public void MouseUpCallback()
    {
        HideSpawnLine();
    }
    public void GenerateFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition(GetTouchPosition());

        // generate fruit
        GameObject fruit =  ObjectPool.instance.GetPoolObject();
        fruit.transform.position = spawnPosition;
        fruit.transform.rotation = Quaternion.identity;
        fruit.SetActive(true);
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
        Debug.Log("hide spawn line");
    }

    public void DisplaySpawnLine()
    {
        lineRenderer.enabled = true;
        Debug.Log("display spawn line");
        Debug.Log(lineRenderer.enabled);
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
