using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
   public float distanceAttack;
   public float speed;
   protected bool isMoving = false;

   protected Rigidbody2D rb2d;
   protected Animator anim;
   protected Transform player;
   protected SpriteRenderer sprite;


   private void Awake()
   {
      rb2d = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      sprite = GetComponent<SpriteRenderer>();
      player = GameObject.Find("PlayerAsh").GetComponent<Transform>();
   }
   protected float PlayerDistace()
   {
      return Vector2.Distance(player.position, transform.position);
   }

   protected void Flip()
   {
      sprite.flipX = !sprite.flipX;
      speed *= -1;
   }

   protected virtual void Update()
   {
      float distance = PlayerDistace();

      isMoving = (distance <= distanceAttack);
        
      if (isMoving)
      {
         if((player.position.x > transform.position.x && sprite.flipX) || (player.position.x < transform.position.x && !sprite.flipX))
         {
            Flip();
         }
      }
   }
}
