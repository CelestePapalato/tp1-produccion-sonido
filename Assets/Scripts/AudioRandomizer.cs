using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;
    [SerializeField] string[] strings;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //AudioClip clip = (AudioClip) ObjectRandomizer.GetRandom(audioClips);
            string text = (string) ObjectRandomizer.GetRandom(strings);
            Debug.Log(text);
        }
    }
}
