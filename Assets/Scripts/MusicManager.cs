using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    private static MusicManager instance;

    public static MusicManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Singleton MusicManager does already exist!");
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            audioSource = GetComponent<AudioSource>();
        }
    }
    #endregion

    [SerializeField]
    [Scene]
    private string[] menuScenes;
    //All others are level Scenes

    [SerializeField]
    private AudioClip menuMusic;

    [SerializeField]
    private AudioClip[] levelMusic;

    private AudioSource audioSource;

    [SerializeField]
    private float timeBetweenMusicClips;
    private float timer;

    private enum MusicType
    {
        MENU,
        LEVEL,
        NONE
    }

    [SerializeField]
    private MusicType currentlyPlaying;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        currentlyPlaying = MusicType.NONE;

        SceneManager.activeSceneChanged += SceneChanged;

        timer = 0;
    }

    private void Update()
    {
        if (currentlyPlaying == MusicType.NONE) return;

        if (!audioSource.isPlaying)
        {
            timer += Time.deltaTime;

            if (timer > timeBetweenMusicClips)
                ContinueMusic();
        }
    }

    private void ContinueMusic()
    {
        switch (currentlyPlaying)
        {
            case MusicType.MENU: StartMenuMusic(); break;
            case MusicType.LEVEL: StartRandomLevelMusic(); break;
        }
    }

    private void SceneChanged(Scene _, Scene newScene)
    {
        foreach(string menuSceneName in menuScenes)
        {
            Debug.Log("menuSceneName: " + menuSceneName);

            int startIndex = menuSceneName.LastIndexOf('/') + 1;
            int endIndex = menuSceneName.LastIndexOf('.');

            string sceneName = menuSceneName.Substring(startIndex, endIndex - startIndex);
            Debug.Log("SceneName: " + newScene.name + "/" + sceneName);
            if(sceneName == newScene.name)
            {
                Debug.Log("MenuScene");
                StartMenuMusic();
                return;
            }
        }

        //No menu Scene
        StartRandomLevelMusic();
    }

    public void StartMenuMusic()
    {
        if (currentlyPlaying == MusicType.MENU && audioSource.isPlaying) return;

        currentlyPlaying = MusicType.MENU;
        ChangeMusicTo(menuMusic);
    }

    public void StartRandomLevelMusic(bool forceNewMusic = false)
    {
        if (currentlyPlaying == MusicType.LEVEL && !forceNewMusic && audioSource.isPlaying) return;

        currentlyPlaying = MusicType.LEVEL;
        AudioClip randomLevelMusic = levelMusic[Random.Range(0,levelMusic.Length)];
        ChangeMusicTo(randomLevelMusic);
    }

    private void ChangeMusicTo(AudioClip audioClip)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
        audioSource.clip = audioClip;
        audioSource.Play();
    }
}
