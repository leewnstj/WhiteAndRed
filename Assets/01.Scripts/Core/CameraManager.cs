using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : SingleTon<CameraManager>
{
    private CinemachineVirtualCamera _cam;
    private CinemachineBasicMultiChannelPerlin m_Perlin;

    private void Awake()
    {
        _cam = GetComponent<CinemachineVirtualCamera>();
        m_Perlin = _cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void ShakeCamera(float value, float time)
    {
        StartCoroutine(Shaking(value, time));
    }

    private IEnumerator Shaking(float value, float time)
    {
        m_Perlin.m_AmplitudeGain = value;
        yield return new WaitForSeconds(time);
        m_Perlin.m_AmplitudeGain = 0;
    }
}
