# 📖 Guia de Desenvolvimento
## Animal Rescue: Fred Costa

---

## 1. Configuração do Ambiente

### 1.1 Pré-requisitos
- Unity 2022 LTS ou superior
- .NET Framework 4.7.1+
- Visual Studio Code ou Visual Studio 2019+
- Git

### 1.2 Setup Inicial

1. Clone o repositório:
```bash
git clone https://github.com/phelipelnf-arch/animal-rescue-fred-costa.git
cd animal-rescue-fred-costa
```

2. Abra com Unity Hub ou navegue para a pasta e selecione com Unity

3. Espere o projeto ser importado (pode levar alguns minutos na primeira vez)

4. Configure o C# IDE em Edit → Preferences → External Tools

---

## 2. Convenções de Código

### 2.1 Nomenclatura

**Classes e Structs**
```csharp
public class PlayerController { }
public struct PlayerStats { }
public interface IInteractable { }
public enum GameState { }
```

**Métodos e Propriedades**
```csharp
public void UpdatePosition() { }
private float CalculateDamage() { }
public int HealthPoints { get; set; }
```

**Campos Privados**
```csharp
private float moveSpeed = 5f;
[SerializeField] private int maxHealth = 100;
[HideInInspector] private bool isJumping = false;
```

**Constantes**
```csharp
private const float GRAVITY = -9.81f;
private const int MAX_INVENTORY_SLOTS = 5;
```

### 2.2 Organização de Classes

```csharp
public class MyClass : MonoBehaviour
{
    // 1. Campos Serializáveis
    [SerializeField] private int fieldValue = 0;
    
    // 2. Campos Privados
    private int internalValue = 0;
    
    // 3. Propriedades
    public int PropertyValue { get; private set; }
    
    // 4. Events
    public event Action OnValueChanged;
    
    // 5. Lifecycle Methods (Awake, Start, Update, etc)
    private void Awake() { }
    private void Start() { }
    private void Update() { }
    
    // 6. Public Methods
    public void PublicMethod() { }
    
    // 7. Private Methods
    private void PrivateMethod() { }
}
```

### 2.3 Comentários

```csharp
// Use comentários para explicar O POR QUÊ, não o O QUÊ
// ❌ Ruim
// Incrementar contador
counter++;

// ✅ Bom
// Incrementar contador para rastrear número de tentativas do jogador
counter++;

// Use XML comments para métodos públicos
/// <summary>
/// Tira dano do jogador
/// </summary>
/// <param name="damage">Quantidade de dano</param>
public void TakeDamage(int damage) { }
```

---

## 3. Scripts Base - Como Criar

### 3.1 Criar um novo Script

1. Right-click na pasta de Scripts
2. Create → C# Script
3. Nomeie seguindo as convenções
4. Implemente a classe base apropriada

### 3.2 Template para PlayerBehavior

```csharp
using UnityEngine;

/// <summary>
/// Descreve a responsabilidade deste script
/// </summary>
public class MyBehavior : MonoBehaviour
{
    [SerializeField] private float value = 1f;
    
    private void Start()
    {
        // Inicialização
    }
    
    private void Update()
    {
        // Lógica de atualização
    }
    
    public void DoSomething()
    {
        // Implementação
    }
}
```

### 3.3 Template para Novo Sistema

```csharp
using UnityEngine;

/// <summary>
/// Gerencia [funcionalidade]
/// </summary>
public class MySystem : MonoBehaviour
{
    public static MySystem Instance { get; private set; }
    
    public event System.Action OnSystemEvent;
    
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    public void PublicMethod()
    {
        // Implementação
        OnSystemEvent?.Invoke();
    }
}
```

---

## 4. Fluxo de Desenvolvimento

### 4.1 Git Workflow

```bash
# Criar branch para feature
git checkout -b feature/player-movement

# Fazer commits regulares
git add Assets/Scripts/Player/PlayerController.cs
git commit -m "Add basic movement to PlayerController"

# Fazer push
git push origin feature/player-movement

# Criar Pull Request no GitHub
```

### 4.2 Commits Semânticos

