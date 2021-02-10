using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
public class AudioSystem : MonoBehaviour
{
    public static AudioSystem audioSystem;
    public AudioSource[] altAudioSources;
    public AudioSource M_audioSrc;
    public GameObject audioPrefab;
    public List<GameObject> pooledAudioSources;
    public int amountToPool;
    public bool expandPool = true;
    public void Awake()
    {
        audioSystem = this;
    }
    private void Start()
    {
        //pooledAudioSources = new List<GameObject>();
        //for (int i = 0; i < amountToPool; i++)
        //{
        //    GameObject newAudio = Instantiate(audioPrefab, transform);
        //    pooledAudioSources.Add(newAudio);
        //}
    }
    public static void PlayGameMusic(AudioClip clip, float pitch, float volume, bool loop)
    {
        audioSystem.M_audioSrc.clip = clip;
        audioSystem.M_audioSrc.pitch = pitch;
        audioSystem.M_audioSrc.volume = volume;
        audioSystem.M_audioSrc.loop = loop;
        audioSystem.M_audioSrc.Play();
    }
    public static void PlayAltAudioSource(int sourceNum, AudioClip clip, float pitch, float volume, bool active)
    {
        if (active)
        {
            audioSystem.altAudioSources[sourceNum].clip = clip;
            audioSystem.altAudioSources[sourceNum].pitch = pitch;
            audioSystem.altAudioSources[sourceNum].volume = volume;
            if (sourceNum == 0 && !audioSystem.altAudioSources[sourceNum].isPlaying)
            {
                audioSystem.altAudioSources[sourceNum].loop = true;
                audioSystem.altAudioSources[sourceNum].PlayOneShot(clip);
            }
            else if (sourceNum == 7)
            {
                if (audioSystem.altAudioSources[sourceNum].isPlaying)
                    audioSystem.altAudioSources[sourceNum].Stop();
                audioSystem.altAudioSources[sourceNum].PlayOneShot(clip);
            } 
            else if (sourceNum > 7)
            {
                if (audioSystem.altAudioSources[sourceNum].isPlaying)
                    audioSystem.altAudioSources[sourceNum].Stop();
                audioSystem.altAudioSources[sourceNum].PlayOneShot(clip);
            }
            else if (sourceNum > 0 && sourceNum < 7)
                audioSystem.altAudioSources[sourceNum].PlayOneShot(clip);
        }
        else
        {
            if (audioSystem.altAudioSources[sourceNum].loop)
                audioSystem.altAudioSources[sourceNum].loop = false;
            audioSystem.altAudioSources[sourceNum].Stop();
        }
    }
    public static void PlayAudioSource(AudioClip clip, float pitch, float volume)
    {
        AudioSource source = audioSystem.GetPooledAudioSource();
        if (source != null)
        {
            source.pitch = pitch;
            source.volume = volume;
            source.clip = clip;
            source.Play();
        }
    }
    public AudioSource GetPooledAudioSource()
    {
        for (int a = 0; a < pooledAudioSources.Count; a++)
        {
            AudioSource selectedAudio = pooledAudioSources[a].GetComponent<AudioSource>();
            if (!selectedAudio.isPlaying)
                return selectedAudio;
        }
        if (expandPool)
        {
            GameObject newAudio = Instantiate(audioPrefab, transform);
            AudioSource selectedNewAudio = newAudio.GetComponent<AudioSource>();
            pooledAudioSources.Add(newAudio);
            return selectedNewAudio;
        }
        else
            return null;
    }
    public void OnSelectable(AudioClip clip)
    {
        PlayAudioSource(clip, 1, 1);
    } 

    public void OnAnnouncer(AudioClip clip)
    {
        PlayAltAudioSource(9, clip, 1, 0.7f, true);
    }
}
