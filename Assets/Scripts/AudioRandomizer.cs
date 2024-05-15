using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] string[] strings;
    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AudioClip clip = (AudioClip) ObjectRandomizer.GetRandom(audioClips);
            if(audioSource) { audioSource.clip = clip; }
            string text = (string) ObjectRandomizer.GetRandom(strings);
            Debug.Log(text);
        }
    }
}
