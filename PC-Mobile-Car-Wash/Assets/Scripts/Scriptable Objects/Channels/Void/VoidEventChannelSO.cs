using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Void Channel", menuName = "Scriptable Objects/Events/Void Channel")]
public class VoidEventChannelSO : ScriptableObject
{
    public UnityAction OnVoidRequest;

    public void RaiseEvent()
    {
        if (OnVoidRequest != null)
        {
            OnVoidRequest.Invoke();
        }
        else
        {
            Debug.Log("Void Event Null!!!");
        }
    }
}
