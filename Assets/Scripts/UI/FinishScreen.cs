using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI highScoreText;

    [SerializeField]
    private TextMeshProUGUI currentHighScoreText;

    public void SetHighScore(int highScore)
    {
        highScoreText.text = highScore.ToString();

        int currentHighScore = GlobalGameController.Instance.PlayerData.GetHighScore(GameController.Instance.LevelIndex);
        Debug.Log("CurrentHighScore: " + currentHighScore);
        if (highScore > currentHighScore)
        {
            currentHighScoreText.text = "New High Score!";
        }
        else
        {
            currentHighScoreText.text = "Current High score: " + currentHighScore.ToString();
        }
    }
}
