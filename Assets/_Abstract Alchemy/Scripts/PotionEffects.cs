using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Potion Effect")]
public class PotionEffects : ScriptableObject
{
    [Flags]
    public enum Effects
    {
        None = 0,
        Grow = 1,
        Shrink = 2,
        Heavy = 4,
        Light = 8,
        Hot = 16,
        Cold = 32
    }
    [Header("Effect")]
    public Effects currentEffect;
    public Effects conflictingEffect;

    [Header("VFX / SFX")]
    public GameObject effectFX;
    public GameObject startFX;
    public Material liquidMaterial;
}