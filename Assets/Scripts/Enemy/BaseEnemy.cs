using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [Header("Component")]
    public GameObject enemySpriteParent;
    
    [Header("Setting")]
    public LayerMask rayLayerMask;
    
    // Var
    private RaycastHit2D[] hits = new RaycastHit2D[5];
    [SerializeField] private Vector3 playerPos;
    [SerializeField] private bool canRayToPlayer = false; // Ray to player to check player can see enemy

    [SerializeField]public List<string> hitsL = new List<string>();
    private void Update()
    {
        if(canRayToPlayer) 
            CheckPlayerSightAndSetActive();
    }

    private void OnDrawGizmos()
    {
        //  Gizmos.DrawLine(transform.position, playerPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canRayToPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canRayToPlayer = false;
        enemySpriteParent.SetActive(false); 
    }

    private void CheckPlayerSightAndSetActive()
    {
        playerPos = GameManager.Instance.playerObject.transform.localPosition;
        hits = Physics2D.RaycastAll(transform.position, playerPos - transform.position, Vector2.Distance(transform.position, playerPos) + 10, rayLayerMask);
        Debug.DrawRay(transform.position, playerPos - transform.position, Color.yellow);
        //TODO: set active bug

        TestAddRayList();

        if (hits.Length != 0)
        {
            // Check first hit object is player, that is be player can see enemy
            if (hits[0].transform.name == GameManager.Instance.playerObject.name)
            {
                //Debug.Log("Did Hit");
                enemySpriteParent.SetActive(true);
            }
            else
            {
                enemySpriteParent.SetActive(false);
            } 
        }
        else
        {
            enemySpriteParent.SetActive(false);
        }
        
    }

    private void TestAddRayList()
    {
        hitsL.Clear();

        foreach (var hit in hits)
        {
            hitsL.Add(hit.transform.name);
        }
    }
}
