using UnityEngine;
using System;

/// <summary>
/// Gerenciador central do jogo. Responsável por gerenciar estado global,
/// transições de cena e dados persistentes.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameDifficulty currentDifficulty = GameDifficulty.Normal;
    
    private GameState currentState = GameState.Menu;
    private int currentLevelIndex = 0;

    public event Action<GameState> OnGameStateChanged;
    public event Action OnLevelLoaded;

    public GameState CurrentState => currentState;
    public int CurrentLevelIndex => currentLevelIndex;
    public GameDifficulty CurrentDifficulty => currentDifficulty;

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

    private void Start()
    {
        ChangeGameState(GameState.Menu);
    }

    /// <summary>
    /// Muda o estado do jogo
    /// </summary>
    public void ChangeGameState(GameState newState)
    {
        if (currentState == newState) return;

        currentState = newState;
        OnGameStateChanged?.Invoke(newState);

        switch (newState)
        {
            case GameState.Playing:
                Time.timeScale = 1f;
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                break;
            case GameState.Loading:
                Time.timeScale = 0f;
                break;
        }
    }

    /// <summary>
    /// Carrega um nível pelo índice
    /// </summary>
    public void LoadLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;
        ChangeGameState(GameState.Loading);
        UnityEngine.SceneManagement.SceneManager.LoadScene($"Level{levelIndex:D2}");
    }

    /// <summary>
    /// Reinicia o nível atual
    /// </summary>
    public void RestartLevel()
    {
        ChangeGameState(GameState.Loading);
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    /// <summary>
    /// Volta ao menu principal
    /// </summary>
    public void GoToMainMenu()
    {
        ChangeGameState(GameState.Menu);
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    /// <summary>
    /// Define a dificuldade do jogo
    /// </summary>
    public void SetDifficulty(GameDifficulty difficulty)
    {
        currentDifficulty = difficulty;
    }

    /// <summary>
    /// Pausa ou despausa o jogo
    /// </summary>
    public void TogglePause()
    {
        if (currentState == GameState.Playing)
        {
            ChangeGameState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            ChangeGameState(GameState.Playing);
        }
    }
}

/// <summary>
/// Estados possíveis do jogo
/// </summary>
public enum GameState
{
    Menu,
    Loading,
    Playing,
    Paused,
    LevelComplete,
    GameOver
}

/// <summary>
/// Níveis de dificuldade
/// </summary>
public enum GameDifficulty
{
    Easy,
    Normal,
    Hard
}