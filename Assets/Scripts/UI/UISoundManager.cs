using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISoundManager : MonoBehaviour
{
    #region Singleton
    private static UISoundManager instance;

    public static UISoundManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Singleton UISoundManager does already exist!");
            Destroy(this.gameObject);
        }
        else
            instance = this;
    }
    #endregion

    [SerializeField]
    private AudioClip buttonHoverSound;

    [SerializeField]
    private AudioClip buttonClickSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlayButtonHoverSound()
    {
        audioSource.PlayOneShot(buttonHoverSound);
    }

    public void PlayButtonClickSound()
    {
        audioSource.PlayOneShot(buttonClickSound);
    }
}
