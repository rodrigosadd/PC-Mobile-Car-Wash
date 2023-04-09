using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Scene Channel", menuName = "Scriptable Objects/Events/Scene Channel")]
public class SceneEventChannelSO : ScriptableObject
{
    public UnityAction<SceneSO> OnSceneRequest;

    public void RaiseEvent(SceneSO scene)
    {
        if (OnSceneRequest != null)
        {
            OnSceneRequest.Invoke(scene);
        }
        else
        {
            Debug.Log("Scene Event Null!!!");
        }
    }
}
