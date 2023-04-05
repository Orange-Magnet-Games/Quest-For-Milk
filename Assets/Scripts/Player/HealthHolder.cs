using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthHolder : MonoBehaviour
{

    public int health;
    public int invincibilityFrames;
    [HideInInspector] public int invCount;

    public void TakeDamage(int damage)
    {
        if (invCount <= 0)
        {
            health -= damage;
            invCount = invincibilityFrames;
            if(health <= 0)
            {
                if (GetComponent<CharacterController3D>()) SceneManager.LoadScene(0);
                if (TryGetComponent<Enemy>(out Enemy enem)) enem.Die();
            }
        }
    }
    private void FixedUpdate()
    {
        if (invCount >= 0) invCount--;
    }
}