```
feat: adiciona novo sistema de resgate
fix: corrige bug no movimento do jogador
docs: atualiza documentação de arquitetura
refactor: reorganiza estrutura do PlayerController
test: adiciona testes para HealthSystem
style: formata código de ObstacleBase
```

### 4.3 Branch Naming

```
feature/player-movement
feature/animal-rescue-system
fix/health-bar-bug
docs/update-gdd
refactor/inventory-system
```

---

## 5. Testando o Jogo

### 5.1 Play Mode Testing

1. Abra a cena desejada em `Assets/Scenes/`
2. Clique em Play (ou Ctrl+P)
3. Teste mecânicas e comportamentos
4. Use Console para logs de debug

### 5.2 Debug Output

```csharp
// Logs informativos
Debug.Log("Animal resgatado: " + animal.name);

// Logs de aviso
Debug.LogWarning("Jogador com vida baixa: " + currentHealth);

// Logs de erro
Debug.LogError("Falha ao carregar nível: " + levelName);

// Logs condicionais
if (debug) Debug.Log("Modo debug ativado");
```

### 5.3 Gizmos para Debug

```csharp
private void OnDrawGizmos()
{
    // Desenhar range de interação
    Gizmos.color = Color.green;
    Gizmos.DrawWireSphere(transform.position, interactionRange);
    
    // Desenhar path da patrulha
    Gizmos.color = Color.yellow;
    for (int i = 0; i < patrolPoints.Length - 1; i++)
    {
        Gizmos.DrawLine(patrolPoints[i], patrolPoints[i + 1]);
    }
}
```

---

## 6. Otimização e Performance

### 6.1 Boas Práticas

- Use Object Pooling para itens frequentes
- Evite usar `FindObjectOfType()` e `GetComponent()` em Loop
- Cache referências em Awake/Start
- Use `CompareTag()` ao invés de string comparison
- Evite alocações de memória em Update()

### 6.2 Profiler

1. Abra Window → Analysis → Profiler
2. Selecione CPU Usage para verificar bottlenecks
3. Memory para verificar vazamentos
4. GPU para verificar carga gráfica

### 6.3 Build Settings para Mobile

- Qualidade gráfica reduzida
- Texture compression
- Mesh compression
- Script optimization
- Build com IL2CPP

---

## 7. Assets e Importação

### 7.1 Modelos 3D
- Formato: FBX ou GLTF
- Escala: 1 Unity Unit = 1 metro
- Textura: formato PNG ou TGA

### 7.2 Áudio
- Formato: WAV (lossless) ou MP3/OGG (compressed)
- Mono para efeitos, Stereo para música
- Sample rate: 44.1kHz

### 7.3 Animações
- Crie Animator Controllers para cada personagem
- Use parameters para transições (int, float, bool)
- Organize estados em sub-layers

---

## 8. Problemas Comuns e Soluções

### 8.1 Objeto não se move
```csharp
// ✅ Correto: Usar Rigidbody
rigidbody.velocity = moveDirection * speed;

// ❌ Errado: Transformar durante FixedUpdate com Rigidbody
transform.position += moveDirection * speed * Time.deltaTime;
```

### 8.2 Evento nunca é chamado
```csharp
// Verificar se subscriber foi adicionado
OnEventHappened += MyMethod; // ✅ Adicionado
OnEventHappened?.Invoke(); // ✅ Chamado

// Debug
Debug.Log("Subscribers: " + OnEventHappened?.GetInvocationList().Length);
```

### 8.3 Script não aparece no Inspector
```csharp
// Garantir que classe herda de MonoBehaviour
public class MyScript : MonoBehaviour { } // ✅ Correto

// Ou adicionar [SerializeField] para campos privados
[SerializeField] private int value = 0; // ✅ Visível
```

---

## 9. Recursos Úteis

- [Unity Manual](https://docs.unity3d.com/Manual/)
- [Unity API Reference](https://docs.unity3d.com/ScriptReference/)
- [C# Documentation](https://docs.microsoft.com/en-us/dotnet/csharp/)
- [Best Practices](https://unity.com/download)

---

**Versão**: 1.0  
**Data de Atualização**: Julho 2026  
**Mantido por**: Equipe de Desenvolvimento