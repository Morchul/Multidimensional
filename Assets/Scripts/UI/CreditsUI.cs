using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsUI : MonoBehaviour
{
    [SerializeField]
    [Scene]
    private string levelSelectionScene;

    public void BackButton()
    {
        SceneManager.LoadScene(levelSelectionScene);
    }
}
