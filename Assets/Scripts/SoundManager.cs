using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    public GameObject sfx;
    public AudioClip[] audioClips;

    public Slider soundSlider;

    public void PlaySound(int soundNum)
    {
        GameObject instGO = Instantiate(sfx, Vector2.zero, Quaternion.identity) as GameObject;
        AudioSource soundSource = instGO.GetComponent<AudioSource>();
        instGO.transform.SetParent(GameObject.Find("EnemyBullets").transform);
        soundSource.clip = audioClips[soundNum];
        soundSource.volume = soundSlider.value;
        soundSource.Play();
        Destroy(instGO, audioClips[soundNum].length);
    }

    public void PlaySound(int soundNum, float volume)
    {
        GameObject instGO = Instantiate(sfx, Vector2.zero, Quaternion.identity) as GameObject;
        AudioSource soundSource = instGO.GetComponent<AudioSource>();
        instGO.transform.SetParent(GameObject.Find("EnemyBullets").transform);
        soundSource.volume = volume;
        soundSource.clip = audioClips[soundNum];
        soundSource.Play();
        Destroy(instGO, audioClips[soundNum].length);
    }
}
