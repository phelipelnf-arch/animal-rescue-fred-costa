using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// Gerencia a dinâmica do nível atual. Responsável por rastrear
/// animais, obstáculos e progresso do jogador no nível.
/// </summary>
public class LevelManager : MonoBehaviour
{
    [SerializeField] private LevelData levelData;
    [SerializeField] private float timeLimit = 300f;

    private int animalsRescued = 0;
    private float levelTimeElapsed = 0f;
    private bool levelComplete = false;
    private List<Animal> animalsInLevel = new();

    public event Action<int, int> OnAnimalRescueCountChanged;
    public event Action<float> OnTimeChanged;
    public event Action OnLevelComplete;
    public event Action OnLevelFailed;

    public int AnimalsRescued => animalsRescued;
    public int TotalAnimals => animalsInLevel.Count;
    public float LevelTimeElapsed => levelTimeElapsed;
    public bool LevelComplete => levelComplete;

    private void Start()
    {
        InitializeLevel();
        GameManager.Instance.ChangeGameState(GameState.Playing);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing)
            return;

        levelTimeElapsed += Time.deltaTime;

        if (timeLimit > 0 && levelTimeElapsed >= timeLimit)
        {
            FailLevel();
        }

        OnTimeChanged?.Invoke(levelTimeElapsed);
    }

    /// <summary>
    /// Inicializa o nível
    /// </summary>
    private void InitializeLevel()
    {
        animalsInLevel.AddRange(FindObjectsOfType<Animal>());
        Debug.Log($"Nível iniciado com {animalsInLevel.Count} animais");
    }

    /// <summary>
    /// Registra que um animal foi resgatado
    /// </summary>
    public void RegisterAnimalRescued(Animal animal)
    {
        if (animalsInLevel.Contains(animal))
        {
            animalsRescued++;
            OnAnimalRescueCountChanged?.Invoke(animalsRescued, TotalAnimals);

            Debug.Log($"Animal resgatado! ({animalsRescued}/{TotalAnimals})");

            if (animalsRescued >= TotalAnimals)
            {
                CompleteLevel();
            }
        }
    }

    /// <summary>
    /// Completa o nível com sucesso
    /// </summary>
    private void CompleteLevel()
    {
        if (levelComplete) return;

        levelComplete = true;
        GameManager.Instance.ChangeGameState(GameState.LevelComplete);
        OnLevelComplete?.Invoke();

        Debug.Log("Nível completado com sucesso!");
    }

    /// <summary>
    /// Falha no nível
    /// </summary>
    public void FailLevel()
    {
        GameManager.Instance.ChangeGameState(GameState.GameOver);
        OnLevelFailed?.Invoke();

        Debug.Log("Nível falhou!");
    }

    /// <summary>
    /// Calcula a pontuação do nível
    /// </summary>
    public int CalculateLevelScore()
    {
        int baseScore = animalsRescued * 100;
        float timeBonus = Mathf.Max(0, timeLimit - levelTimeElapsed);
        int timeScore = (int)(timeBonus * 10);

        return baseScore + timeScore;
    }

    /// <summary>
    /// Calcula as estrelas ganhas
    /// </summary>
    public int CalculateStars()
    {
        if (animalsRescued == 0) return 0;
        if (animalsRescued < TotalAnimals) return 1;
        if (levelTimeElapsed > timeLimit * 0.7f) return 2;
        return 3;
    }
}

/// <summary>
/// Dados do nível
/// </summary>
[System.Serializable]
public class LevelData
{
    public string levelName;
    public string levelDescription;
    public int targetAnimals;
    public float timeLimit;
    public GameDifficulty minimumDifficulty;
}