using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource BGMSource;

    public AudioClipSetSO BGMSet;

    private GameManager gameManager;
    private bool isPlayingMainBGM = false;
    private float curTime;

    [Header("BGM Speed Configs")]
    public float defaultPitch = 1f;
    public float secondStagePitch = 1.1f;
    public float thirdStagePitch = 1.2f;
    public float finalStagePitch = 1.3f;
    public float secondStageTime = 60f;
    public float thirdStageTime = 120f;
    public float finalStageTime = 150f;
    
    // Start is called before the first frame update
    void Start()
    {
        BGMSource = gameObject.GetComponentInChildren<AudioSource>();
        BGMSet.Items[0].Play(BGMSource);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchBGM();
        AdjustBGMPitch();   
    }

    private void SwitchBGM()
    {
        gameManager = GameManager.instance;
        if (gameManager.isPlaying && !isPlayingMainBGM)
        {
            isPlayingMainBGM = true;
            BGMSet.Items[1].Play(BGMSource);
        }
    }

    private void AdjustBGMPitch()
    {
        gameManager = GameManager.instance;
        curTime = gameManager.timer;

        if (curTime >= secondStageTime)
            BGMSource.pitch = secondStagePitch;

        if (curTime >= thirdStageTime)
            BGMSource.pitch = thirdStagePitch;

        if (curTime >= finalStageTime)
            BGMSource.pitch = finalStagePitch;
        
    }
}
