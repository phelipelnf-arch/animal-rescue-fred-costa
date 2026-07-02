using UnityEngine;

/// <summary>
/// Constantes globais do jogo
/// </summary>
public static class Constants
{
    // Dano
    public const int SPIKE_DAMAGE = 20;
    public const int FALL_DAMAGE = 30;
    public const int ENEMY_DAMAGE = 15;

    // Cura
    public const int BANDAGE_HEAL = 20;
    public const int POTION_HEAL = 50;
    public const int FULL_HEAL = 999;

    // Pontuação
    public const int POINTS_PER_ANIMAL = 100;
    public const int TIME_BONUS_MULTIPLIER = 10;
    public const int PERFECT_RESCUE_BONUS = 50;

    // Tempos
    public const float INTERACTION_COOLDOWN = 0.5f;
    public const float RESCUE_ANIMATION_TIME = 2f;

    // Layer Names
    public const string GROUND_LAYER = "Ground";
    public const string ANIMAL_LAYER = "Animal";
    public const string OBSTACLE_LAYER = "Obstacle";
    public const string PLAYER_LAYER = "Player";

    // Tags
    public const string PLAYER_TAG = "Player";
    public const string ANIMAL_TAG = "Animal";
    public const string OBSTACLE_TAG = "Obstacle";
    public const string COLLECTIBLE_TAG = "Collectible";
}

/// <summary>
/// Configurações de dificuldade
/// </summary>
public static class DifficultySettings
{
    public static float GetHealthMultiplier(GameDifficulty difficulty)
    {
        return difficulty switch
        {
            GameDifficulty.Easy => 1.5f,
            GameDifficulty.Normal => 1f,
            GameDifficulty.Hard => 0.7f,
            _ => 1f
        };
    }

    public static float GetEnemySpeedMultiplier(GameDifficulty difficulty)
    {
        return difficulty switch
        {
            GameDifficulty.Easy => 0.7f,
            GameDifficulty.Normal => 1f,
            GameDifficulty.Hard => 1.5f,
            _ => 1f
        };
    }

    public static float GetDamageMultiplier(GameDifficulty difficulty)
    {
        return difficulty switch
        {
            GameDifficulty.Easy => 0.5f,
            GameDifficulty.Normal => 1f,
            GameDifficulty.Hard => 1.5f,
            _ => 1f
        };
    }
}