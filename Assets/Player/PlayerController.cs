using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        private int _amountOfJumpsLeft;
        private float _movementInputDirection;
        private bool _isFacingRight = true;
        private bool _isRunning = true;
        private bool _isGrounded;
        private bool _isTouchingWall;
        private bool _isWallSliding;
        private bool _canJump;


        private Rigidbody2D _rb;
        private Animator _anim;

        #region public variables

        // movement
        public int amountOfJumps = 2;
        public float movementSpeed = 10f;
        public float jumpForce = 16f;
        public float wallSlideSpeed = 1f;
        public float movementForceInAir = 5f;
        public float airDragMultiplier = 2f;
        public float variableJumpHeightMultiplier = 0.5f;


        // checks
        public Transform groundCheck;
        public Transform wallCheck;

        public float wallCheckDistance = 1.5f;
        public float groundCheckRadius = 0.4f;


        public LayerMask whatIsGround;
        public LayerMask whatIsWall;

        #endregion


        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _anim = GetComponent<Animator>();
            _amountOfJumpsLeft = amountOfJumps;
        }

        // Update is called once per frame
        void Update()
        {
            CheckInput();
            CheckMovementDirection();
            UpdateAnimations();
            CheckIfCanJump();
            CheckIfWallSliding();
        }

        private void FixedUpdate()
        {
            ApplyMovement();
            CheckSurroundings();
        }


        private void CheckSurroundings()
        {
            _isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
            _isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right,
                wallCheckDistance, whatIsWall);
        }

        private void CheckIfCanJump()
        {
            if (_isGrounded && _rb.velocity.y <= 0)
            {
                _amountOfJumpsLeft = amountOfJumps;
            }

            _canJump = _amountOfJumpsLeft > 0;
        }


        private void ApplyMovement()
        {
            if (_isGrounded)
                _rb.velocity = new Vector2(_movementInputDirection * movementSpeed, _rb.velocity.y);
            else if (!_isGrounded && !_isWallSliding && _movementInputDirection != 0)
            {
                Vector2 forceToAdd = new Vector2(movementForceInAir * _movementInputDirection, 0);
                _rb.AddForce(forceToAdd);

                if (Mathf.Abs(_rb.velocity.x) > movementSpeed)
                {
                    _rb.velocity = new Vector2(movementSpeed * _movementInputDirection, _rb.velocity.y);
                }
            }
            else if (!_isGrounded && !_isWallSliding && _movementInputDirection == 0)
            {
                var velocity = _rb.velocity;
                velocity = new Vector2(velocity.x * airDragMultiplier, velocity.y);
                _rb.velocity = velocity;
            }

            if (_isWallSliding)
            {
                if (_rb.velocity.y < -wallSlideSpeed)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, -wallSlideSpeed);
                }
            }
        }

        private void CheckMovementDirection()
        {
            if (_isFacingRight && _movementInputDirection < 0 || !_isFacingRight && _movementInputDirection > 0)
            {
                if (_isWallSliding) return;
                _isFacingRight = !_isFacingRight;
                transform.Rotate(0f, 180.0f, 0f);
            }

            _isRunning = _rb.velocity.x != 0;
        }

        private void UpdateAnimations()
        {
            _anim.SetBool(PlayerAnimParams.IsRunning, _isRunning);
            _anim.SetBool(PlayerAnimParams.IsOnGround, _isGrounded);
            _anim.SetBool(PlayerAnimParams.IsWallSliding, _isWallSliding);
        }

        private void CheckIfWallSliding()
        {
            _isWallSliding = _isTouchingWall && !_isGrounded && _rb.velocity.y < 0;
        }

        private void CheckInput()
        {
            _movementInputDirection = Input.GetAxisRaw("Horizontal");
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (Input.GetButtonUp("Jump"))
            {
                var velocity = _rb.velocity;
                velocity = new Vector2(velocity.x, velocity.y * variableJumpHeightMultiplier);
                _rb.velocity = velocity;
            }
            
        }

        private void Jump()
        {
            if (!_canJump) return;
            // _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            _amountOfJumpsLeft--;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
            var wallCheckPosition = wallCheck.position;
            var wallCheckDestination = new Vector2(
                _isFacingRight
                    ? wallCheckPosition.x + wallCheckDistance
                    : wallCheckPosition.x - wallCheckDistance, wallCheckPosition.y);
            Gizmos.DrawLine(wallCheckPosition, wallCheckDestination);
        }
    }
}