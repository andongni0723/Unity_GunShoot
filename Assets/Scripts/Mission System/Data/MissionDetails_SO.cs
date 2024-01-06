using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "Game Data/Mission Detail", fileName = "Details_SO")]
public class MissionDetails_SO : ScriptableObject
{
    public string missionName;
    [TextArea] public string missionDescription;

    [Space(10)] 
    public MissionType missionType;
    public MissionContentType missionContentType;

    [Space(20)]
    [Title("Go to item point")]
    [TagSelector] public string targetPointTag;
    public string RealTargetPointName;

    [Space(20)] 
    [Title("Count Item")] 
    [TagSelector] public string CountItemTag;


    [Space(20)] 
    public List<MissionDetails_SO> MissionDoneNextMissionList = new List<MissionDetails_SO>();
}

