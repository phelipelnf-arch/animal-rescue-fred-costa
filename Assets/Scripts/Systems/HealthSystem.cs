using UnityEngine;
using System;

/// <summary>
/// Gerencia saúde de uma entidade (jogador ou animal).
/// Controla dano, cura e morte.
/// </summary>
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private bool destroyOnDeath = false;

    private int currentHealth;
    private bool isDead = false;

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;
    public event Action OnHeal;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;
    public bool IsDead => isDead;
    public float HealthPercentage => (float)currentHealth / maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Aplica dano à entidade
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Debug.Log($"{gameObject.name} tomou {damage} de dano. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Cura a entidade
    /// </summary>
    public void Heal(int amount)
    {
        if (isDead) return;

        int oldHealth = currentHealth;
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth > oldHealth)
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            OnHeal?.Invoke();
            Debug.Log($"{gameObject.name} foi curado em {amount}. Vida: {currentHealth}/{maxHealth}");
        }
    }

    /// <summary>
    /// Mata a entidade
    /// </summary>
    public void Die()
    {
        if (isDead) return;

        isDead = true;
        currentHealth = 0;
        OnDeath?.Invoke();

        Debug.Log($"{gameObject.name} morreu!");

        if (destroyOnDeath)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Restaura a saúde completa
    /// </summary>
    public void FullHeal()
    {
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    /// <summary>
    /// Define a saúde máxima
    /// </summary>
    public void SetMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }
}