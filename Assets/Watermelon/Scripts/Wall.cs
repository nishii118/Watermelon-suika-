using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Transform rightWall;
    [SerializeField] private Transform leftWall;
    // Start is called before the first frame update
    void Start()
    {
        float aspectRatio = (float)Screen.height / Screen.width;

        Camera mainCamera = Camera.main;

        float halfHorizontalSize = mainCamera.orthographicSize / aspectRatio;
        // Debug.Log(halfHorizontalSize);
        rightWall.transform.position = new Vector3(halfHorizontalSize + .1f, 0, 0);
        leftWall.transform.position = -rightWall.transform.position;
    }

    // Update is called once per frame
    
}
