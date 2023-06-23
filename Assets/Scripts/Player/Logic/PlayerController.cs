using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
   [Header("Object")] 
   public GameObject player;
   public new Collider2D light;

   public Transform gunPoint;
   
   
   [Header("Component")]
   public PlayerInputControlls inputControlls;
   private Rigidbody2D rb => GetComponent<Rigidbody2D>();
   public PlayerWeapon playerWeapon => GetComponent<PlayerWeapon>();
   private ItemManager itemManager => GetComponent<ItemManager>();
   private PlayerHealth playerHealth => GetComponent<PlayerHealth>();
   public Light2D gunFireLight;
   public Button interactiveButton;

   [Header("Test")] 
   public CharacterDetails_SO data;

   public int d;
   
   [Header("Var")]
   public Vector2 moveDirection;
   public Vector2 mousePosition;
   public  Vector2 worldMousePosition;
   public float speed = 5f;
   public float currentSpeed;

   private GameObject wantInteractiveObject;
   private bool canInteractive = false;
   
   private void Awake()
   {
      inputControlls = new PlayerInputControlls();
      
      inputControlls.GamePlay.Fire.performed += _ => OnFire();
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

      Debug.Log("load ppp");
      EventHandler.CallLoadPlayerEnd();
   }

   private void OnFire()
   {
      if(!EventSystem.current.IsPointerOverGameObject())
         playerWeapon.Fire(player, gunPoint.gameObject);
   }
   
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
      itemManager.UseRecoveryItem(data.recoveryHealth);
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
      moveDirection = inputControlls.GamePlay.Move.ReadValue<Vector2>();
      mousePosition = inputControlls.GamePlay.Look.ReadValue<Vector2>();
      worldMousePosition  = Camera.main.ScreenToWorldPoint(mousePosition);
      
      currentSpeed = (moveDirection != default) ? 1 : 0;
      playerWeapon.SpeedToChangeShootAngle(currentSpeed);
   }
   

   private void FixedUpdate()
   {
      Move();
      Look();
   }

   private void Move()
   {
      rb.velocity = new Vector2(moveDirection.x * speed * Time.deltaTime, moveDirection.y * speed * Time.deltaTime);
   }

   private void Look()
   {
      // Look to mouse 
      float angle = Mathf.Atan2(worldMousePosition.y - transform.position.y ,worldMousePosition.x - transform.position.x) * Mathf.Rad2Deg;

      player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
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
