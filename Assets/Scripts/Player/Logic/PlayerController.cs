using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
    [Header("Object")] 
    public GameObject player;
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
    public TextMeshProUGUI interactiveItemNameText;
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

    private void Awake()
    {
        inputControlls = new PlayerInputControlls();

        
        inputControlls.GamePlay.Fire.performed += _ => OnFire();
        inputControlls.GamePlay.Fire.canceled += _ => OnFireDone();
        inputControlls.GamePlay.FireMobile.performed += _ => OnFireMobile();
        inputControlls.GamePlay.FireMobile.canceled += _ => OnFireDone();
        inputControlls.GamePlay.InteractiveItem.performed += _ => OnPressInteractive();
        inputControlls.GamePlay.ReloadBullet.performed += _ => OnReload();
        inputControlls.GamePlay.Skill.performed += _ => OnUseSkill();
        inputControlls.GamePlay.Recovery.performed += _ => OnRecovery();
        inputControlls.GamePlay.PlayerDetails.performed += _ => OnOpenPlayerDetails();
        inputControlls.GamePlay.ChangeWeapon.performed += _ => OnChangeWeapon();
        inputControlls.UI.Cancel.performed += _ => OnCancelUI();
        inputControlls.Enable();

        // Object
        interactiveButton.gameObject.SetActive(false);

    }


    #region Event

    private void OnEnable()
    {
        EventHandler.LoadPlayer += OnLoadPlayer; // update player details form SO
        EventHandler.OpenStorePanel += OnOpenPanel; // Stop Player Input
        EventHandler.OpenPlayerDetailsPanel += OnOpenPanel; // Stop Player Input
        EventHandler.CloseUI += OnClosePanel; // Open Player Input
        EventHandler.GameWin += OnGameWin; // Stop Player Input
    }
    private void OnDisable()
    {
        EventHandler.LoadPlayer -= OnLoadPlayer;
        EventHandler.OpenStorePanel -= OnOpenPanel; 
        EventHandler.OpenPlayerDetailsPanel -= OnOpenPanel;
        EventHandler.CloseUI -= OnClosePanel;
        EventHandler.GameWin -= OnGameWin;
        
        InputControllerDisable();
    }

    private void OnOpenPanel()
    {
        inputControlls.GamePlay.Disable();
    }
    private void OnClosePanel()
    {
        inputControlls.GamePlay.Enable();
    }

    private void InputControllerDisable()
    {
        inputControlls.Disable();
    }

    private void OnLoadPlayer(CharacterDetails_SO _data)
    {
        playerWeapon.weaponList.AddRange(_data.startWeaponList);
        itemManager.skillItem = _data.skillItemDetails;
        itemManager.skillItemIconImage.sprite = _data.skillItemSprite;
        playerHealth.maxHealth = _data.startHealth;
        playerHealth.currentHealth = _data.startHealth;
        playerHealth.defense = _data.startDefense;

        mobileUIParent.SetActive(GameManager.Instance.gamePlatform == GamePlatform.Mobile);
        
        Debug.Log("load ppp");
        EventHandler.CallLoadPlayerEnd();
    }

    private void OnGameWin()
    {
        InputControllerDisable();
    }
    

    // Fire Action
    private void OnFire()
    {
        //Mouse
        if (GameManager.Instance.gamePlatform == GamePlatform.PC)
        {
            playerWeapon.SetShootDone(false);
            playerWeapon.Fire(player, gunPoint.gameObject);
            
            // if (!EventSystem.current.IsPointerOverGameObject())
            // {
            //     
            // }
        }
    }
    private void OnFireMobile()
    {
        // Button
        if (GameManager.Instance.gamePlatform == GamePlatform.Mobile)
        {
            playerWeapon.SetShootDone(false);
            playerWeapon.Fire(player, gunPoint.gameObject);
        }
    }

    private void OnFireDone()
    {
        playerWeapon.SetShootDone(true);
    }

    // Others
    private void OnReload()
    {
        StartCoroutine(playerWeapon.ReloadBullet());
    } 

    private void OnUseSkill()
    {
        itemManager.UseSkillItem((Vector2)gunPoint.transform.position - (Vector2)transform.position, 
            gunPoint.transform.position, player.transform.rotation);
    } 
   
    private void OnRecovery()
    {
        itemManager.UseRecoveryItem(GameManager.Instance.playerDetail.recoveryHealth);
    }
   
    private void OnOpenPlayerDetails()
    {
        EventHandler.CallOpenPlayerDetailsPanel();
        
        // Check player has second weapon, and read player detail
        EventHandler.CallReadPlayDetail(playerWeapon.weaponList[0], 
            (playerWeapon.weaponList.Count >= 2 ? playerWeapon.weaponList[1] : default), playerWeapon.data.weaponDetails); 
    }
   
    private void OnChangeWeapon()
    {
        // is Main weapon to change Second weapon
        // and is Second to Main
        EventHandler.CallChangeWeapon(playerWeapon.currentWeapon == playerWeapon.weaponList[0]? playerWeapon.weaponList[1] : playerWeapon.weaponList[0]);
    }
    
    private void OnPressInteractive()
    {
        if (canInteractive)
        {
            wantInteractiveObject.GetComponent<BaseInteractiveItem>().InteractiveAction();
        }
    }
   
    private void OnCancelUI()
    {
        EventHandler.CallCloseUI();
    }
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
                angle = mobileMouseDragArea.rotationZ;
                //Debug.Log(angle);
                player.transform.rotation = Quaternion.Euler(0, 0, angle);
                break;
        }
      
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("InteractiveObject"))
        {
            wantInteractiveObject = other.gameObject;
            
            interactiveItemNameText.text = wantInteractiveObject.GetComponent<BaseInteractiveItem>().InteractiveDescription;
            interactiveButton.gameObject.SetActive(true);
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