using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Config/PlayerDataSO", order = 1)]
public class PlayerDataSO : ScriptableObject{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif

    [Header("Speed Config")]
    public float initSpeed;
    public float rushSpeedMultiplier;

    [Header("Player Status Config")]
    public int maxHp;
    public float maxSp;
    public float maxMp;
    public float mpRecoverRate;

    [Header("Speed Config")]
    public float attackRadius;
    public int plerMaxAttackCount;
}
