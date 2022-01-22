using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Int Variable", menuName = "Variables/Int",order = 1)]
public class IntSO : ScriptableObject
{

#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    public int Value;

    public void SetValue(int value)
    {
        Value = value;
    }

    public void SetValue(IntSO value)
    {
        Value = value.Value;
    }

    public void ApplyChange(int amount)
    {
        Value += amount;
    }

    public void ApplyChange(IntSO amount)
    {
        Value += amount.Value;
    }
}
