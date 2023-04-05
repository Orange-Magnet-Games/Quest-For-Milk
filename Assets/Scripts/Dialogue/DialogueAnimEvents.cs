using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimEvents : MonoBehaviour
{
    Animator anim;
    public void KillYourself() => Destroy(gameObject);
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void SetCharacterAnim(int index)
    {
        anim.SetInteger("Animation", index);
    }
    public void DestroyCharacter()
    {
        Destroy(transform.parent.gameObject);
    }
}
