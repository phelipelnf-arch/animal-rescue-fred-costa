using UnityEngine;
using System;

/// <summary>
/// Classe base para todos os animais no jogo.
/// Define comportamentos e propriedades compartilhadas.
/// </summary>
public abstract class Animal : MonoBehaviour
{
    [Header("Configuração Base")]
    [SerializeField] protected AnimalType animalType;
    [SerializeField] protected string animalName = "Animal";
    [SerializeField] protected int maxHealth = 50;

    protected int currentHealth;
    protected AnimalState currentState = AnimalState.Scared;
    protected HealthSystem healthSystem;
    protected Animator animator;

    public event Action<AnimalState> OnStateChanged;
    public event Action OnRescued;

    public AnimalType Type => animalType;
    public string AnimalName => animalName;
    public AnimalState CurrentState => currentState;
    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public float HealthPercentage => (float)currentHealth / maxHealth;

    protected virtual void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        if (healthSystem == null)
        {
            healthSystem = gameObject.AddComponent<HealthSystem>();
        }
        healthSystem.SetMaxHealth(maxHealth);

        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    protected virtual void Start()
    {
        ChangeState(AnimalState.Scared);
    }

    /// <summary>
    /// Muda estado do animal
    /// </summary>
    protected void ChangeState(AnimalState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        OnStateChanged?.Invoke(newState);

        Debug.Log($"{animalName} mudou para estado: {newState}");
    }

    /// <summary>
    /// Reação ao jogador se aproximar
    /// </summary>
    public abstract void React(PlayerController player);

    /// <summary>
    /// Tira dano do animal
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        healthSystem?.TakeDamage(damage);
        currentHealth = healthSystem.CurrentHealth;
    }

    /// <summary>
    /// Cura o animal
    /// </summary>
    public virtual void Heal(int amount)
    {
        healthSystem?.Heal(amount);
        currentHealth = healthSystem.CurrentHealth;
    }

    /// <summary>
    /// Alimenta o animal
    /// </summary>
    public virtual void Feed()
    {
        ChangeState(AnimalState.Calm);
        Debug.Log($"{animalName} foi alimentado e está calmo");
    }

    /// <summary>
    /// Resgate do animal
    /// </summary>
    public virtual void Rescue()
    {
        ChangeState(AnimalState.Rescued);
        OnRescued?.Invoke();
        gameObject.SetActive(false);
        Debug.Log($"{animalName} foi resgatado com sucesso!");
    }
}

/// <summary>
/// Tipos de animais
/// </summary>
public enum AnimalType
{
    Dog,
    Cat
}

/// <summary>
/// Estados possíveis do animal
/// </summary>
public enum AnimalState
{
    Scared,
    Injured,
    Calm,
    Rescued
}