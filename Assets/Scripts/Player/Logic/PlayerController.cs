using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [Header("Object")] 
    public GameObject player;
    public new Collider2D light;
    public Transform gunPoint;
    public FloatingJoystick joystick;
   
   
    [Header("Component")]
    public PlayerInputControlls inputControlls;
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public PlayerWeapon playerWeapon => GetComponent<PlayerWeapon>();
    private ItemManager itemManager => GetComponent<ItemManager>();
    private PlayerHealth playerHealth => GetComponent<PlayerHealth>();
    public Light2D gunFireLight;
    public Button interactiveButton;
    public GameObject mobileUIParent;
    public CanvasScaler canvasScaler;
    public MobileRotateController mobileMouseDragArea;

    [Header("Var")]
    public Vector3 moveDirection;
    public Vector3 mousePosition;
    public  Vector2 worldMousePosition;
    public float speed = 5f;
    public float currentSpeed;

    private GameObject wantInteractiveObject;
    private bool canInteractive = false;

    private float angle;
    private float lastPosX; 
    private float currentPosX;
    private bool isDrag;

    [Header("Debug")] 
    public Vector2 mouseRectPosition;
   
    private void Awake()
    {
        inputControlls = new PlayerInputControlls();
      
        inputControlls.GamePlay.Fire.performed += _ => OnFire();
        inputControlls.GamePlay.FireMobile.performed += _ => OnFireMobile();
        
        //inputControlls.GamePlay.DragScreenMobile.started += OnDragScreenMobileStart;
        //inputControlls.GamePlay.DragScreenMobile.performed += OnDragScreenMobile; // Mobile Look
        //inputControlls.GamePlay.DragScreenMobile.canceled += OnDragScreenMobileEnd;
        
        inputControlls.GamePlay.ReloadBullet.performed += _ => OnReload();
        inputControlls.GamePlay.Skill.performed += _ => OnUseSkill();
        inputControlls.GamePlay.Recovery.performed += _ => OnRecovery();
        inputControlls.GamePlay.PlayerDetails.performed += _ => OnOpenPlayerDetails();
        inputControlls.GamePlay.ChangeWeapon.performed += _ => OnChangeWeapon();
        inputControlls.GamePlay.InteractiveItem.performed += _ => OnPressInteractive();
        inputControlls.UI.Cancel.performed += _ => OnCancelUI();
      
        // Object
        interactiveButton.gameObject.SetActive(false);

    }

    

    #region Event

    private void OnEnable()
    {
        EventHandler.LoadPlayer += OnLoadPlayer; // update player details form SO
        inputControlls.Enable();
    }

    private void OnDisable()
    {
        EventHandler.LoadPlayer -= OnLoadPlayer;
        inputControlls.Disable();
    }

    private void OnLoadPlayer(CharacterDetails_SO _data)
    {
        playerWeapon.weaponList.AddRange(_data.startWeaponList);
        itemManager.skillItem = _data.skillItemDetails;
        itemManager.skillItemIconImage.sprite = _data.skillItemSprite;
        playerHealth.maxHealth = _data.startHealth;
        playerHealth.defense = _data.startDefense;

        mobileUIParent.SetActive(GameManager.Instance.gamePlatform == GamePlatform.Mobile);
        
        Debug.Log("load ppp");
        EventHandler.CallLoadPlayerEnd();
    }
    
    // Fire Action
    private void OnFire()
    {
        //Mouse
        if (GameManager.Instance.gamePlatform == GamePlatform.PC)
        {
            if(!EventSystem.current.IsPointerOverGameObject())
                playerWeapon.Fire(player, gunPoint.gameObject);
        }
    }
    private void OnFireMobile()
    {
        // Button
        if (GameManager.Instance.gamePlatform == GamePlatform.Mobile)
        {
            playerWeapon.Fire(player, gunPoint.gameObject);
        }
    }

    // Mobile Look Action
    private void OnDragScreenMobileStart(InputAction.CallbackContext ctx)
    {
        //Debug.Log(mousePosition);
        if (GameManager.Instance.gamePlatform == GamePlatform.Mobile)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(mobileUIParent.GetComponent<RectTransform>(),
                inputControlls.GamePlay.Look.ReadValue<Vector2>(), Camera.main,out mouseRectPosition);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(mobileMouseDragArea,
                //inputControlls.GamePlay.Look.ReadValue<Vector2>(), Camera.main,out mouseRectPosition);
                
            //Debug.Log(mouseRectPosition);

            // if (mobileMouseDragArea.rect.Contains(mouseRectPosition))
            // {
            //     isDrag = true;
            //     lastPosX = worldMousePosition.x; // first set 
            // }
            
            //if (RectTransformUtility.ScreenPointToLocalPointInRectangle(mobileUIParent.GetComponent<RectTransform>(), mousePosition, Camera.main,
                    // out var mouseRectPosition))
            {
                
            }

            // Debug.Log($"{mobileMouseDragArea.rect.xMin} {mouseRectPosition} {inputControlls.GamePlay.Look.ReadValue<Vector2>()} ");
            // if (mouseRectPosition.x >= mobileMouseDragArea.anchoredPosition.x &&
            //     mouseRectPosition.y >= mobileMouseDragArea.anchoredPosition.y)
            // {
            //     isDrag = true;
            //     lastPosX = worldMousePosition.x; // first set
            // }
            
            // float mouseAnchoredPosX = mousePosition.x - Screen.width - canvasScaler
            //     .referenceResolution.x;
            // float mouseAnchoredPosY = mousePosition.y - Screen.height - canvasScaler
            //     .referenceResolution.y;
            //
            // Debug.Log($"{mousePosition.x} {Screen.width} {canvasScaler.referenceResolution.x}");
            //
            // // Check player press in Look Touch area
            // if (mouseAnchoredPosX >= mobileMouseDragArea.anchoredPosition.x &&
            //     mouseAnchoredPosY >= mobileMouseDragArea.anchoredPosition.y)
            // {
            //     isDrag = true;
            //     lastPosX = worldMousePosition.x; // first set
            // }
        }
    }
    private void OnDragScreenMobileEnd(InputAction.CallbackContext ctx)
    {
        isDrag = false;
    
    }
    // private void LateUpdate()
    // {
    //     if (GameManager.Instance.gamePlatform == GamePlatform.Mobile)
    //     {
    //         if (isDrag)
    //         {
    //             currentPosX = worldMousePosition.x;
    //             player.transform.rotation = Quaternion.Euler(0, 0, 
    //                 player.transform.eulerAngles.z - (currentPosX - lastPosX) * 20);
    //
    //             lastPosX = currentPosX;  
    //         }
    //     }
    // }


    // Others
    private void OnReload()
    {
        StartCoroutine(playerWeapon.ReloadBullet());
    } 

    private void OnUseSkill()
    {
        itemManager.UseSkillItem(worldMousePosition - (Vector2)transform.position, 
            gunPoint.transform.position, player.transform.rotation);
    } 
   
    private void OnRecovery()
    {
        itemManager.UseRecoveryItem(GameManager.Instance.playerDetail.recoveryHealth);
    }
   
    private void OnOpenPlayerDetails()
    {
        // Check player has second weapon, and read player detail
        Debug.Log("TAB");
        EventHandler.CallReadPlayDetail(playerWeapon.weaponList[0], 
            (playerWeapon.weaponList.Count >= 2 ? playerWeapon.weaponList[1] : default), playerWeapon.data.weaponDetails); 
    }
   
    private void OnChangeWeapon()
    {
        // is Main weapon to change Second weapon
        // and is Second to Main
        if (playerWeapon.isReloadEnd)
        {
            EventHandler.CallChangeWeapon(playerWeapon.currentWeapon == playerWeapon.weaponList[0]? playerWeapon.weaponList[1] : playerWeapon.weaponList[0]);
            EventHandler.CallChangeCameraSight(playerWeapon.currentWeapon.cameraSight);
        }
    }
   
    private void OnPressInteractive()
    {
        if (canInteractive)
        {
            EventHandler.CallInteractiveItem(wantInteractiveObject);
        }
    }
   
    private void OnCancelUI()
    {
        EventHandler.CallOnCancelUI();
    }
    // Interface
    #endregion
   
    private void Update()
    {
        currentSpeed = (rb.velocity != default) ? 1 : 0;
        playerWeapon.SpeedToChangeShootAngle(currentSpeed);
    }
   

    private void FixedUpdate()
    {
        moveDirection = inputControlls.GamePlay.Move.ReadValue<Vector2>();
        mousePosition = inputControlls.GamePlay.Look.ReadValue<Vector2>();
        worldMousePosition  = Camera.main.ScreenToWorldPoint(mousePosition);
        
        Move();
        Look();
    }

    private void Move()
    {
        switch (GameManager.Instance.gamePlatform)
        {
            case GamePlatform.PC:
                // Keyboard
                rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime,
                    moveDirection.y * speed * Time.deltaTime);
                break;
         
            case GamePlatform.Mobile:
                // Joystick
                rb.velocity = new Vector2(joystick.Horizontal * speed * Time.deltaTime,
                    joystick.Vertical * speed * Time.deltaTime);
                break;
        }
    }

    private void Look()
    {
        // Look to mouse 
        switch (GameManager.Instance.gamePlatform)
        {
            case GamePlatform.PC:
                // Mouse
                angle = Mathf.Atan2(worldMousePosition.y - transform.position.y ,worldMousePosition.x - transform.position.x) * Mathf.Rad2Deg;
                player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
                break;
            case GamePlatform.Mobile:
                angle = mobileMouseDragArea.rZ;
                Debug.Log(angle);
                player.transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
        }
      
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractiveObject"))
        {
            interactiveButton.gameObject.SetActive(true);
            wantInteractiveObject = other.gameObject;
            canInteractive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("InteractiveObject"))
        {
            interactiveButton.gameObject.SetActive(false);
            wantInteractiveObject = default;
            canInteractive = false;
        } 
    }
}