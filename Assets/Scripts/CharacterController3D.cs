using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class CharacterController3D : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    InputMaster input;

    public float turnSmoothTime, speed, jumpPower;
    private float turnSmoothVelocity, angle;
    private Vector3 moveDir, direction;
    public bool isGrounded = true;

    private Transform cam;

    private ParticleSystem walkDust;

    #region Setup

    public static CharacterController3D instance;
    
    private void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
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
        walkDust = GetComponentInChildren<ParticleSystem>();
        cam = CameraManager.instance.gameCam.gameObject.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    void Update()
    {
        direction = new Vector3(input.Player.Move.ReadValue<Vector2>().x, input.Player.Jump.triggered ? 1 : 0, input.Player.Move.ReadValue<Vector2>().y);
        Movement();
        Jump();
        ParticleControl();
        

    }
    void Jump()
    {
        if(isGrounded && direction.y == 1)
        {
            rb.velocity += Vector3.up * jumpPower;
            isGrounded = false;
        }
    }
    void Movement()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        }
        transform.eulerAngles = new Vector3(0, angle, 0);
        rb.velocity = Drag(rb.velocity, .1f);
    }
    void ParticleControl()
    {
        if (rb.velocity.magnitude >= .1f && isGrounded) { if(!walkDust.isPlaying) walkDust.Play(); }
        else walkDust.Stop();
    }

    Vector3 Drag(Vector3 vel, float drag)
    {
        
        drag = 1 - drag;
        vel = new Vector3(vel.x * drag, vel.y, vel.z * drag);
        if (vel.x < drag && vel.x > -drag) vel = new Vector3(0, vel.y, vel.z);
        if (vel.z < drag && vel.z > -drag) vel = new Vector3(vel.x, vel.y, 0);
        return vel;
        
    }
}
