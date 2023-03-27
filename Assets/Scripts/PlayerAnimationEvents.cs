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
    }
    void AttackOver()
    {
        if(player) player.isAttacking = false;
        if(enemy) enemy.isAttacking = false;
    }
    void Update()
    {
        
    }
}
