using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.Feedbacks;

public class CameraController : MonoBehaviour
{
    public MMFeedbacks CameraShakeFeebacks;

    // Start is called before the first frame update
    void Start()
    {
        CameraShakeFeebacks = gameObject.GetComponent<MMFeedbacks>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CameraShake()
    {
        CameraShakeFeebacks.PlayFeedbacks();
    }
}
