using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public Animator animator;
    private Rigidbody2D _rb;
    private Collider2D _collider;

    public ContactFilter2D groundCheckFilter;
    public float groundCheckDistance = 0.1f;
    public float wallRaycastDistance = 0.6f;

    private List<RaycastHit2D> groundHits = new List<RaycastHit2D>();
    private List<RaycastHit2D> wallHits = new List<RaycastHit2D>();


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxisRaw(PlayerAnimParams.AxisXInput);

        animator.SetFloat(PlayerAnimParams.MoveX, moveX);

        bool isMoving = !Mathf.Approximately(moveX, 0f);

        animator.SetBool(PlayerAnimParams.IsMoving, isMoving);

        bool lastOnGround = animator.GetBool(PlayerAnimParams.IsOnGround);
        bool newOnGround = CheckIfOnGround();
        animator.SetBool(PlayerAnimParams.IsOnGround, newOnGround);

        if (!lastOnGround && newOnGround)
        {
            animator.SetTrigger(PlayerAnimParams.LandedOnGround);
        }
    }

    // having everything related to physics in fixed
    private void FixedUpdate()
    {
        float forceX = animator.GetFloat(PlayerAnimParams.ForceX);
        if (forceX != 0) _rb.AddForce(new Vector2(forceX, 0) * Time.deltaTime);
        float impulseY = animator.GetFloat(PlayerAnimParams.ImpulseY);
        if (impulseY != 0) _rb.AddForce(new Vector2(0, impulseY), ForceMode2D.Impulse);
    }

    bool CheckIfOnGround()
    {
        // cast takes the whole rigid body's copy and puts it onto object to check collide
        _collider.Cast(Vector2.down, groundCheckFilter, groundHits, groundCheckDistance);
        return groundHits.Count > 0;
    }

    bool CheckIfOnWall()
    {
        Vector2 localScale = transform.localScale;
        // raycast takes middle of the character and parses it in the left and right direction
        _collider.Raycast(Mathf.Sign(localScale.x) * Vector2.right, groundCheckFilter, wallHits, wallRaycastDistance);
        return wallHits.Count > 0;
    }
}