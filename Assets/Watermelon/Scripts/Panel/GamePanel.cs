using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    public void ClickSettingButton()
    {
        Time.timeScale = 0;
        PanelManager.Instance.OpenPanel("Blur Panel");
        PanelManager.Instance.OpenPanel("Setting Panel");
    }
}
