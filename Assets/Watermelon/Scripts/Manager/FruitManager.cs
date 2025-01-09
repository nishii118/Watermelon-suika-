
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;



public class FruitManager : Singleton<FruitManager>
{

    [Header("Element")]
    [SerializeField] private LineRenderer lineRenderer;
    private Fruit fruit;
    //private Rigidbody2D fruitRb;
    [SerializeField] List<FruitPool> fruitPools = new List<FruitPool>();
    // [SerializeField] private SkinSO[] skinSOs;
    [SerializeField] private SkinSO currentSkinSO;

    [SerializeField] private Transform fruitPoolsContainer;

    [Header("Setting")]
    [SerializeField] private float spawnPositionY;
    private bool canControl;
    private bool isControlling;

    [Header("Next Fruit Setting")]
    private int nextFruitIndex;

    [Header("Debug")]
    [SerializeField] private bool isGizmosEnable;

    // private override void Awake()
    // {
    //     InitializeFruitPools();
    // }
    private void InitializeFruitPools()
    {
        if (currentSkinSO == null) return;
        // fruitPools = currentSkinSO.GetFruitPools();

        foreach (FruitPool fruitPool in fruitPools)
        {
            if (fruitPool != null && fruitPool.gameObject != null)
            {
                Destroy(fruitPool.gameObject);
            }
        }
        fruitPools.Clear();
        Debug.Log(fruitPools);
        foreach (FruitPool fruitPool in currentSkinSO.GetFruitPools())
        {
            if (fruitPool != null)
            {
                GameObject fruitPoolInstance = Instantiate(fruitPool.gameObject, fruitPoolsContainer);
                FruitPool fruitPoolScript = fruitPoolInstance.GetComponent<FruitPool>();
                fruitPools.Add(fruitPoolScript);
            }
        }
        // Debug.Log(fruitPools);
    }
    void Start()
    {
        InitializeFruitPools();
        // Debug.Log("init fruit pools");
        // HideSpawnLine();

        canControl = true;
        isControlling = false;

        nextFruitIndex = Random.Range(0, 3);
        InitGameDefault();
        // Debug.Log("init game default");
        //Messenger.Broadcast(EventKey.UPDATENEXTFRUITSPRITEHINT);
    }

    private void InitGameDefault()
    {
        DisplaySpawnLine();
        // SetSpawnFallingLinePosition();
        SetSpawnFallingLinePositionInit();
        SpawnFruitDefault();
    }

    private void InitGame()
    {
        DisplaySpawnLine();
        SetSpawnFallingLinePosition();
        // SetSpawnFallingLinePositionInit();
        SpawnFruit();
    }
    void Update()
    {
        if (canControl && !IsPointerOverUIObject())
        {
            PlayerInput();
        }
    }

