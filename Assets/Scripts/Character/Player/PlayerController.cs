using System;
using System.Collections;
using API;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : LivingEntity
{
    public static PlayerController Instance;
    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private Animator _animator;
    public Transform groundCheck;
    public bool isTalking = false;
    public LayerMask ground;

    private SpriteRenderer spriteRenderer;

    [Header("Move")] private float _moveH;

    private float _moveV;
    public float moveSpeed;

    [Header("Jump")] [SerializeField] private float jumpForce;
    private int _jumpTimes = 1;
    private readonly float _fallMultiplier = 5f;
    private readonly float _lowJumpMultiplier = 2f;

    [Header("Attack")] public GameObject attackPrefab;
    private Transform firePoint;
    private float attackTimer = 0f;
    public float fireRate;
    public int fireMaxSize;
    public int fireCurrentSize;
    [SerializeField] private Slider slider;

    [Space] [Header("Booleans")] public bool canMove;
    public bool doubleJump;

    public bool canShoot;
    // public bool wallGrab;
    // public bool wallJumped;
    // public bool wallSlide;
    // public bool isDashing;

    [Space] [SerializeField] private bool isGround;
    private bool _hasDashed;

    [HideInInspector] public string scenePassword = "01";

    //Animation
    private static readonly int Idling = Animator.StringToHash("idling");
    private static readonly int Running = Animator.StringToHash("running");
    private static readonly int Jumping = Animator.StringToHash("jumping");
    private static readonly int Falling = Animator.StringToHash("falling");

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    protected override void Start()
    {
        base.Start();
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck").transform;
        firePoint = transform.Find("FirePoint").transform;

        fireCurrentSize = fireMaxSize;
        slider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        if (!isTalking)
        {
            Jump();
            BetterJump();
            Shot();
        }
        SwitchAnim();
    }

    void FixedUpdate()
    {
        if (!isTalking)
        {
            PlayerMove();
            isGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
        }
        else
        {
            _rb.velocity = new Vector2(0, 0);
            _animator.SetFloat(Running, 0);
        }
    }

    void PlayerMove()
    {
        _moveH = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        _moveV = Input.GetAxis("Vertical");
        _rb.velocity = new Vector2(_moveH * moveSpeed, _rb.velocity.y);
        //change animation
        _animator.SetFloat(Running, Math.Abs(_moveH));
        // PlayerFlip();
    }

    //抓墙flip
    // void PlayerFlip(int side)
    // {
    //     if (wallGrab || wallSlide)
    //     {
    //         if (side == -1 && _sr.flipX)
    //             return;
    //
    //         if (side == 1 && !_sr.flipX)
    //         {
    //             return;
    //         }
    //     }
    //
    //     bool state = (side == 1) ? false : true;
    //     _sr.flipX = state;
    // }


    #region Jump

    void Jump() //Jump & Double Jump
    {
        if (isGround && doubleJump)
        {
            _jumpTimes = 2;
        }
        else if (isGround && !doubleJump)
        {
            _jumpTimes = 1;
        }

        if (isGround && Input.GetKeyDown(KeyCode.Space) && _jumpTimes == 1)
        {
            //jumpParticle.Play();
            //jumpAudio.Play();
            _jumpTimes--;
            _rb.velocity = Vector2.up * jumpForce;
            _animator.SetBool(Jumping, true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && _jumpTimes == 0)
        {
            _jumpTimes--;
            //jumpAudio.Play();
            _rb.velocity = Vector2.up * jumpForce;
            _animator.SetBool(Jumping, true);
        }
    }

    void BetterJump()
    {
        if (_rb.velocity.y < -1f)
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            _animator.SetBool(Falling, true);
        }
        else if (_rb.velocity.y > 0 && !Input.GetKeyDown(KeyCode.Space))
        {
            _rb.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    #endregion

    #region Attack

    void Shot()
    {
        if (canShoot && Input.GetMouseButtonDown(0) && Time.time > attackTimer && fireCurrentSize > 0)

        {
            fireCurrentSize--;
            attackTimer = Time.time + fireRate;
            Instantiate(attackPrefab, firePoint.position, transform.rotation);
            //stuffAudio.Play();
        }
    }

    public void LoadFireSize()
    {
        if (slider.value == slider.maxValue)
            fireCurrentSize = fireMaxSize;
    }

    #endregion

    void SwitchAnim()
    {
        if (_moveH < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else if (_moveH > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        //spriteRenderer.flipX = Physics2D.gravity.x > 0;
        // spriteRenderer.flipY = Physics2D.gravity.y > 0;

        //掉落
        //_animator.SetBool(Idling, false);
        if (_animator.GetBool(Jumping))
        {
            if (_rb.velocity.y < -1.0f)
            {
                _animator.SetBool(Jumping, false);
                _animator.SetBool(Falling, true);
            }
        }
        else if (isGround)
        {
            _animator.SetBool(Falling, false);
            _animator.SetBool(Idling, true);
        }

        //抓墙
        // if (_collision.onWall && canMove)
        // {
        //     if (side != _collision.wallSide)
        //         PlayerFlip(side * -1);
        //     wallGrab = true;
        //     wallSlide = false;
        // }
        //
        // if (wallGrab && !isDashing)
        // {
        //     _rb.gravityScale = 0;
        //     if (_moveH > .2f || _moveH < -.2f)
        //         _rb.velocity = new Vector2(_rb.velocity.x, 0);
        //     float speedModifier = _moveV > 0 ? .5f : 1;
        //     _rb.velocity = new Vector2(_rb.velocity.x, _moveV * (moveSpeed * speedModifier));
        // }
        // else
        // {
        //     _rb.gravityScale = 3;
        // }
    }

    //碰到敌人
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //消灭敌人
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (_animator.GetBool(Falling))
            {
                Console.WriteLine("JumpOn");
                enemy.JumpOn();
                _rb.velocity = Vector2.up * jumpForce / 3;
                _animator.SetBool(Jumping, true);
            }
        }
    }


}