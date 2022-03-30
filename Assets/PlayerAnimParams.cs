using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerAnimParams
{
    public const string JumpKeyName = "stopVelocity";
    public const string AxisXInput = "Horizontal";
    public static readonly int MoveX = Animator.StringToHash("moveX");
    public static readonly int IsMoving = Animator.StringToHash("isMoving");
    public static readonly int ForceX = Animator.StringToHash("forceX");
    public static readonly int ImpulseY = Animator.StringToHash("impulseY");
    public static readonly int VelocityY = Animator.StringToHash("velocityY");
    public static readonly int IsOnGround = Animator.StringToHash("isOnGround");
    public static readonly int IsOnWall = Animator.StringToHash("isOnWall");
    public static readonly int JumpTriggerName = Animator.StringToHash("jump");
    public static readonly int LandedOnGround = Animator.StringToHash("landedOnGround");
    public static readonly int HasDoubleJumped = Animator.StringToHash("hasDoubleJumped");
    public static readonly int StopVelocity = Animator.StringToHash("stopVelocity");
}