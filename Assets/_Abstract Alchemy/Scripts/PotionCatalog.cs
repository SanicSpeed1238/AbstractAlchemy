using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PotionCatalog : MonoBehaviour
{
    public static PotionCatalog instance { get; private set; }

    public PotionEffects[] potionEffects = new PotionEffects[6];
    // Start is called before the first frame update
    void Start()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    public PotionEffects GetScriptableObjectFromEnum(PotionEffects.Effects effectEnum)
    {
        if (effectEnum.HasFlag(PotionEffects.Effects.Grow)) { return potionEffects[0]; }
        if (effectEnum.HasFlag(PotionEffects.Effects.Shrink)) { return potionEffects[1]; }
        if (effectEnum.HasFlag(PotionEffects.Effects.Heavy)) { return potionEffects[2]; }
        if (effectEnum.HasFlag(PotionEffects.Effects.Light)) { return potionEffects[3]; }
        if (effectEnum.HasFlag(PotionEffects.Effects.Hot)) { return potionEffects[4]; }
        if (effectEnum.HasFlag(PotionEffects.Effects.Cold)) { return potionEffects[5]; }
        return null;
    }

    public static void ErrorIfNoCatalog()
    {
        if (!PotionCatalog.instance)
        {
            Debug.LogError("This scene is missing a PotionCatalog object. This can be found in Prefabs > Potions > PotionCatalog. Add this anywhere to the scene");
        }
    }
}

public static class PotionExtension
{
    public static PotionEffects GetScriptableObject(this PotionEffects.Effects effectEnum)
    {
        PotionCatalog.ErrorIfNoCatalog();
        return PotionCatalog.instance.GetScriptableObjectFromEnum(effectEnum);
    }

    public static void UpdateMaterialWithPotionEffect(this Material material, PotionEffects.Effects effectEnum)
    {
        PotionCatalog.ErrorIfNoCatalog();
        bool mainColorSet = false;
        for (int i = 0; i < PotionCatalog.instance.potionEffects.Length; i++)
        {
            if (effectEnum.HasFlag(PotionCatalog.instance.potionEffects[i].currentEffect))
            {
                if (!mainColorSet)
                {
                    material.SetColor("_MainColor", PotionCatalog.instance.potionEffects[i].liquidColor);
                    material.SetColor("_MixedColor", PotionCatalog.instance.potionEffects[i].liquidColor);
                    mainColorSet = true;
                }
                else
                {
                    material.SetColor("_MixedColor", PotionCatalog.instance.potionEffects[i].liquidColor);
                    return;
                }
            }
        }
    }
}
