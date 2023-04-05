using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class CharacterController3D : MonoBehaviour
{
    //Attached Components
    private Rigidbody rb;
    private InputMaster input;
    private Animator anim;
    private HealthHolder health;

    //Movement Maths
    public float turnSmoothTime, speed, jumpPower;
    private float turnSmoothVelocity, angle;
    private Vector3 moveDir; 
    [HideInInspector] public Vector3 direction;
    public Vector3 lastWalked;
    private bool _isGrounded = true;
    public bool IsGrounded
    {
        get { return _isGrounded; }
        set
        {
            if (value != _isGrounded)
            {
                _isGrounded = value;
                if(_isGrounded)
                {
                    CameraManager.instance.soundMan.Land(1);
                }
            }
        }
    }
    //Attacks
    public bool isAttacking = false;
    bool attackSide = false;

    //Camera
    private Transform cam;

    //Particle Effects
    private ParticleSystem walkDust;

    public GameObject shield;

    #region Setup
    //Singletonification
    public static CharacterController3D instance;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void OnEnable()
    {
        input = new();
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
    #endregion

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        walkDust = GetComponentInChildren<ParticleSystem>();
        cam = CameraManager.instance.gameCam.gameObject.transform;
        rb = GetComponent<Rigidbody>();
        health = GetComponent<HealthHolder>();
    }

    void Update()
    {
        if (health.invCount > 0 && !shield.activeSelf) shield.SetActive(true);
        else if (health.invCount <= 0 && shield.activeSelf) shield.SetActive(false);
        direction = new Vector3(input.Player.Move.ReadValue<Vector2>().x, input.Player.Jump.triggered ? 1 : 0, input.Player.Move.ReadValue<Vector2>().y);
        Movement();
        Jump();
        Animate();
        ParticleControl();
        Attack();
        if (transform.position.y < -100)
        {
            rb.velocity = Vector3.zero;
            transform.position = lastWalked;
        }

    }


    void Animate()
    {
        anim.SetBool("Running", rb.velocity.magnitude > 0.1);
        anim.SetBool("IsGrounded", IsGrounded);
    }


    void Attack()
    {
        if(!isAttacking && input.Player.Attack.triggered)
        {
            isAttacking = true;
            if (attackSide) anim.SetTrigger("AttackLeft");
            else anim.SetTrigger("AttackRight");
            attackSide = !attackSide;
        }
    }


    void Jump()
    {
        if (IsGrounded && direction.y == 1)
        {
            anim.SetBool("Jumping", true);
        }
    }


    public void AnimationJump()
    {
        rb.velocity += Vector3.up * jumpPower;
        IsGrounded = false;
    }


    void Movement()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            rb.velocity += new Vector3(moveDir.x * speed, 0, moveDir.z * speed);

        }
        transform.eulerAngles = new Vector3(0, angle, 0);
        rb.velocity = Drag(rb.velocity, .9f);
    }


    void ParticleControl()
    {
        if (rb.velocity.magnitude >= .1f && IsGrounded) { if(!walkDust.isPlaying) walkDust.Play(); }
        else walkDust.Stop();
    }

    Vector3 Drag(Vector3 vel, float drag)
    {

        float y = vel.y;
        vel *= drag;
        vel = new Vector3(vel.x, y, vel.z);
        return vel;
        
    }
}
