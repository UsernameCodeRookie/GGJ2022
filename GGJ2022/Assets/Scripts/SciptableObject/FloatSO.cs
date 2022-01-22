using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Float Variable", menuName = "Variables/Float", order = 2)]
public class FloatSO : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public float Value;

    public void SetValue(float value)
    {
        Value = value;
    }

    public void SetValue(FloatSO value)
    {
        Value = value.Value;
    }

    public void ApplyChange(float amount)
    {
        Value += amount;
    }

    public void ApplyChange(FloatSO amount)
    {
        Value += amount.Value;
    }
}