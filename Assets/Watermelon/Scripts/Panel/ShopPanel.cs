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
        GameObject[] skins = Resources.LoadAll<GameObject>("Skins");
        foreach(GameObject skin in skins) {
            GameObject skinObj = Instantiate(skin, content);
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
