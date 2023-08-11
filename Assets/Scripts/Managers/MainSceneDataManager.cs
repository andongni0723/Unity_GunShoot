using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneDataManager : Singleton<MainSceneDataManager>
{
    [Header("Data")]
    public CharacterDetails_SO playerChooseCharacterDetails;


    [Header("Setting")]
    [SceneName] public string startSceneName;
    [SceneName] public string gameSceneName;


    #region Scene Load

    public void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(startSceneName);
    }
    
    public void LoadStartScene()
    {
        SceneManager.LoadScene(startSceneName, LoadSceneMode.Additive);
        SceneManager.UnloadSceneAsync(gameSceneName);
    }

    #endregion
}
