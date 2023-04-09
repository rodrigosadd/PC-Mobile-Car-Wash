using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.AddressableAssets;
using Unity.VisualScripting;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private SceneEventChannelSO _loadSceneChannel;
    [SerializeField] private SceneEventChannelSO _startTransitionSceneChannel;
    [SerializeField] private SceneEventChannelSO _endTransitionSceneChannel;

    private SceneInstance _previousScene;
    private bool _clearPreviousScene;

    private void OnEnable()
    {
        _loadSceneChannel.OnSceneRequest += CheckTransition;
        _endTransitionSceneChannel.OnSceneRequest += UnloadPreviousScene;
    }

    private void OnDisable()
    {
        _loadSceneChannel.OnSceneRequest -= CheckTransition;     
        _endTransitionSceneChannel.OnSceneRequest -= UnloadPreviousScene;
    }

    void CheckTransition(SceneSO scene)
    {
        if (scene.showTransition)
        {
            _startTransitionSceneChannel.RaiseEvent(scene);
        }
        else
        {
            UnloadPreviousScene(scene);
        }
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
