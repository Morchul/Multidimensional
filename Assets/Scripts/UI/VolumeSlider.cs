using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private string variableName;

    [SerializeField]
    private PlayerData.VolumeType volumeType;

    private PlayerData playerData;
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        playerData = GlobalGameController.Instance.PlayerData;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateVolume);

        if (audioMixer.GetFloat(variableName, out float value))
        {
            slider.value = value;
        }
    }

    private void UpdateVolume(float newValue)
    {
        audioMixer.SetFloat(variableName, newValue);
        playerData.SaveVolumeSettings(volumeType, newValue);
    }
}
