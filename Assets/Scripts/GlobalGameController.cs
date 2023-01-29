using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GlobalGameController : MonoBehaviour
{
    #region Singleton
    private static GlobalGameController instance;

    public static GlobalGameController Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogError("Singleton GlobalGameController does already exist!");
            Destroy(this.gameObject);
        }
        else
            instance = this;
    }
    #endregion

    [SerializeField]
    private LevelContainer levelContainer;

    [SerializeField]
    private AudioMixer audioMixer;

    public PlayerData PlayerData { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        PlayerData = SaveLoadPlayerData.Load();

        if (PlayerData == null)
            PlayerData = new PlayerData(levelContainer.levels.Length);

        //Maybe refactor.
        audioMixer.SetFloat("MasterVolume", PlayerData.GetVolume(PlayerData.VolumeType.MASTER));
        audioMixer.SetFloat("UIVolume", PlayerData.GetVolume(PlayerData.VolumeType.UI));
        audioMixer.SetFloat("GameVolume", PlayerData.GetVolume(PlayerData.VolumeType.GAME));
        audioMixer.SetFloat("MusicVolume", PlayerData.GetVolume(PlayerData.VolumeType.MUSIC));
    }

    private void OnDestroy()
    {
        SaveLoadPlayerData.Save(PlayerData);
    }
}
