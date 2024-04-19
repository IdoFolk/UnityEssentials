using System;
using System.Collections.Generic;
using IdoFolk_Essentials.Tools.Random;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundConfig",menuName = "Sound/SoundConfig",order = 5)]
public class SoundsConfig : ScriptableObject
{
    //TODO find a way to create new catergories from the inspector
    public Dictionary<SoundEffectCategory, SoundEffect[]> SoundEffects =>
        new()
        {
            
        };
    
    
}
[Serializable]
public class SoundEffect
{
    public SoundEffectType Type;
    public bool ClipVariations;
    public bool VolumeVariations;
    public bool HighPriority;
    [HideIf(nameof(ClipVariations))]public AudioClip Clip;
    [ShowIf(nameof(ClipVariations))]public AudioClip[] Clips;
    [ShowIf(nameof(VolumeVariations)),MinMaxSlider(0,1)]public Vector2 VolumeValues;
    private SoundEffectCategory _category;

    public AudioClip ProvideRandomClip()
    {
        if (!ClipVariations) return null;
        RandomDeck<AudioClip> randomDeck = new RandomDeck<AudioClip>(Clips);
        return randomDeck.Provide();
    }
}
public enum SoundEffectCategory
{

}
public enum SoundEffectType
{
   
}