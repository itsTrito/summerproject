using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyPlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sp;

    private Vector2 _vel;
    
    public Character player;
    
    private void Start()
    {
        player = new Character(4f, 2f);
        player.setLife(10f);
        player.setJumpForce(2f);
    }

    private void FixedUpdate()
    {
        if (rb.velocity.y != 0f)
            _vel.y += -1f * Time.deltaTime;
        else
        {
            _vel.y = 0f;
            player.IsJumping = false;
        }

        if (Input.GetKey(KeyCode.D) && !player.IsJumping)
        {
            player.IsWalk = true;
            _vel.x = (!player.IsArmed?player.WalkSpeed:player.ArmedWalkSpeed);
            FlipX(false);
        }

        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
        {
            _vel.x = 0f;
            player.IsWalk = false;
        }

        if ((Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D)) && !player.IsJumping)
        {
            player.IsWalk = true;
        }

        if (Input.GetKey(KeyCode.A) && !player.IsJumping)
        {
            player.IsWalk = true;
            _vel.x = -(!player.IsArmed?player.WalkSpeed:player.ArmedWalkSpeed);
            FlipX(true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && _vel.y == 0f)
        {
            _vel.y = player.jumpForce;
            player.IsWalk = false;
            player.IsJumping = true;
        }

        rb.velocity = _vel;
        //if walk play animation
    }

    private void FlipX(bool flip)
    {
        sp.flipX = flip;
    }
}