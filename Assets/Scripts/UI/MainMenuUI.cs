using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [Scene] [SerializeField]
    private string LevelSelectionScene;

    private void Start()
    {
        MusicManager.Instance.StartMenuMusic();
    }

    public void StartButton()
    {
        SceneManager.LoadScene(LevelSelectionScene);
    }

    public void ExitButton()
    {
        Application.Quit();
    }
}
