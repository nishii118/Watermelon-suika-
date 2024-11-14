using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class FruitManager : Singleton<FruitManager>
{

    [Header("Element")]
    [SerializeField] private LineRenderer lineRenderer;
    private Fruit fruit;
    //private Rigidbody2D fruitRb;
    [SerializeField] private FruitPool[] fruitPools;

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

    private void OnEnable()
    {
        Messenger.AddListener<FruitType, Vector2>(EventKey.SPAWNMERGEFRUIT, MergeProcessCallback);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<FruitType, Vector2>(EventKey.SPAWNMERGEFRUIT, MergeProcessCallback);
    }
    public void PlayerInput()
    {
        

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
        isControlling = true;
    }
    public void MouseDragCallback()
    {
        // set position for spawn falling line 
        SetSpawnFallingLinePosition();

        fruit.SetPositionBeforeSpawn(GetSpawnPosition(GetTouchPosition()));

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
        lineRenderer.SetPosition(1, GetSpawnPosition(GetTouchPosition()) + Vector2.down * 20);
    }
    public void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition(GetTouchPosition());

        // generate fruit
        GameObject fruitObject = fruitPools[Random.Range(0,3)].GetPoolObject();
        fruitObject.SetActive(true);
        fruit = fruitObject.GetComponent<Fruit>();

        fruit.SetActiveFruit(spawnPosition);
        // spawn fruit 
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
    }

    public void DisplaySpawnLine()
    {
        lineRenderer.enabled = true;
        
    }

    public void StartControllTimer()
    {
        Invoke("StopControllTimer", 0.5f);
    }

    public void StopControllTimer()
    {
        canControl = true;
    }

    public void MergeProcessCallback(FruitType mergedFruitType, Vector2 positionMergedFruit)
    {
        foreach(FruitPool fruitPool in fruitPools)
        {
            if(fruitPool.GetFruitType() == mergedFruitType)
            {
                SpawnMergedFruit(fruitPool, positionMergedFruit);
                break;
            }
        }
    }

    public void SpawnMergedFruit(FruitPool fruitPool, Vector2 positionMergedFruit)
    {
        GameObject fruitObject = fruitPool.GetPoolObject();
        fruitObject.SetActive(true);
        fruit = fruitObject.GetComponent<Fruit>();

        fruit.SetActiveFruit(positionMergedFruit);
        fruit.SetRigibody2dDynamic();
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
