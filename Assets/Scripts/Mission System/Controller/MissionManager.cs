using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MissionManager : MonoBehaviour
{
    [Header("Setting")]
    public List<MissionDetails_SO> startGameMissionList = new List<MissionDetails_SO>();

    [Header("Component")] 
    public GameObject missionPanel;
    public GameObject mainMissionPrefabs;
    public GameObject otherMissionPrefabs;

    private void Start()
    {
        foreach (MissionDetails_SO data in startGameMissionList)
        {
            EventHandler.CallNewMission(data);
        }
    }


    #region Event

    private void OnEnable()
    {
        EventHandler.NewMission += OnNewMission; // Instantiate New Mission and Setting
    }

    private void OnDisable()
    {
        EventHandler.NewMission -= OnNewMission;
    }

    private void OnNewMission(MissionDetails_SO data)
    {
        GameObject newMission = new GameObject();
        switch (data.missionType)
        {
            case MissionType.MainMission:
                newMission = Instantiate(mainMissionPrefabs, missionPanel.transform);
                newMission.transform.SetSiblingIndex(1); // Put the Main Mission on top
                break;
            
            case MissionType.OtherMission:
                newMission = Instantiate(otherMissionPrefabs, missionPanel.transform);
                break;
        }

        newMission.GetComponent<MissionUI>().missionDetails = data;
    }

    #endregion 
}
