using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO _sceneEventChannel;

    private SceneInstance _previousScene;
    private bool _clearPreviousScene;

    private void OnEnable()
    {
        _sceneEventChannel.OnSceneRequest += UnloadPreviousScene;
    }

    private void OnDisable()
    {
        _sceneEventChannel.OnSceneRequest -= UnloadPreviousScene;        
    }

    void UnloadPreviousScene(SceneSO scene)
    {
        if (_clearPreviousScene)
        {
            Addressables.UnloadSceneAsync(_previousScene);
        }

        LoadScene(scene);
    }

    void LoadScene(SceneSO scene)
    {
        Addressables.LoadSceneAsync(scene.sceneReference, LoadSceneMode.Additive).Completed += (asyncHandle) =>
        {
            _clearPreviousScene = true;
            _previousScene = asyncHandle.Result;
        };
    }
}
