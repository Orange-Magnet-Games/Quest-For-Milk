using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartUI : MonoBehaviour
{
    public List<Animator> anims;
    HealthHolder playerHealth;
    private void Start()
    {
        playerHealth = CharacterController3D.instance.GetComponent<HealthHolder>();
    }
    // Update is called once per frame
    void Update()
    {
        int hlf = playerHealth.health;
        foreach(var anim in anims)
        {
            if (hlf > 0) anim.SetBool("Heart", true);
            else anim.SetBool("Heart", false);
            hlf--;
        }
    }
}
