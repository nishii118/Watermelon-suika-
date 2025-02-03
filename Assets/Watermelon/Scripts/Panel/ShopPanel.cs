using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : Panel
{

    [SerializeField] private Transform content;
    // Start is called before the first frame update
    void Start()
    {
        GenerateSkin();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateSkin() {
        Skin[] skins = Resources.LoadAll<Skin>("Skins");
        foreach(Skin skin in skins) {
            Skin skinObj = Instantiate(skin, content);
            // skinObj.transform.SetParent(transform);
            // skinObj.transform.localScale = Vector3.one;
        }
    }


    public void ClickExitButton()
    {
        PanelManager.Instance.ClosePanel("BlurPanel");
        PanelManager.Instance.ClosePanel("ShopPanel");

        ClickButton();
    }

    public void OnClickAchivementButton() {
        PanelManager.Instance.ClosePanel("ShopPanel");
        PanelManager.Instance.OpenPanel("AchievementPanel");

        ClickButton();
    }


    public void OnClickSettingButton() {
        PanelManager.Instance.ClosePanel("ShopPanel");
        PanelManager.Instance.OpenPanel("MenuPanel");

        ClickButton();
    }
}
