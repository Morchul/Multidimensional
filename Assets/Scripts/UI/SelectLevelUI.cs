using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectLevelUI : MonoBehaviour
{
    [SerializeField][Scene]
    private string creditsScene;

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        SceneManager.LoadScene(creditsScene);
    }
}
