using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioRandomizer : MonoBehaviour
{
    [SerializeField] bool playOnStart = true;
    [SerializeField] float minTiempoEspera;
    [SerializeField] float maxTiempoEspera;
    [SerializeField] int cantidadAudiosEnReserva;
    [SerializeField] AudioClip[] audioClips;

    AudioSource audioSource;
    
    List<AudioClip> reproducir = new List<AudioClip>();
    List<AudioClip> reserva = new List<AudioClip> ();

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

        foreach(AudioClip clip in audioClips)
        {
            reproducir.Add(clip);
        }
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
            AudioClip clip = (AudioClip)ObjectRandomizer.GetRandom(reproducir.ToArray());
            if (audioSource) { audioSource.clip = clip; }
            audioSource.Play();
            actualizarListaAudio(clip);
            Debug.Log(name + " reproduciendo clip: " + clip.name + ".");
            yield return new WaitForSeconds(clip.length);
        }
    }

    private void actualizarListaAudio(AudioClip clipReproducido)
    {
        reserva.Add(clipReproducido);
        reproducir.Remove(clipReproducido);
        if(reserva.Count >= cantidadAudiosEnReserva || reproducir.Count == 0)
        {
            AudioClip llevarAReproduccion = reserva[0];
            reserva.RemoveAt(0);
            reproducir.Add(llevarAReproduccion);
        }
    }
}