    private void OnEnable()
    {
        Messenger.AddListener<FruitType, Vector2>(EventKey.SPAWNMERGEFRUIT, MergeProcessCallback);
        Messenger.AddListener(EventKey.StopPlaying, StopPlaying);
        Messenger.AddListener(EventKey.ResumePlaying, ResumePlaying);
        Messenger.AddListener<SkinSO>(EventKey.ONCHANGESKIN, OnChangeSkin);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener<FruitType, Vector2>(EventKey.SPAWNMERGEFRUIT, MergeProcessCallback);
        Messenger.RemoveListener(EventKey.StopPlaying, StopPlaying);
        Messenger.RemoveListener(EventKey.ResumePlaying, ResumePlaying);
        Messenger.RemoveListener<SkinSO>(EventKey.ONCHANGESKIN, OnChangeSkin);
    }
    public void PlayerInput()
    {
        if (IsPointerOverUIObject())
        {
            // Debug.Log("over ui object");
            return; // Bỏ qua xử lý logic nếu đang click vào UI
        }

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
        else if (Input.GetMouseButtonUp(0))
        {
            MouseUpCallback();
        }
    }
    private bool IsPointerOverUIObject()
    {
        // Kiểm tra nếu con trỏ chuột ở trên UI
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
    public void MouseDownCallback()
    {
        // DisplaySpawnLine();
        // SetSpawnFallingLinePosition();

        // SpawnFruit();
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

        Messenger.Broadcast<string>(EventKey.DROPFRUITSOUND, "Drop Fruit");

        canControl = false;
        StartControllTimer();
        // StartCoroutine(WaitForFruitToStop());
        isControlling = false;
        // HideSpawnLine();
        //sound 
        // Messenger.Broadcast<string>(EventKey.DROPFRUITSOUND, "Drop Fruit");
    }

    public void SetSpawnFallingLinePosition()
    {
        lineRenderer.SetPosition(0, GetSpawnPosition(GetTouchPosition()));
        lineRenderer.SetPosition(1, GetSpawnPosition(GetTouchPosition()) + Vector2.down * 20);
    }

    private void SetSpawnFallingLinePositionInit()
    {
        Vector2 linePositionDefault = new Vector2(0, spawnPositionY);
        lineRenderer.SetPosition(0, linePositionDefault);
        lineRenderer.SetPosition(1, linePositionDefault + Vector2.down * 20);
    }
    public void SpawnFruitDefault()
    {
        Vector2 spawnPosition = new Vector2(0, spawnPositionY);

        // generate fruit

        GameObject fruitObject = fruitPools[nextFruitIndex].GetPoolObject();
        fruitObject.SetActive(true);
        fruit = fruitObject.GetComponent<Fruit>();

        fruit.SetActiveFruit(spawnPosition);
        // spawn fruit 

        // gen new index for new fruit
        nextFruitIndex = Random.Range(0, 3);
        Messenger.Broadcast(EventKey.UPDATENEXTFRUITSPRITEHINT);


    }

    public void SpawnFruit()
    {
        Vector2 spawnPosition = GetSpawnPosition(GetTouchPosition());

        // generate fruit
        GameObject fruitObject = fruitPools[nextFruitIndex].GetPoolObject();
        fruitObject.SetActive(true);
        fruit = fruitObject.GetComponent<Fruit>();

        fruit.SetActiveFruit(spawnPosition);
        // spawn fruit 

        // gen new index for new fruit
        nextFruitIndex = Random.Range(0, 3);
        Messenger.Broadcast(EventKey.UPDATENEXTFRUITSPRITEHINT);
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
        InitGame();

    }

    private IEnumerator WaitForFruitToStop()
    {
        // Lấy Rigidbody2D của fruit hiện tại
        while (fruit.CheckVelocity()) // Đợi đến khi fruit gần như đứng yên
        {
            yield return null; // Chờ 1 frame
        }

        InitGame(); // Khởi tạo fruit mới
        canControl = true;
    }
    public void MergeProcessCallback(FruitType mergedFruitType, Vector2 positionMergedFruit)
    {
        foreach (FruitPool fruitPool in fruitPools)
        {
            if (fruitPool.GetFruitType() == mergedFruitType)
            {
                SpawnMergedFruit(fruitPool, positionMergedFruit);
                break;
            }
        }

        // sound 
        Messenger.Broadcast<string>(EventKey.GROWPLANT, "Grow Plant");
    }

    public void SpawnMergedFruit(FruitPool fruitPool, Vector2 positionMergedFruit)
    {
        GameObject fruitObject = fruitPool.GetPoolObject();
        fruitObject.SetActive(true);
        Fruit newFruit = fruitObject.GetComponent<Fruit>();

        newFruit.SetActiveFruit(positionMergedFruit);
        newFruit.SetRigibody2dDynamic();
    }

    public Sprite GetNextFruitSprite()
    {
        GameObject fruitObject = fruitPools[nextFruitIndex].GetPoolObject();
        Fruit nextFruit = fruitObject.GetComponent<Fruit>();
        return nextFruit.GetSpriteRenderer().sprite;


    }

    private void StopPlaying()
    {
        canControl = false;
    }

    private void ResumePlaying()
    {
        canControl = true;
    }

    private void OnChangeSkin(SkinSO skinSO)
    {
        currentSkinSO = skinSO;
        Debug.Log("Change skin and current skin: " + currentSkinSO.name);
        InitializeFruitPools();

        canControl = true;
        isControlling = false;

        nextFruitIndex = Random.Range(0, 3);
        InitGameDefault();

        Messenger.Broadcast(EventKey.ONRESETCURRENTSCORE);
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
