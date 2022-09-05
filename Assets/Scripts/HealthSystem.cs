using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float health;
    [SerializeField] float maxHealth;
    [SerializeField] float regenAmount;
    [SerializeField] float regenDelay;
    [SerializeField] float criticState;

    [SerializeField] HeartBeatAudioManager heartBeat;
    [SerializeField] Image healthBar;

    [SerializeField] Animator animator;

    private float LastTimeHit;

    private float lerpSpeed;

    private void Update()
    {
        lerpSpeed = 3f * Time.deltaTime;
        HealthBarFiller();
        ColorBarChanger();

        if (health <= 0)
        {
            //GameOver
            return;
        }
        if(health > maxHealth)
        {
            // gérer la décroissance au cours du temps du surplus 
            return;
        }

        if (LastTimeHit + regenDelay < Time.time)
        {
            LastTimeHit = Time.time;
            health += regenAmount;
        }
    }
    public void TakeDamage(int amount)
    {
        animator.SetTrigger("Hurt");
        LastTimeHit = Time.time;
        if(health - amount < 0)
        {
            health = 0;
        }
        else
        {
            health -= amount;
        }

        if (!heartBeat.active && health/maxHealth <= criticState)
        {
            heartBeat.ActivateHeartBeat();
        }
    }

    public void Heal(int amount)
    {
        health += amount;
        if(heartBeat.active && health / maxHealth > criticState)
        {
            heartBeat.DesactivateHeartBeat();
        }
    }

    private void HealthBarFiller()
    {
        healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, health/maxHealth, lerpSpeed);
    }

    private void ColorBarChanger()
    {
        Color healthColor = Color.Lerp(Color.red, Color.green, (health / maxHealth));
        healthBar.color = healthColor;
    }
}
