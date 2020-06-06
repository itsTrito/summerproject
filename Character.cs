using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    public bool IsWalk;
    public bool IsArmed;
    public bool IsJumping;

    public float WalkSpeed;
    public float ArmedWalkSpeed;

    public float life;

    public float jumpForce;

    public Character(float walkSpeed, float armedWalkSpeed)
    {
        this.WalkSpeed = walkSpeed;
        this.ArmedWalkSpeed = armedWalkSpeed;
        this.IsWalk = false;
        this.IsArmed = false;
    }

    public void setLife(float life)
    {
        this.life = life;
    }

    public void setJumpForce(float force)
    {
        this.jumpForce = force;
    }
}
