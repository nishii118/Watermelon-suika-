using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : Panel
{
    public void ClickReloadButton()
    {
        PanelManager.Instance.ClosePanel("BlurPanel");
        PanelManager.Instance.ClosePanel("MenuPanel");

        GameManager.Instance.LoadSceneByIndex(0);

        ClickButton();
    }

    public void ClickExitButton()
    {
        PanelManager.Instance.ClosePanel("BlurPanel");
        PanelManager.Instance.ClosePanel("MenuPanel");

        ClickButton();
    }

    public void ClickAchievementButton()
    {
        PanelManager.Instance.ClosePanel("MenuPanel");
        PanelManager.Instance.OpenPanel("AchievementPanel");

        ClickButton();
    }
}
