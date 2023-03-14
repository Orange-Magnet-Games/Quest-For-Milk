using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    CharacterController3D player;
    Animator anim;
    private void Start()
    {
        player = CharacterController3D.instance;
        anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Jump() 
    { 
        player.AnimationJump();
        anim.SetBool("Jumping", false);
    }
    void AttackOver()
    {
        player.isAttacking = false;
    }
}
