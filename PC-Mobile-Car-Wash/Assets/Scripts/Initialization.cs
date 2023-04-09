using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class Initialization : MonoBehaviour
{
    [SerializeField] private SceneSO _manager;
    [SerializeField] private SceneSO _sceneToLoad;
    [SerializeField] private SceneEventChannelSO _loadSceneChannel;

    private void Start()
    {
        SetupScenes();
    }
    
    void SetupScenes()
    {
        _sceneToLoad.showTransition = false;

        Addressables.LoadSceneAsync(_manager.sceneReference, LoadSceneMode.Additive).Completed += (asyncHandle) =>
        {
            _loadSceneChannel.RaiseEvent(_sceneToLoad);
            SceneManager.UnloadSceneAsync(0).completed += (teste) =>
            {
                _sceneToLoad.showTransition = true;
            };
        } ;
    }
}
