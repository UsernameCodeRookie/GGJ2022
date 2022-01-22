using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObject/PlayerDataSO", order = 1)]

public class PlayerDataSO:ScriptableObject{
    public float initSpeed,rushSpeed,atkRad,maxSp,maxMp,mpRate;
    public int atkCnt;
}
