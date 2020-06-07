﻿namespace Script.BaseCharacter
 {
     public class Character
     {
         public bool IsWalk;
         public bool IsArmed;
         public bool IsJumping;
         public bool OnGround;
         public bool IsCrouch;

         public readonly float WalkSpeed;
         public readonly float ArmedWalkSpeed;

         public float life;

         public readonly float JumpForce;

         public Character(float walkSpeed, float armedWalkSpeed, float jumpForce)
         {
             this.WalkSpeed = walkSpeed;
             this.ArmedWalkSpeed = armedWalkSpeed;
             this.JumpForce = jumpForce;
             this.IsWalk = false;
             this.IsArmed = false;
         }

         public void setLife(float life)
         {
             this.life = life;
         }
     }
 }
