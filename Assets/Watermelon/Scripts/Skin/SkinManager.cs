using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private SkinSO[] skinSOs;

    // [SerializeField] private Skin[] skins;
    //   
    void Awake()
    {
        SkinSO skinSO = GetSkinSO(PlayerPrefs.GetString("Skin", "Fruit") );
        Messenger.Broadcast<SkinSO>(EventKey.ONCHANGESKIN, skinSO);
    }
    public SkinSO GetSkinSO(string name) {
        return System.Array.Find(skinSOs, skinSO => skinSO.GetSkinName() == name);
    }

    void OnEnable()
    {
        Messenger.AddListener<string>(EventKey.SKIN_SELECTED, OnSkinSelected);
    }

    void OnDisable()
    {
        Messenger.RemoveListener<string>(EventKey.SKIN_SELECTED, OnSkinSelected);
    }

    private void OnSkinSelected(string name) {
        SkinSO skinSO = GetSkinSO(name);
        PlayerPrefs.SetString("Skin", name);
        Messenger.Broadcast<SkinSO>(EventKey.ONCHANGESKIN, skinSO);
    }
}
