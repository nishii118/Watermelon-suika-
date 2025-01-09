using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuPanel : Panel
{
    [SerializeField] private TextMeshProUGUI nameSongTxt;

    void Start()
    {
        if (PlayerPrefs.GetInt("Music", 1) == 1) {
            nameSongTxt.text = "## Random";
        } else {
            nameSongTxt.text = "-";
        }
    }

    void OnEnable()
    {
        Messenger.AddListener<string>(EventKey.OnUpdateNameSong, OnUpdateNameSong);
    }
    void OnDisable()
    {
        Messenger.RemoveListener<string>(EventKey.OnUpdateNameSong, OnUpdateNameSong);
    }
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

    public void ClickShopButton() {
        PanelManager.Instance.ClosePanel("MenuPanel");
        PanelManager.Instance.OpenPanel("ShopPanel");

        ClickButton();
    }

    public void OnUpdateNameSong(string songName) {
        
        if (PlayerPrefs.GetInt("Music", 1) == 1) {
            //check israndom mode song 
            if (AudioManager.Instance.GetIsRandom()) {
                nameSongTxt.text = "## Random";

            } else {
                nameSongTxt.text = songName;
            }
            // nameSongTxt.text = "## Random";
        } else {
            nameSongTxt.text = "-";
        }
        // nameSongTxt.text = songName;
    }

    public void NextSong() {
        Messenger.Broadcast(EventKey.OnUpdateNextSong);

    }

    public void PreSong() {
        Messenger.Broadcast(EventKey.OnUpdatePreSong);
    }
}
