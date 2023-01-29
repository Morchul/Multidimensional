using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private UFO ufo;
    private GameController gameController;

    [SerializeField]
    private Transform gameOverScreen;

    [SerializeField]
    private FinishScreen winScreen;

    [SerializeField]
    private Transform pauseScreen;

    [Header("Scan Slider")]
    [SerializeField]
    private Slider slider;

    [Header("HighScore text")]
    [SerializeField]
    private TextMeshProUGUI highscoreText;

    void Start()
    {
        gameController = GameController.Instance;
        ufo = gameController.UFO;
    }

    void Update()
    {
        slider.maxValue = ufo.ScanCooldown;
        slider.value = ufo.ScanCooldownTimer;
        highscoreText.text = gameController.HighScore.ToString();
    }

    public void SetGameOverScreenActive(bool active)
    {
        gameOverScreen.gameObject.SetActive(active);
        //gameOverScreen.SetHighScore(GameController.Instance.HighScore);
    }

    public void SetWinScreenActive(bool active)
    {
        winScreen.gameObject.SetActive(active);
        if(active)
            winScreen.SetHighScore(GameController.Instance.HighScore);
    }

    public void ShowPauseMenu()
    {
        pauseScreen.gameObject.SetActive(true);
    }

    public void Retry()
    {
        SetGameOverScreenActive(false);
        SetWinScreenActive(false);
        GameController.Instance.Restart();
    }

    public void Exit()
    {
        GameController.Instance.Exit();
    }
}
