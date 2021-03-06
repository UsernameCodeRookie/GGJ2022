using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntReference
{
    public bool UseConstant = true;
    public int ConstantValue;
    public IntSO Variable;

    public IntReference()
    { }

    public IntReference(int value)
    {
        UseConstant = true;
        ConstantValue = value;
    }

    public int Value
    {
        get { return UseConstant ? ConstantValue : Variable.Value; }
    }

    public static implicit operator int(IntReference reference)
    {
        return reference.Value;
    }
}
