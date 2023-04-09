using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionLoader : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private float _transitionTime;
    [SerializeField] private SceneEventChannelSO _startTransitionSceneChannel;
    [SerializeField] private SceneEventChannelSO _endTransitionSceneChannel;

    private void OnEnable()
    {
        _startTransitionSceneChannel.OnSceneRequest += SetTransition;
    }

    private void OnDisable()
    {
        _startTransitionSceneChannel.OnSceneRequest -= SetTransition;        
    }

    void SetTransition(SceneSO scene)
    {
        StartCoroutine(Transition(scene));
    }

    IEnumerator Transition(SceneSO scene)
    {
        _anim.SetTrigger("Start");

        yield return new WaitForSeconds(_transitionTime);

        _endTransitionSceneChannel.RaiseEvent(scene);
    }
}
