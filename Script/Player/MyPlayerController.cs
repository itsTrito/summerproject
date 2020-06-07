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
        private static readonly int Armed = Animator.StringToHash("Armed");

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

            if (Input.GetKeyDown(KeyCode.Q) && !player.IsJumping && !player.IsCrouch && !player.IsWalk)
            {
                player.IsArmed = !player.IsArmed;
            }

            if (Input.GetKey(KeyCode.D) && (!player.IsCrouch || player.IsCrouch && player.IsWalk))
            {
                _vel.x = Move(false);
            }
            
            if (Input.GetKey(KeyCode.A) && (!player.IsCrouch || player.IsCrouch && player.IsWalk))
            {
                _vel.x = -Move(true);
            }

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                _vel.x = 0f;
                player.IsWalk = false;
            }

            if (Input.GetKeyDown(KeyCode.LeftControl) && !player.IsArmed)
            {
                player.IsCrouch = true;
            }

            if (Input.GetKeyUp(KeyCode.LeftControl) && !player.IsArmed)
            {
                player.IsCrouch = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(_vel.y) < 0.001f)
            {
                _vel.y = Jump();
            }

            rb.velocity = _vel;
            an.SetBool(OnGround, player.OnGround);
            an.SetBool(Jumping, player.IsJumping);
            an.SetBool(Walk, player.IsWalk);
            an.SetBool(Crouch, player.IsCrouch);
            an.SetBool(Armed, player.IsArmed);
        }

        private float Move(bool flip)
        {
            player.IsWalk = !player.IsJumping;
            FlipX(flip);
            return (!player.IsArmed?player.WalkSpeed:player.ArmedWalkSpeed);
        }

        private float Jump()
        {
            player.IsJumping = true;
            player.OnGround = false;
            player.IsCrouch = false;
            return player.JumpForce / (player.IsArmed?1.5f:1f);
        }

        private void FlipX(bool flip)
        {
            sp.flipX = flip;
        }
    }
}