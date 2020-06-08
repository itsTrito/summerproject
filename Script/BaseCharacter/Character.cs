﻿namespace BaseCharacter
 {
     public class Character
     {
         public bool IsWalk;
         public bool IsArmed;
         public bool IsJumping;
         public bool OnGround;
         public bool IsCrouch;
         public bool IsAttacking;
         public bool IsBigAttacking;

         private readonly float _walkSpeed;
         private readonly float _armedWalkSpeed;

         public int life;
         public int GroundNumAttack;
         public int AirNumAttack;

         public readonly float FallForce;
         private readonly float _jumpForce;

         public Character(float walkSpeed, float armedWalkSpeed, float jumpForce, float fallForce, int life)
         {
             this._walkSpeed = walkSpeed;
             this._armedWalkSpeed = armedWalkSpeed;
             this._jumpForce = jumpForce;
             this.FallForce = fallForce;
             this.life = life;
             this.IsWalk = false;
             this.IsArmed = false;
             this.IsCrouch = false;
             this.IsJumping = false;
             this.IsAttacking = false;
             this.OnGround = false;
             this.IsBigAttacking = false;
         }

         public void SetAttackPossibility(int groundNum, int airNum)
         {
             this.GroundNumAttack = groundNum;
             this.AirNumAttack = airNum;
         }

         public float Move()
         {
             IsWalk = !IsJumping;
             return (!IsArmed?_walkSpeed * (IsCrouch?1.5f:1f):_armedWalkSpeed);
         }

         public float Jump()
         {
             float jumpPower = _jumpForce / (IsArmed?1.5f:1f) * (IsCrouch?1.5f:1f);
             IsJumping = true;
             OnGround = false;
             IsCrouch = false;
             return jumpPower;
         }

         public bool DoAttack()
         {
             return IsAttacking || IsBigAttacking;
         }
     }
 }
