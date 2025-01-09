
using UnityEngine;

[System.Serializable]
public class Skin : MonoBehaviour
{
    public string skinName;
    // public Sprite sprite;

    public string GetSkinName() {
        return skinName;
    }

    public void OnClickSkin() {
     

        Messenger.Broadcast<string>(EventKey.SKIN_SELECTED, GetSkinName());
    }
}
