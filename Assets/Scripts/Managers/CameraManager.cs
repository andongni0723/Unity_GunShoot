using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera winBigMapCamera;

    private void Awake()
    {
        playerCamera.gameObject.SetActive(true);
        winBigMapCamera.gameObject.SetActive(false);
    }


    #region Event

    private void OnEnable()
    {
        EventHandler.GameWin += OnGameWin; // Camera switch to bigMapCamera
    }

    private void OnDisable()
    {
        EventHandler.GameWin += OnGameWin;
    }

    private void OnGameWin()
    {
        playerCamera.gameObject.SetActive(false);
        winBigMapCamera.gameObject.SetActive(true);
        
        // after 2.5 second, the camera switch animation is done
        Invoke(nameof(OnGameWinCameraSwitchDone), 2.5f);
    }
    
    private void OnGameWinCameraSwitchDone()
    {
        EventHandler.CallGameWinCameraSwitchDone();   
    }

    #endregion 

}
