# 🏗️ Arquitetura do Sistema
## Animal Rescue: Fred Costa

---

## 1. Visão Geral da Arquitetura

```
┌─────────────────────────────────────────────────┐
│           Camada de Apresentação (UI)           │
│  MenuController | HUDController | UIManager     │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│        Camada de Gerenciamento de Jogo         │
│  GameManager | LevelManager | SceneManager     │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│           Camada de Lógica de Jogo             │
│  RescueSystem | InventorySystem | HealthSystem │
└────────────────────┬────────────────────────────┘
                     │
┌────────────────────▼────────────────────────────┐
│         Camada de Entidades e Comportamentos   │
│  PlayerController | AnimalBase | ObstacleBase  │
└─────────────────────────────────────────────────┘
```

---

## 2. Componentes Principais

### 2.1 GameManager (Singleton)
**Responsabilidade**: Gerenciar estado global do jogo

```csharp
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    public GameState CurrentState { get; private set; }
    public int CurrentLevelIndex { get; private set; }
    public PlayerStats PlayerStats { get; private set; }
    
    public void ChangeGameState(GameState newState) { }
    public void LoadLevel(int levelIndex) { }
    public void RestartLevel() { }
}

public enum GameState
{
    Menu,
    Loading,
    Playing,
    Paused,
    LevelComplete,
    GameOver
}
```

### 2.2 LevelManager
**Responsabilidade**: Gerenciar dinâmica do nível atual

```csharp
public class LevelManager : MonoBehaviour
{
    public LevelData CurrentLevelData { get; private set; }
    public int AnimalsRescued { get; private set; }
    public int TotalAnimals { get; private set; }
    public float LevelTimeElapsed { get; private set; }
    
    public void RegisterAnimalRescued(Animal animal) { }
    public void OnLevelComplete() { }
    public void OnPlayerDeath() { }
}
```

### 2.3 PlayerController
**Responsabilidade**: Controlar movimento e ações do jogador

```csharp
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    private CharacterController characterController;
    private Animator animator;
    private HealthSystem healthSystem;
    
    public void Move(Vector2 input) { }
    public void Jump() { }
    public void Interact() { }
    public void TakeDamage(int damage) { }
}
```

### 2.4 HealthSystem
**Responsabilidade**: Gerenciar saúde de Fred e animais

```csharp
public class HealthSystem : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
    
    public event Action OnHealthChanged;
    public event Action OnDeath;
    
    public void TakeDamage(int damage) { }
    public void Heal(int amount) { }
    public float GetHealthPercentage() => (float)currentHealth / maxHealth;
}
```

### 2.5 InventorySystem
**Responsabilidade**: Gerenciar itens do jogador

```csharp
public class InventorySystem : MonoBehaviour
{
    private Dictionary<ItemType, int> items = new();
    
    public void AddItem(ItemType type, int quantity) { }
    public bool UseItem(ItemType type) { }
    public int GetItemQuantity(ItemType type) { }
}

public enum ItemType
{
    Bandage,
    HealthPotion,
    AnimalFood,
    Key,
    Ladder
}
```

### 2.6 RescueSystem
**Responsabilidade**: Gerenciar lógica de resgate de animais

```csharp
public class RescueSystem : MonoBehaviour
{
    public void AttemptRescue(Animal animal, PlayerController player) { }
    public void HealAnimal(Animal animal, ItemType itemType) { }
    public void CompleteRescue(Animal animal) { }
}
```

### 2.7 Animal (Base Class)
**Responsabilidade**: Definir comportamento base dos animais

```csharp
public abstract class Animal : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 50;
    protected int currentHealth;
    
    [SerializeField] protected AnimalType animalType;
    public AnimalState CurrentState { get; protected set; }
    
    public abstract void React(PlayerController player);
    public virtual void TakeDamage(int damage) { }
    public virtual void Heal(int amount) { }
}

public enum AnimalType { Dog, Cat }
public enum AnimalState { Scared, Injured, Calm, Rescued }
```

### 2.8 Dog & Cat Classes
**Responsabilidade**: Implementar comportamentos específicos

```csharp
public class Dog : Animal
{
    public override void React(PlayerController player)
    {
        // Latir, correr, ficar amigável
    }
}

public class Cat : Animal
{
    public override void React(PlayerController player)
    {
        // Miar, fugir, ficar defensivo
    }
}
```

