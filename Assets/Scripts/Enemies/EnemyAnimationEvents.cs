using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationEvents : MonoBehaviour
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
    void Jump(float vol)
    {
        player.AnimationJump();
        anim.SetBool("Jumping", false);

        CameraManager.instance.soundMan.Jump(vol);
    }
    void AttackOver()
    {
        enemy.isAttacking = false;
    }
    public void Step(float vol)
    {
        CameraManager.instance.soundMan.Step(vol);
    }
    public void Swing(float vol)
    {
        CameraManager.instance.soundMan.Swing(vol);
    }
}
