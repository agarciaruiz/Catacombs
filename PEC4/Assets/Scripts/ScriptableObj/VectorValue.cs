using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    [Header("Value running in game")]
    public Vector2 initValue;

    [Header("Value by default at start")]
    public Vector2 defaultValue;

    public void OnAfterDeserialize()
    {
        initValue = defaultValue;
    }

    public void OnBeforeSerialize() { }
}
