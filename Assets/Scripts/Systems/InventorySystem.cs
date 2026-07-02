using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Gerencia inventário do jogador.
/// Controla itens, quantidade e uso.
/// </summary>
public class InventorySystem : MonoBehaviour
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemType itemType;
        public int quantity;

        public InventorySlot(ItemType type, int qty)
        {
            itemType = type;
            quantity = qty;
        }
    }

    [SerializeField] private int maxSlots = 10;
    private Dictionary<ItemType, InventorySlot> items = new();

    public event Action<ItemType, int> OnItemAdded;
    public event Action<ItemType> OnItemUsed;
    public event Action OnInventoryChanged;

    public int MaxSlots => maxSlots;

    private void Awake()
    {
        InitializeInventory();
    }

    /// <summary>
    /// Inicializa inventário vazio
    /// </summary>
    private void InitializeInventory()
    {
        items.Clear();
    }

    /// <summary>
    /// Adiciona item ao inventário
    /// </summary>
    public bool AddItem(ItemType itemType, int quantity = 1)
    {
        if (items.Count >= maxSlots && !items.ContainsKey(itemType))
        {
            Debug.LogWarning("Inventário cheio!");
            return false;
        }

        if (items.ContainsKey(itemType))
        {
            items[itemType].quantity += quantity;
        }
        else
        {
            items[itemType] = new InventorySlot(itemType, quantity);
        }

        OnItemAdded?.Invoke(itemType, items[itemType].quantity);
        OnInventoryChanged?.Invoke();

        Debug.Log($"Item adicionado: {itemType} x{quantity}");
        return true;
    }

    /// <summary>
    /// Remove item do inventário
    /// </summary>
    public bool RemoveItem(ItemType itemType, int quantity = 1)
    {
        if (!items.ContainsKey(itemType))
        {
            return false;
        }

        items[itemType].quantity -= quantity;

        if (items[itemType].quantity <= 0)
        {
            items.Remove(itemType);
        }

        OnInventoryChanged?.Invoke();
        return true;
    }

    /// <summary>
    /// Usa um item
    /// </summary>
    public bool UseItem(ItemType itemType)
    {
        if (!HasItem(itemType))
        {
            Debug.LogWarning($"Item {itemType} não disponível");
            return false;
        }

        OnItemUsed?.Invoke(itemType);
        RemoveItem(itemType);

        return true;
    }

    /// <summary>
    /// Verifica se tem item
    /// </summary>
    public bool HasItem(ItemType itemType)
    {
        return items.ContainsKey(itemType) && items[itemType].quantity > 0;
    }

    /// <summary>
    /// Retorna quantidade de um item
    /// </summary>
    public int GetItemQuantity(ItemType itemType)
    {
        return items.ContainsKey(itemType) ? items[itemType].quantity : 0;
    }

    /// <summary>
    /// Limpa inventário
    /// </summary>
    public void ClearInventory()
    {
        items.Clear();
        OnInventoryChanged?.Invoke();
    }

    /// <summary>
    /// Retorna todos os itens
    /// </summary>
    public Dictionary<ItemType, InventorySlot> GetAllItems()
    {
        return new Dictionary<ItemType, InventorySlot>(items);
    }
}

/// <summary>
/// Tipos de itens disponíveis
/// </summary>
public enum ItemType
{
    Bandage,
    HealthPotion,
    AnimalFood,
    Key,
    Ladder
}