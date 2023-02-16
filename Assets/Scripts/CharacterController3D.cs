using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class CharacterController3D : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    InputMaster input;
    public float turnSmoothTime, speed;
    private float turnSmoothVelocity, angle;
    private Vector3 moveDir;
    private Transform cam;
    void OnEnable()
    {
        input = new();
        input.Enable();
    }
    void OnDisable()
    {
        input.Disable();
    }
    void Start()
    {
        cam = CameraManager.instance.gameCam.gameObject.transform;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    void Update()
    {
        Vector3 direction = new Vector3(input.Player.Move.ReadValue<Vector2>().x, 0, input.Player.Move.ReadValue<Vector2>().y); //get directional input from player and assign it to the right axes
        Debug.Log(direction.x);
        
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            
            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        }
        transform.eulerAngles = new Vector3(0, angle, 0);
        rb.velocity *= .9f;

    }
}
