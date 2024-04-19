using System;
using System.Collections.Generic;
using IdoFolk_Essentials.Helpers;
using UnityEngine;

public class BgMusicManager : MonoSingleton<BgMusicManager>
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioReverbFilter _audioReverbFilter;
    [SerializeField] private AudioLowPassFilter _audioLowPassFilter;
    [SerializeField] private float _fadeSpeed;
    [SerializeField] private List<MusicData> _musicClips;

    public AudioSource AudioSource => _audioSource;

    public AudioReverbFilter AudioReverbFilter => _audioReverbFilter;

    public AudioLowPassFilter AudioLowPassFilter => _audioLowPassFilter;

    public void PlayMusic(MusicClip musicClip)
    {
        foreach (var musicData in _musicClips)
        {
            if (musicData.MusicClip == musicClip) _audioSource.clip = musicData.AudioClip;
        }

        if (_audioSource.clip == null)
        {
            Debug.LogError("MusicClip not found in List");
            return;
        }
        _audioSource.Play();
    }

    public void StopMusic()
    {
        if (!_audioSource.isPlaying) return;
        _audioSource.Stop();
    }
    
    public void ChangeMusicVolume(float volume)
    {
        //_musicAudioMixer.audioMixer.SetFloat()
        _audioSource.volume = volume;
    }
}
[Serializable]
public struct MusicData
{
    public MusicClip MusicClip;
    public AudioClip AudioClip;
}

public enum MusicClip
{
    
}