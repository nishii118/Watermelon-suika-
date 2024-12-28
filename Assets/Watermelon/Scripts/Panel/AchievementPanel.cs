using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementPanel : Panel
{
    public void ClickExitButton()
    {
        PanelManager.Instance.ClosePanel("BlurPanel");
        PanelManager.Instance.ClosePanel("AchievementPanel");

        ClickButton();
    }

    public void ClickSettingButton()
    {
        PanelManager.Instance.ClosePanel("AchievementPanel");
        PanelManager.Instance.OpenPanel("MenuPanel");

        ClickButton();
    }   
}
