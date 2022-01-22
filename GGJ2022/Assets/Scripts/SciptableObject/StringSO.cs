using UnityEngine;

[CreateAssetMenu(fileName = "String Variable", menuName = "Variables/String", order = 3)]
public class StringSO : ScriptableObject
{
    [SerializeField]
    private string value = "";

    public string Value
    {
        get { return value; }
        set { this.value = value; }
    }
}