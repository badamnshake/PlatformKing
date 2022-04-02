using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimParams
{
    // public const string JumpKeyName = "stopVelocity";
    // public const string AxisXInput = "Horizontal";
    public static readonly int IsRunning = Animator.StringToHash("isRunning");
    public static readonly int IsOnGround = Animator.StringToHash("isOnGround");
    public static readonly int IsWallSliding = Animator.StringToHash("isWallSliding");
}