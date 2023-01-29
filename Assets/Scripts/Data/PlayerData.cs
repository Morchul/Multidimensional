using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int currentLevel;
    public int[] currentHighScores;

    public float[] volumeSettings;

    public PlayerData(int amountOfLevels)
    {
        currentHighScores = new int[amountOfLevels];
        volumeSettings = new float[4];
        currentLevel = 1;
    }

    public void LevelCompleted(int levelIndex, int HighScore)
    {
        currentLevel = Mathf.Max(currentLevel, levelIndex + 1);
        currentHighScores[levelIndex - 1] = Mathf.Max(currentHighScores[levelIndex - 1], HighScore);
    }

    public int GetHighScore(int levelIndex)
    {
        return currentHighScores[levelIndex - 1];
    }

    public void SaveVolumeSettings(VolumeType type, float value)
    {
        this.volumeSettings[(int)type] = value;
    }

    public float GetVolume(VolumeType type) => volumeSettings[(int)type];

    public override string ToString()
    {
        return $"CurrentLevel: {currentLevel} / Highscore in Level 1: {currentHighScores[0]}";
    }

    public enum VolumeType
    {
        MASTER = 0,
        UI = 1,
        GAME = 2,
        MUSIC = 3
    }
}
