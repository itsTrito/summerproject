using System;
using Script.BaseCharacter;
using UnityEngine;

namespace Script.Player
{
    public class MyPlayerController : MonoBehaviour
    {
        public Rigidbody2D rb;
        public SpriteRenderer sp;
        public Animator an;

        private Vector2 _vel;
    
        public Character player;
        private static readonly int OnGround = Animator.StringToHash("OnGround");
        private static readonly int Jumping = Animator.StringToHash("Jumping");
        private static readonly int Walk = Animator.StringToHash("Walk");
        private static readonly int Crouch = Animator.StringToHash("Crouch");

        private const float Tolerance = 0.05f;

        private void Start()
        {
            player = new Character(4f, 2f, 8f);
            player.setLife(10f);
        }

        private void FixedUpdate()
        {
            if (Mathf.Abs(rb.velocity.y) > Tolerance)
                _vel.y += -8f * Time.deltaTime;
            else
            {
                if (Math.Abs(rb.velocity.y) < 0.001f)
                    if (!player.OnGround)
                        player.OnGround = true;
                _vel.y = 0f;
                if (player.IsJumping)
                    player.IsJumping = false;
            }

            if (Input.GetKey(KeyCode.D) && (!player.IsCrouch || player.IsCrouch && player.IsWalk))
            {
                MoveRight();
            }
            
            if (Input.GetKey(KeyCode.A) && (!player.IsCrouch || player.IsCrouch && player.IsWalk))
            {
                MoveLeft();
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                _vel.x = 0f;
                player.IsWalk = false;
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                player.IsCrouch = !player.IsCrouch;
            }

            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_vel.y) < Tolerance)
            {
                _vel.y = player.JumpForce;
                player.IsJumping = true;
                player.OnGround = false;
                player.IsCrouch = false;
            }

            rb.velocity = _vel;
            an.SetBool(OnGround, player.OnGround);
            an.SetBool(Jumping, player.IsJumping);
            an.SetBool(Walk, player.IsWalk);
            an.SetBool(Crouch, player.IsCrouch);
        }

        private void MoveRight()
        {
            player.IsWalk = !player.IsJumping;
            _vel.x = (!player.IsArmed?player.WalkSpeed:player.ArmedWalkSpeed);
            FlipX(false);
        }

        private void MoveLeft()
        {
            player.IsWalk = !player.IsJumping;
            _vel.x = -(!player.IsArmed?player.WalkSpeed:player.ArmedWalkSpeed);
            FlipX(true);
        }

        private void FlipX(bool flip)
        {
            sp.flipX = flip;
        }
    }
}