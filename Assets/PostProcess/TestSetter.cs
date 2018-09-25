using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.PostProcessing;

using Cinemachine.PostFX;

public class TestSetter : MonoBehaviour {

    [Range(0f, 1f)]
    public float blackMix;
    [Range(0f, 1f)]
    public float whiteMix;

    CinemachinePostProcessing fx;

    private void Start()
    {
        fx = GetComponent<CinemachinePostProcessing>();
    }

    // Use this for initialization
    private void Update()
    {
        fx.m_Profile.GetSetting<fadeBlack>().blend.value = blackMix;
        fx.m_Profile.GetSetting<fadeWhite>().blend.value = whiteMix;
    }
}
