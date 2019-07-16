using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayRandom : MonoBehaviour
{
    public List<AudioClip> sounds;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    private IEnumerator Start()
    {
        while (true)
        {
            audioSource.clip = sounds[Random.Range(0, sounds.Count)];
            audioSource.Play();

            float waitForTime = audioSource.clip.length;
            yield return new WaitForSeconds(waitForTime);
        }
    }


    //public AudioSource GOAudio
    //{
    //    get
    //    {
    //        if (GOAudio != null)
    //        {
    //            return audioSource;
    //        }
    //        else
    //        {
    //            return audioSource = this.gameObject.GetComponent<AudioSource>();
    //        }
    //    }
    //    private set
    //    {
    //        audioSource = value;
    //    }
    //}
}
