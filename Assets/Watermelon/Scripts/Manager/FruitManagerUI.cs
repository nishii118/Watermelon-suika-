using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FruitManagerUI : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] Image nextFruitImage;

    private void OnEnable()
    {
        Messenger.AddListener(EventKey.UPDATENEXTFRUITSPRITEHINT, SetNextFruitImage);
    }

    private void OnDisable()
    {
        Messenger.RemoveListener(EventKey.UPDATENEXTFRUITSPRITEHINT, SetNextFruitImage);
    }
    public void SetNextFruitImage()
    {
        nextFruitImage.sprite = FruitManager.Instance.GetNextFruitSprite();
    }
}
