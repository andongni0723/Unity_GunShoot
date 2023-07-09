using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BaseSeePlayer : MonoBehaviour
{
    [Header("Setting")] 
    public LayerMask rayLayerMask;
    
    [FoldoutGroup("Debug", true)]
    [FoldoutGroup("Debug")] public bool canSeePlayer = false;
    [FoldoutGroup("Debug")] public bool isFirstSeePlayer; // If enemy not see player yet, enemy will not attack player until see.

    [Space(15)]
    [FoldoutGroup("Debug")] public float playerDistance;
    [FoldoutGroup("Debug")] public Vector3 playerPos;
    [FoldoutGroup("Debug")] public Vector3 playerPosUpdate;
    public List<string> hitsL = new List<string>();

    // Var
    private RaycastHit2D[] hits = new RaycastHit2D[5];
    [SerializeField] private bool canRayToPlayer = false; 
    
    
    private void Update()
    {
        playerPosUpdate = GameManager.Instance.playerHeadObject.transform.position;
        playerDistance = Vector2.Distance(transform.position,playerPosUpdate);
        
        // Set self active
        if (canRayToPlayer)
        {
            CheckPlayerSightAndSetActive();
        }
    }
    
    #region CheckSeePlayer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Light"))
        {
            canRayToPlayer = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.transform.parent.name);
        if (other.CompareTag("Light"))
        {
            canRayToPlayer = false;
            canSeePlayer = false;
            
            OnPlayerLeft();
            //enemySpriteObject.SetActive(false); 
        }
    }

    private void CheckPlayerSightAndSetActive()
    {
        playerPos = playerPosUpdate;

        //hits = Physics2D.RaycastAll(transform.position, playerPos - transform.position, Vector2.Distance(transform.position, playerPos) + 10, rayLayerMask);
        int hitCount = Physics2D.RaycastNonAlloc(transform.position, playerPos - transform.position, hits,
            Vector2.Distance(transform.position, playerPos) + 10, rayLayerMask);

        //TODO: fix
        //TestAddRayList(hitCount);

        hitsL.Clear();
        for (int i = 0; i < hitCount;)
        {
            
            hitsL.Add(hits[i].transform.name);

            if (hits[0].transform.name == GameManager.Instance.playerObject.name)
            {
                canSeePlayer = true;
                isFirstSeePlayer = true;
                
                OnSeePlayer();
            }
            else
            {
                canSeePlayer = false;
                
                OnPlayerLeft();
            }
            
            break;
        }
    }

    
    /// <summary>
    /// Debug to see that item hit
    /// </summary>
    private void TestAddRayList(int hitCount)
    {
        hitsL.Clear();

        for (int i = 0; i < hitCount;)
        {
            hitsL.Add(hits[i].transform.name);
        }
    }

    #endregion

    protected virtual void OnSeePlayer() { }

    protected virtual void OnPlayerLeft() { }
}
