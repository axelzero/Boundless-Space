using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audioSource;

    void Update()
    {
        Volume();
    }


    private void Volume()
    {
        audioSource.volume = slider.value;
    }
}