### 2.9 ObstacleBase
**Responsabilidade**: Definir comportamento base dos obstáculos

```csharp
public abstract class ObstacleBase : MonoBehaviour
{
    public abstract void OnPlayerCollision(PlayerController player);
}
```

### 2.10 UIManager
**Responsabilidade**: Gerenciar toda a interface do usuário

```csharp
public class UIManager : MonoBehaviour
{
    public void UpdateHealthBar(float healthPercent) { }
    public void UpdateInventoryDisplay(InventorySystem inventory) { }
    public void ShowGameOverScreen() { }
    public void ShowLevelCompleteScreen(LevelResult result) { }
}
```

---

## 3. Padrões de Design Utilizados

### 3.1 Singleton
- **GameManager**: Instância única do gerenciador de jogo
- **UIManager**: Instância única do gerenciador de UI

### 3.2 Observer Pattern
- **Events**: Mudanças de saúde, resgate de animal, morte
- Permite comunicação frouxa entre sistemas

### 3.3 Strategy Pattern
- **Animal Behaviors**: Diferentes estratégias por tipo de animal
- **Obstacle Handlers**: Diferentes respostas a obstáculos

### 3.4 Factory Pattern
- **AnimalFactory**: Criação de animais com variações
- **ItemFactory**: Criação de itens

### 3.5 Object Pooling
- **BulletPool**: Reutilização de objetos de projéteis
- **EffectsPool**: Reutilização de efeitos visuais

---

## 4. Fluxo de Dados

```
Input (Jogador)
      ↓
PlayerController
      ↓
┌─────────────────────┐
│ Movement Logic      │
│ Interaction Logic   │
│ Combat Logic        │
└────────┬────────────┘
         ↓
    GameObject Transforms
    Animations
    Physics
         ↓
    Systems (Health, Inventory, Rescue)
         ↓
    GameManager
         ↓
    UIManager → Tela
```

---

## 5. Estrutura de Pastas

```
Assets/
├── Scripts/
│   ├── Core/
│   │   ├── GameManager.cs
│   │   ├── LevelManager.cs
│   │   └── GameState.cs
│   ├── Player/
│   │   ├── PlayerController.cs
│   │   ├── PlayerAnimations.cs
│   │   └── PlayerInput.cs
│   ├── Animals/
│   │   ├── Animal.cs
│   │   ├── Dog.cs
│   │   ├── Cat.cs
│   │   └── AnimalBehavior.cs
│   ├── Systems/
│   │   ├── HealthSystem.cs
│   │   ├── InventorySystem.cs
│   │   ├── RescueSystem.cs
│   │   └── AudioManager.cs
│   ├── UI/
│   │   ├── UIManager.cs
│   │   ├── HUDController.cs
│   │   ├── MenuController.cs
│   │   └── Panels/
│   ├── Obstacles/
│   │   ├── ObstacleBase.cs
│   │   ├── Spike.cs
│   │   ├── MovingPlatform.cs
│   │   ├── Fire.cs
│   │   └── Enemy.cs
│   ├── Utils/
│   │   ├── Constants.cs
│   │   ├── Helpers.cs
│   │   ├── Enums.cs
│   │   └── Data/
│   └── Managers/
│       └── SceneManager.cs
│
├── Prefabs/
│   ├── Player/
│   ├── Animals/
│   ├── Obstacles/
│   └── UI/
│
├── Scenes/
│   ├── MainMenu.unity
│   ├── Level01.unity
│   ├── Level02.unity
│   └── Level03.unity
│
└── Resources/
    ├── Data/
    │   └── Levels/
    ├── Animations/
    ├── Materials/
    ├── Sounds/
    └── Textures/
```

---

## 6. Comunicação entre Sistemas

### Events Chave
```csharp
// GameManager
public event Action<GameState> OnGameStateChanged;
public event Action OnLevelLoaded;

// HealthSystem
public event Action<int> OnHealthChanged;
public event Action OnPlayerDeath;

// InventorySystem
public event Action<ItemType, int> OnItemAdded;
public event Action<ItemType> OnItemUsed;

// RescueSystem
public event Action<Animal> OnAnimalRescued;
public event Action OnAllAnimalsRescued;
```

---

**Versão**: 1.0  
**Data de Atualização**: Julho 2026