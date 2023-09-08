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
    public GameObject UICanvas;
    private BaseWeapon baseWeapon => GetComponent<BaseWeapon>();
    private EnemySeePlayer enemySeePlayer => GetComponent<EnemySeePlayer>();

    [Header("Setting")] 
    public float chaseDistance = 15;
    public float attackDistance = 8;

    
    // State Machine
    [FoldoutGroup("Debug", true)]
    [FoldoutGroup("Debug"), SerializeField] private bool isGoneTargetPos = false;
    [FoldoutGroup("Debug"), SerializeField] private Vector3 originPos;
    [FoldoutGroup("Debug"), SerializeField] private float speed = 2;

    [Space(15)]
    [FoldoutGroup("Debug")]public bool canTestDraw;
    [FoldoutGroup("Debug")]public EnemyState currentEnemyState;

    public List<string> hitsL = new List<string>();

    private void Start()
    {
        originPos = transform.position;
        enemySpriteObject.SetActive(false);
        UICanvas.SetActive(false);
        
        StartCoroutine(ExecuteStateAction());
    }

    #region Event

    private void OnEnable()
    {
        EventHandler.GameWin += OnGameWin;
    }

    private void OnDisable()
    {
        EventHandler.GameWin -= OnGameWin;
    }

    private void OnGameWin()
    {
        enemySeePlayer.enabled = false;
        baseWeapon.enabled = false;
        enemySeePlayer.enemySpriteObject.SetActive(true);
    }

    #endregion 

    private void Update()
    {
        baseWeapon.SpeedToChangeShootAngle((currentEnemyState == EnemyState.Chase)? 1 : 0);
    }

    // Test
    private void OnDrawGizmos()
    {
        if (canTestDraw)
        {
            Debug.DrawRay(transform.position, enemySeePlayer.playerPos - transform.position, Color.yellow);
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            Gizmos.DrawWireSphere(transform.position, attackDistance);
        }
    }

    #region StateMachine

    /// <summary>
    /// Main function to execute all state action
    /// </summary>
    /// <returns></returns>
    IEnumerator ExecuteStateAction()
    {
        while (true)
        {
            SwitchState();

            switch (currentEnemyState)
            {
                case EnemyState.Back:
                    transform.position = Vector3.MoveTowards(transform.position, originPos, speed * Time.deltaTime);
                    break;
            
                case EnemyState.Attack:
                    // Rotation
                    float angle = Mathf.Atan2(enemySeePlayer.playerPosUpdate.y - transform.position.y ,enemySeePlayer.playerPosUpdate.x - transform.position.x) * Mathf.Rad2Deg;
                    enemyObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                
                    // Attack
                    if(baseWeapon.isReloadTimerEnd)
                        baseWeapon.Fire(gunPoint, enemyObject);
                    break;
            
                case EnemyState.Chase:
                    if (!enemySeePlayer.isFirstSeePlayer) break;

                    // Position
                    transform.position = Vector3.MoveTowards(transform.position, enemySeePlayer.playerPos,
                            speed * Time.deltaTime);
                    
                    // Check Enemy is hit the wall
                    // if (Vector2.Distance(GameObject.Find(enemySeePlayer.hitsL[0]).transform.position, transform.position) <= 2)
                    // {
                    //     
                    // }

                    // Rotation
                    angle = Mathf.Atan2(enemySeePlayer.playerPosUpdate.y - transform.position.y,
                        enemySeePlayer.playerPosUpdate.x - transform.position.x) * Mathf.Rad2Deg;
                    
                    enemyObject.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                    break;
            
                case EnemyState.Static:
                    enemySeePlayer.isFirstSeePlayer = false;
                    break;
            }

            yield return null;   
        }
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
        return !enemySeePlayer.canSeePlayer && chaseDistance > enemySeePlayer.playerDistance && !isGoneTargetPos;
    }

    /// <summary>
    /// Can see player and player in attack area
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToAttack()
    {
        return enemySeePlayer.canSeePlayer && attackDistance > enemySeePlayer.playerDistance;
    }

    /// <summary>
    /// Can't see player and player not in All area
    /// </summary>
    /// <returns></returns>
    private bool CanChangeToBack()
    {
        return !enemySeePlayer.canSeePlayer && enemySeePlayer.playerDistance > chaseDistance && !isGoneTargetPos;
    }

    #endregion
}
