using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MissionUI : MonoBehaviour
{
    [Header("Data")]
    public MissionDetails_SO missionDetails;

    [Header("Component")] 
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public CanvasGroup missionDoneCanvasGroup;
    public GameObject missionPointPrefab;
    public GameObject realItemMissionPointPrefab;
    private Image _image => GetComponent<Image>();

    [Header("Setting")] 
    public float missionDoneFadeTime = 1;
    public float missionDisappearTime = 1;
    
    [Title("Go To Item Point Mission")]
    public List<GameObject> goToMissionTargetPointList = new List<GameObject>();
    public List<GameObject> missionTargetPointList = new List<GameObject>();
    public int allMissionPointCount;
    public int playerArrivesTargetPointCount; // When player arrives target point,
                                              // the MissionPoint will call this var increase.

    public bool isPlayerArrivesRealItemPoint; // When player arrives the real item target point,
                                              // the MissionPoint will call this var true.

    [Space(20)] 
    [Title("Count Item")] 
    public List<GameObject> countItemTargetList = new List<GameObject>();
    public int allCountItemCount;
    public int itemDisappearCount; // When item which mission count was destroy,
                                   // the MissionCountItem will call this var increase.

    private bool _isMissionDoneActionCall; // Make sure the mission done action run only once.

    private IEnumerator Start()
    {
        // Wait
        yield return new WaitUntil(() => missionDetails != null);
        
        if (missionDetails.missionContentType == MissionContentType.GotoItemPoint)
            yield return new WaitUntil(() => GameObject.FindWithTag(missionDetails.TargetPointTag) != null);

        // Setting UI
        titleText.text = missionDetails.missionName;

        PrepareMissionCheckItem();
        
    }

    private void LateUpdate()
    {
        UpdateMissionUI();
    }

    private void PrepareMissionCheckItem()
    {
        switch (missionDetails.missionContentType)
        {
            case MissionContentType.GotoItemPoint:
                
                // Get All Mission Item to spawn Mission Point
                foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag(missionDetails.TargetPointTag))
                {
                   goToMissionTargetPointList.Add(targetObject); //Debug
                   
                   MissionPoint newMissionPoint = Instantiate(missionPointPrefab, targetObject.transform.position, 
                       Quaternion.identity).GetComponent<MissionPoint>();

                   newMissionPoint.parentMission = this;
                   
                   missionTargetPointList.Add(newMissionPoint.gameObject);
                }
                
                allMissionPointCount = missionTargetPointList.Count;
                
                // Spawn Real Item Point
                MissionPoint realItem = Instantiate(realItemMissionPointPrefab, 
                    GameObject.FindWithTag(missionDetails.TargetPointTag).transform.position, Quaternion.identity)
                    .GetComponent<MissionPoint>();
                
                realItem.parentMission = this;
                realItem.isItemRealPoint = true;

                break;
            
            case MissionContentType.CountSomeThing:

                Debug.Log("Find");
                // Get All Item which mission count to Add Script to Check them status
                foreach (GameObject targetObject in GameObject.FindGameObjectsWithTag(missionDetails.CountItemTag))
                {
                    countItemTargetList.Add(targetObject);

                    MissionCountItem newMissionCountItem = targetObject.AddComponent<MissionCountItem>();

                    newMissionCountItem.parentMission = this;
                }

                allCountItemCount = countItemTargetList.Count;
                break;
        }
    }

    private void UpdateMissionUI()
    {
        switch (missionDetails.missionContentType)
        {
            case MissionContentType.GotoItemPoint:
                
                descriptionText.text =  $"{missionDetails.missionDescription} ({playerArrivesTargetPointCount} / {allMissionPointCount} )";

                // Player arrived all target point
                if (!_isMissionDoneActionCall)
                {
                    if ((playerArrivesTargetPointCount == allMissionPointCount && allMissionPointCount != 0 ) || isPlayerArrivesRealItemPoint )
                    {
                        MissionDone();
                        _isMissionDoneActionCall = true;
                    }
                }
                break;
            
            case MissionContentType.CountSomeThing:
                
                descriptionText.text =  $"{missionDetails.missionDescription} ({itemDisappearCount} / {allCountItemCount} )";
                
                // Player collected item which mission count
                if (!_isMissionDoneActionCall)
                {
                    if (itemDisappearCount == allCountItemCount && allCountItemCount != 0)
                    {
                        MissionDone();
                        _isMissionDoneActionCall = true;
                    }
                }
                break;
        }
    }

    private void MissionDone()
    {
        // Spawn Next Mission
        foreach (MissionDetails_SO mission in missionDetails.MissionDoneNextMissionList)
        {
            EventHandler.CallNewMission(mission);
        }
        
        // Destroy all mission target point UI
        foreach (GameObject targetPoint in missionTargetPointList)
        {
            if(targetPoint != null)
                Destroy(targetPoint);
        }
        
        // Animation
        Sequence sequence = DOTween.Sequence();
        sequence.Append(missionDoneCanvasGroup.DOFade(1, missionDoneFadeTime));
        sequence.Append(_image.DOFade(0, missionDisappearTime));
        sequence.OnComplete(() => Destroy(gameObject));
    }
}
