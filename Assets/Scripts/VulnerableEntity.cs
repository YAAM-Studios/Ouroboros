using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VulnerableEntity : MonoBehaviour
{
    // This will probably be how we give I-Frames
    public enum VulnerableState
    {
        VULNERABLE,
        INVULNERABLE
    }
    
    [Header("Stats")]
    [Tooltip("This character's current max health.")]
    public float maxHealth;
    
    [Tooltip("This character's base health (what they start at).")]
    public float baseHealth;
    
    private float currentHealth;

    [Header("States")] 
    [Tooltip("Whether or not this character starts off vulnerable or invulnerable.")]
    public VulnerableState startingVulnerableState;
    
    private VulnerableState vulnerableState;
    
    private bool isAlive;

    public UnityEvent deathEvent;
    public UnityEvent damageEvent;
    public UnityEvent healEvent;

    private void Start()
    {
        currentHealth = baseHealth;
        vulnerableState = startingVulnerableState;
        isAlive = true;
    }

    private void Update()
    {
        if (isAlive && currentHealth <= 0)
        {
            isAlive = false;
            
            // Notify listeners
            deathEvent.Invoke();
            
            Debug.Log($"{name} has died");
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (vulnerableState == VulnerableState.VULNERABLE)
        {
            currentHealth -= damageAmount;
            
            // Notify listeners
            damageEvent.Invoke();
            
            Debug.Log($"{name} has taken {damageAmount} damage");
        }
    }

    public void HealPoints(float healAmount)
    {
        if (currentHealth + healAmount <= maxHealth)
        {
            currentHealth += healAmount;
        }
        else
        {
            currentHealth = maxHealth;
        }
        
        // Notify listeners
        healEvent.Invoke();
        
        Debug.Log($"{name} has healed {healAmount} points");
    }
}
