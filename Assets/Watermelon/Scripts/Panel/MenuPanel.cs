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
    }
}
