using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "Scene Reference", menuName = "Scriptable Objects/Scene/Scene Reference")]
public class SceneSO : ScriptableObject
{
    public AssetReference sceneReference;
}
