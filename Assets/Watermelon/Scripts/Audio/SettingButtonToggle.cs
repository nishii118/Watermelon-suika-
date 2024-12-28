using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingButtonToggle : MonoBehaviour
{
    [SerializeField] string prefKey;
    [SerializeField] Sprite spriteOn, spriteOff;
    [SerializeField] Image image;

    void Start()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        if (PlayerPrefs.GetInt(prefKey, 1) == 1)
        {
            image.sprite = spriteOn;
        }
        else
        {
            image.sprite = spriteOff;
        }
    }

    public void Toggle() {
        Messenger.Broadcast<string>(EventKey.OnToggleSound, prefKey);
        UpdateState();
    }

    // private void OnValidate()
    // {
    //     image = GetComponent<Image>();
    // }
}

