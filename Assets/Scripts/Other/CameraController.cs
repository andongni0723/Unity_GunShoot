using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using Unity.Mathematics;
using UnityEditor.ShaderGraph.Internal;

public class CameraController : Singleton<CameraController>
{
    private new CinemachineVirtualCamera camera;
    private CinemachineBasicMultiChannelPerlin noise;

    protected override void Awake()
    {
        base.Awake();
        camera = GetComponent<CinemachineVirtualCamera>();
        noise = camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.ChangeCameraSight += OnChangeCameraSight; // Change camera sight use animation
    }

    private void OnDisable()
    {
        EventHandler.ChangeCameraSight -= OnChangeCameraSight; 
    }

    private void OnChangeCameraSight(float targetSize)
    {
        // camera size use 1 second to target size
        DOTween.To(() => camera.m_Lens.OrthographicSize, x => camera.m_Lens.OrthographicSize = x, targetSize, 1);
    }

    #endregion 

    public void CameraShake(float intensity, float time)
    {
        noise.m_AmplitudeGain = intensity;
        //noise.
        StartCoroutine(TimeToCloseShake(time));
    }

    private IEnumerator TimeToCloseShake(float time)
    {
        yield return new WaitForSeconds(time);

        noise.m_AmplitudeGain = 0;
    }

}
