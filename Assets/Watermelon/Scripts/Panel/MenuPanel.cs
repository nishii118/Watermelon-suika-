using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
   public void ClickReloadButton()
    {
        PanelManager.Instance.ClosePanel("Blur Panel");
        PanelManager.Instance.ClosePanel("Menu Panel");
    }
}
