using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private SceneSO _manager;
    [SerializeField] private SceneSO _sceneToLoad;
    [SerializeField] private SceneEventChannelSO _sceneEventChannelSO;

    private void Start()
    {
        SetupScenes();
    }
    
    void SetupScenes()
    {
        Addressables.LoadSceneAsync(_manager.sceneReference, LoadSceneMode.Additive).Completed += (asyncHandle) =>
        {
            _sceneEventChannelSO.RaiseEvent(_sceneToLoad);
            SceneManager.UnloadSceneAsync(0);
        } ;
    }
}
