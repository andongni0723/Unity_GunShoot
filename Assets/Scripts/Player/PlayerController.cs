using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;


public class PlayerController : Singleton<PlayerController>
{
   [Header("Object")] 
   public GameObject player;
   public Collider2D light;

   public Transform gunPoint;
   
   
   // Component
   public PlayerInputControlls inputControlls;
   private Rigidbody2D rb => GetComponent<Rigidbody2D>();
   public BaseWeapon playerWeapon => GetComponent<BaseWeapon>();
   private ItemManager itemManager => GetComponent<ItemManager>();
   public Light2D gunFireLight;


   [Header("Var")]
   public Vector2 moveDirection;
   public Vector2 mousePosition;
   public  Vector2 worldMousePosition;
   public float speed = 5f;
   public float currentSpeed;
   
   
   private void Awake()
   {
      inputControlls = new PlayerInputControlls();
      // Fire Action
      inputControlls.GamePlay.Fire.performed += _ => OnFire();
      inputControlls.GamePlay.ReloadBullet.performed += _ => OnReload();
      inputControlls.GamePlay.Skill.performed += _ => OnUseSkill();
      inputControlls.GamePlay.PlayerDetails.performed += _ => OnOpenPlayerDetails();
      inputControlls.GamePlay.ChangeWeapon.performed += _ => OnChangeWeapon();
   }
   
   #region Event

   private void OnEnable()
   {
      inputControlls.Enable();
   }

   private void OnDisable()
   {
      inputControlls.Disable();
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

   
}
