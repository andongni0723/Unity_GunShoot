using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Sirenix;
using Sirenix.OdinInspector;

public class BaseEnemy : MonoBehaviour
{
    [Header("Component")] 
    public GameObject enemyObject;
    public GameObject enemySpriteObject;
    public GameObject gunPoint;
    public BaseWeapon baseWeapon;

    [Header("Setting")] 
    public LayerMask rayLayerMask;
    public float chaseDistance = 15;
    public float attackDistance = 8;

    // Var
    private RaycastHit2D[] hits = new RaycastHit2D[5];
    [SerializeField] private bool canRayToPlayer = false; // Ray to player to check player can see enemy
    
    // State Machine
    [FoldoutGroup("Debug", true)]
    [FoldoutGroup("Debug"), SerializeField] private bool canSeePlayer = false;
    [FoldoutGroup("Debug"), SerializeField] private bool isGoneTargetPos = false;
    [FoldoutGroup("Debug"), SerializeField] private Vector3 originPos;
    [FoldoutGroup("Debug"), SerializeField] private float speed = 2;

    [Space(15)]
    [FoldoutGroup("Debug")]public bool canTestDraw;
    [FoldoutGroup("Debug"), SerializeField] private bool isFirstSeePlayer; // If enemy not see player yet, enemy will not attack player until see.
    [FoldoutGroup("Debug")]public EnemyState currentEnemyState;
    [FoldoutGroup("Debug")]public float playerDistance;
    [FoldoutGroup("Debug"), SerializeField] private Vector3 playerPos;
    [FoldoutGroup("Debug"), SerializeField] private Vector3 playerPosUpdate;
    public List<string> hitsL = new List<string>();

    private void Start()
    {
        originPos = transform.position;
    }

    private void Update()
    {
        playerPosUpdate = GameManager.Instance.playerObject.transform.position;
        playerDistance = Vector2.Distance(transform.position,playerPosUpdate);
        
        baseWeapon.SpeedToChangeShootAngle((currentEnemyState == EnemyState.Chase)? 1 : 0);
        
        StartCoroutine(ExecuteStateAction());
        
        // Set self active
        if (canRayToPlayer)
        {
            CheckPlayerSightAndSetActive();
        }
    }
    
    

    // Test
    private void OnDrawGizmos()
    {
        if (canTestDraw)
        {
            Debug.DrawRay(transform.position, playerPos - transform.position, Color.yellow);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

    #region CheckSeePlayer

    private void OnTriggerEnter2D(Collider2D other)
    {
        canRayToPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log(other.transform.parent.name);
        if (other.CompareTag("Light"))
        {
            canRayToPlayer = false;
            canSeePlayer = false;
            enemySpriteObject.SetActive(false); 
        }
    }

    private void CheckPlayerSightAndSetActive()
    {
        playerPos = playerPosUpdate;
        hits = Physics2D.RaycastAll(transform.position, playerPos - transform.position,
            Vector2.Distance(transform.position, playerPos) + 10, rayLayerMask);

        TestAddRayList();

        if (hits.Length != 0)
        {
            // Check first hit object is player, that is be player can see enemy
            if (hits[0].transform.name == GameManager.Instance.playerObject.name)
            {
                canSeePlayer = true;
                enemySpriteObject.SetActive(true);
                isFirstSeePlayer = true;
            }
            else
            {
                canSeePlayer = false;
                enemySpriteObject.SetActive(false);
            }
        }
        else
        {
            enemySpriteObject.SetActive(false);
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

    #endregion


    #region StateMachine

    /// <summary>
    /// Main function to execute all state action
    /// </summary>
    /// <returns></returns>
    IEnumerator ExecuteStateAction()
    {
        SwitchState();

        switch (currentEnemyState)
        {
            case EnemyState.Back:
                transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
                break;
            case EnemyState.Attack:
                // Rotation
                enemyObject.transform.up = playerPosUpdate - transform.position;
                // Attack
                baseWeapon.Fire(gunPoint, enemyObject);
                break;
            case EnemyState.Chase:
                if (isFirstSeePlayer)
                {
                    // Position
                    transform.position = Vector3.MoveTowards(transform.position, playerPos, speed * Time.deltaTime);
                    // Rotation
                    enemyObject.transform.up = playerPosUpdate - transform.position;
                }
                break;
            case EnemyState.Static:
                isFirstSeePlayer = false;
                break;
        }

        yield return null;
    }

    /// <summary>
    /// Check current state and switch state
    /// </summary>
    private void SwitchState()
    {
        if (CanChangeToBack())
        {
            currentEnemyState = EnemyState.Back;
        }
        else if(CanChangeToAttack())
        {
            currentEnemyState = EnemyState.Attack;
        }
        else if(CanChangeToChase())
        {
            currentEnemyState = EnemyState.Chase;
        }
        else if (CanChangeToStatic())
        {
            currentEnemyState = EnemyState.Static;
        }
    }

    /// <summary>
    /// Enemy move action is finish and other state is false
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToStatic()
    {
        return isGoneTargetPos;
    }

    /// <summary>
    /// Can see player and player in chase area
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToChase()
    {
        return !canSeePlayer && chaseDistance > playerDistance && !isGoneTargetPos;
    }

    /// <summary>
    /// Can see player and player in attack area
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToAttack()
    {
        return canSeePlayer && attackDistance > playerDistance;
    }

    /// <summary>
    /// Can't see player and player not in All area
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToBack()
    {
        return !canSeePlayer && playerDistance > chaseDistance && !isGoneTargetPos;
    }

    #endregion
}
