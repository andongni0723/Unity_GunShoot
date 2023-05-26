using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using DG.Tweening;
using Random = UnityEngine.Random;


public class PlayerController : MonoBehaviour
{
   [Header("Object")] 
   public GameObject player;
   public Collider2D light;

   public Transform gunPoint;
   
   
   // Component
   public PlayerInputControlls inputControlls;
   private Rigidbody2D rb;
   private BaseWeapon playerWeapon;
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
      rb = GetComponent<Rigidbody2D>();
      playerWeapon = GetComponent<BaseWeapon>();
      //bulletPool = GetComponent<BulletPool>();
      
      // Fire Action
      inputControlls.GamePlay.Fire.performed += _ => OnFire();
      inputControlls.GamePlay.ReloadBullet.performed += _ => OnReload();
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
      playerWeapon.Fire(player, gunPoint.gameObject);
   }
   
   private void OnReload()
   {
      StartCoroutine(playerWeapon.ReloadBullet());
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
      //float angle = MathF.Atan2(worldMousePosition.y, worldMousePosition.x);
      
      float angle = Mathf.Atan2(worldMousePosition.y - transform.position.y ,worldMousePosition.x - transform.position.x) * Mathf.Rad2Deg;


      player.transform.rotation = Quaternion.Euler(0, 0, angle - 90);
   }

   
}
