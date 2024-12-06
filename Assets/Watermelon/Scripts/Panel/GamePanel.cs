using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : Panel
{
    public void ClickSettingButton()
    {
        Debug.Log("Click setting button");
        //Time.timeScale = 0;
        PanelManager.Instance.OpenPanel("BlurPanel");
        PanelManager.Instance.OpenPanel("MenuPanel");
    }
}
