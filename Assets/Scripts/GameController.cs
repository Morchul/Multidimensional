using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private UFO ufo;
    public UFO UFO => ufo;

    [SerializeField]
    private CameraController cameraController;

    [SerializeField]
    [Scene]
    private string levelSelectionScene;

    [SerializeField]
    private Transform startPos;
    public Vector3 StartPos => startPos.position;

    [SerializeField]
    private UIController uiController;

    [SerializeField]
    private int levelIndex;
    public int LevelIndex => levelIndex;

    private AudioSource audioSource;

    public bool IsGameOver { get; private set; }

    public int HighScore { get; private set; }

    #region Singleton
    private static GameController instance;

    public static GameController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Singleton GameController does already exist!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }
    #endregion

    private void Start()
    {
        IsGameOver = false;
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        IsGameOver = true;
        Pause();
        ufo.GameOver();
        uiController.SetGameOverScreenActive(true);
    }

    public void Win()
    {
        Pause();
        uiController.SetWinScreenActive(true);

        audioSource.Play();

        PlayerData playerData = GlobalGameController.Instance.PlayerData;
        playerData.LevelCompleted(levelIndex, HighScore);
    }

    public void Restart()
    {
        IsGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Exit()
    {
        IsGameOver = false;
        Time.timeScale = 1;
        SceneManager.LoadScene(levelSelectionScene);
    }

    public bool IsCircleInView(Vector2 position, float radius)
    {
        return cameraController.IsCircleInView(position, radius);
    }

    public float GetCameraXPosition()
    {
        return cameraController.transform.position.x;
    }

    public bool IsPointInView(Vector2 point)
    {
        return cameraController.IsPointInView(point);
    }

    public void IncreaseHighScore(int highScorePoints)
    {
        HighScore += highScorePoints;
    }

    public Vector2 GetMouseWorldPos()
    {
        Vector3 worldPos = cameraController.PlayerCam.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(worldPos.x, worldPos.y);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
            uiController.ShowPauseMenu();
        }
    }

    public void Pause()
    {
        ufo.Freeze = true;
        cameraController.Freeze = true;
        Time.timeScale = 0;
    }

    public void Resume()
    {
        ufo.Freeze = false;
        cameraController.Freeze = false;
        Time.timeScale = 1;
    }
}
