using UnityEngine;
using UnityEngine.Events;

public class StaminaComponent : MonoBehaviour
{
    public enum StaminaAddableState
    {
        ABLE,
        UNABLE
    }
    
    [Header("Stats")] 
    [Tooltip("The max amount of stamina allowed for this character.")]
    public float maxStamina;

    [Tooltip("The base amount of stamina that the character starts with.")]
    public float baseStamina;

    [Header("States")] 
    [Tooltip("Whether or not the character can gain stamina at the start of the game.")]
    public StaminaAddableState baseAddableState;
    
    [Tooltip("Event that gets called when stamina reaches 0")]
    public UnityEvent staminaDepletedEvent;
    [Tooltip("Event that gets called when stamina is gained")]
    public UnityEvent staminaGainEvent;
    [Tooltip("Event that gets called when stamina is lost")]
    public UnityEvent staminaLossEvent;
    
    private float currentStamina;
    private bool isStaminaAvailable;
    private StaminaAddableState currentState;
    
    // Start is called before the first frame update
    private void Start()
    {
        currentStamina = baseStamina;
        currentState = baseAddableState;
        isStaminaAvailable = true;
    }

    private void Update()
    {
        if (isStaminaAvailable && currentStamina <= 0)
        {
            isStaminaAvailable = false;
            
            staminaDepletedEvent.Invoke();
            
            Debug.Log($"{name} has run out of stamina!");
        }
    }

    /// <summary>
    /// This method tries to increase the amount of stamina that the character has.
    /// </summary>
    /// <param name="staminaToAdd">Amount of stamina to lose in float.</param>
    /// <returns>New amount of stamina after gaining staminaToAdd points.</returns>
    public float AddStamina(float staminaToAdd)
    {
        if (currentState == StaminaAddableState.ABLE)
        {
            if (currentStamina <= 0) isStaminaAvailable = true;
            
            if (currentStamina + staminaToAdd < maxStamina)
            {
                currentStamina += staminaToAdd;
            }
            else
            {
                currentStamina = maxStamina;
            }
        }

        staminaGainEvent.Invoke();
        Debug.Log($"{name} has gained stamina!");
        
        return currentStamina;
    }

    /// <summary>
    /// This method tries to decrease the amount of stamina that the character has.
    /// </summary>
    /// <param name="staminaToLose">Amount of stamina to lose in float.</param>
    /// <returns>New amount of stamina after losing staminaToLose points.</returns>
    public float LoseStamina(float staminaToLose)
    {
        var staminaAfterLoss = currentStamina - staminaToLose;
        if (staminaAfterLoss >= 0f)
        {
            currentStamina -= staminaToLose;
        }
        else
        {
            currentStamina = 0f;
        }
        
        staminaLossEvent.Invoke();
        Debug.Log($"{name} has lost stamina!");

        return currentStamina;
    }
}
