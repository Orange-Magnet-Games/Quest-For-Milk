using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    CharacterController3D player;
    Enemy enemy;
    Animator anim;
    private void Start()
    {
        player = CharacterController3D.instance;
        enemy = GetComponentInParent<Enemy>();
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Jump() 
    { 
        player.AnimationJump();
        anim.SetBool("Jumping", false); 
        CameraManager.instance.soundMan.Jump(1);
    }
    void AttackOver()
    {
        player.isAttacking = false;
    }
    public void Step()
    {
        CameraManager.instance.soundMan.Step(1);
    }
    public void Swing()
    {
        CameraManager.instance.soundMan.Swing(1);
    }
}
