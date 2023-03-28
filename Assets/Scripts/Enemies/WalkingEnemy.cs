using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class Enemy : MonoBehaviour
{
    public GameObject heart;
    public bool seen, isGrounded, isAttacking;
    private void OnDestroy()
    {
        if (Random.Range(0, 101) < 25)
        {
            GameObject instance = Instantiate(heart);
            instance.transform.position = transform.position;
            heart.GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(0f, 5f), 5f, Random.Range(0f, 5f));
        }
    }
}
public class WalkingEnemy : Enemy
{
    private Rigidbody rb;
    private Animator anim;
    private ParticleSystem walkDust;


    public float turnSmoothTime, speed, jumpPower;
    private float turnSmoothVelocity, angle;
    private Vector3 moveDir, direction, displacement;

    public int attackCooldown; //in frames
    int attackCount;
    
    bool attackSide = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        walkDust = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (seen)
        {
            displacement = (CharacterController3D.instance.transform.position - transform.position).normalized;
            direction = new Vector3(displacement.x, 0, displacement.z);
            Movement();
            Attack();
        }
        else
        {
            direction = Vector3.zero;
        }
        Animate();
        ParticleControl();
    }
    void Animate()
    {
        anim.SetBool("Running", direction.magnitude >= 0.1f);
        anim.SetBool("IsGrounded", isGrounded);
    }


    void Attack()
    {
        if (!isAttacking && Vector3.Distance(transform.position, CharacterController3D.instance.transform.position) < 5)
        {
            attackCount--;
            if (attackCount <= 0)
            {
                attackCount = attackCooldown;
                isAttacking = true;
                if (attackSide) anim.SetTrigger("AttackLeft");
                else anim.SetTrigger("AttackRight");
                attackSide = !attackSide;
            }
        }
    }




    public void AnimationJump()
    {
        rb.velocity += Vector3.up * jumpPower;
        isGrounded = false;
    }


    void Movement()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + displacement.y;
            angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            moveDir = Quaternion.Euler(0, angle, 0) * Vector3.forward;

            rb.velocity = new Vector3(moveDir.x * speed, rb.velocity.y, moveDir.z * speed);

        }
        transform.eulerAngles = new Vector3(0, angle, 0);
        rb.velocity = Drag(rb.velocity, .1f);
    }


    void ParticleControl()
    {
        if (rb.velocity.magnitude >= .1f && isGrounded) { if (!walkDust.isPlaying) walkDust.Play(); }
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
