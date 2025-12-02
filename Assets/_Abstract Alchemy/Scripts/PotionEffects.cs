using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Effect", menuName = "Potion Effect")]
public class PotionEffects : ScriptableObject
{
    [Flags]
    public enum Effects
    {
        None = 0,
        Shrink = 1,
        Grow = 2,
        Light = 4,
        Heavy = 8,
        Cold = 16,
        Hot = 32
    }
    [Header("Effect")]
    public Effects currentEffect;
    public Effects conflictingEffect;

    [Header("VFX / SFX")]
    public GameObject effectFX;
    public GameObject startFX;
    public GameObject dropletFX;
    public Color liquidColor;
}