using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] bool playOnStart = true;
    [SerializeField] float minTiempoEspera;
    [SerializeField] float maxTiempoEspera;
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;

    bool isActive = true;
    public bool IsPlaying
    {
        get { return isActive; }
        set
        {
            if (value && value != isActive)
            {
                isActive = value;
                StartCoroutine(playNextClip());
            }
            if (!value && value != isActive)
            {
                isActive = value;
                StopCoroutine(playNextClip());
            }
        }
    }

    private void Awake()
    {
        Mathf.Max(0f, minTiempoEspera);
        Mathf.Max(0f, minTiempoEspera, maxTiempoEspera);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if(playOnStart) { StartCoroutine(playNextClip());}
        else { isActive = false; }
    }

    IEnumerator playNextClip()
    {
        while (isActive)
        {
            float time = Random.Range(minTiempoEspera, maxTiempoEspera);
            Debug.Log(name + " reproduciendo siguiente clip en " + time + "s.");
            yield return new WaitForSeconds(time);
            AudioClip clip = (AudioClip)ObjectRandomizer.GetRandom(audioClips);
            if (audioSource) { audioSource.clip = clip; }
            audioSource.Play();
            Debug.Log(name + " reproduciendo clip: " + clip.name + ".");
            yield return new WaitForSeconds(audioClips.Length);
        }
    }
}
