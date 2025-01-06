
using UnityEngine.SceneManagement;
using UnityEngine;


public class GameManager : Singleton<GameManager>
{
    private bool isGameover = false;

    private void OnEnable()
    {
        //Messenger.AddListener
    }

    private void SetIsGameover(bool state)
    {
        isGameover = state;
    }
    public bool GetIsGameOver()
    {
        return isGameover;
    }
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log(currentScene.name);
        SceneManager.LoadScene(currentScene.name);
    }
}
