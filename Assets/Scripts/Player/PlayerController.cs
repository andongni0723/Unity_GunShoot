using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
   [Header("Object")] 
   public Collider2D light;
   
   
   // Component
   private PlayerInputControlls inputControlls;
   private Rigidbody2D rb;
   
   [Header("Var")]
   public Vector2 moveDirection;
   public Vector2 mousePosition;
   public  Vector2 worldMousePosition;
   public float speed = 5f;
   
   private void Awake()
   {
      inputControlls = new PlayerInputControlls();
      rb = GetComponent<Rigidbody2D>();
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

   #endregion

   private void OnDrawGizmos()
   {
      //Gizmos.DrawLine(transform.position, transform.localPosition + Vector3.up * 30);
      
   }

   private void Update()
   {
      moveDirection = inputControlls.GamePlay.Move.ReadValue<Vector2>();
      mousePosition = inputControlls.GamePlay.Look.ReadValue<Vector2>();
      worldMousePosition  = Camera.main.ScreenToWorldPoint(mousePosition);

      RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, LayerMask.NameToLayer("Default"));

      if (hit.collider != null)
      {
         if (hit.transform.CompareTag("Enemy"))
         {
            Debug.Log("Enemy!!!");
         }
      }
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


      transform.rotation = Quaternion.Euler(0, 0, angle - 90);
   }
}
