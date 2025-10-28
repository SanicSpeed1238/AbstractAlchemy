using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PotionTargetAbstract : MonoBehaviour
{
    private PotionEffects.Effects currentEffects;
    public PotionEffects.Effects GetPotionEffects()
    {
        return currentEffects;
    }
    public void AddPotionEffect(PotionEffects effects)
    {
        if (effects.conflictingEffect != PotionEffects.Effects.None && HasPotionEffect(effects.conflictingEffect))
        {
            RemovePotionEffect(effects.conflictingEffect);
        }
        AddPotionEffect(effects.currentEffect);
    }
    public void AddPotionEffect(PotionEffects.Effects effects)
    {
        PotionEffects.Effects newEffects = currentEffects ^ effects;
        currentEffects |= effects;

        if (newEffects != PotionEffects.Effects.None)
        {
            OnPotionEffectsAdded(newEffects);
        }
    }
    public void RemovePotionEffect(PotionEffects.Effects effects)
    {
        PotionEffects.Effects removedEffects = currentEffects & effects;
        currentEffects &= ~effects;

        if (removedEffects != PotionEffects.Effects.None)
        {
            OnPotionEffectsRemoved(removedEffects);
        }
    }
    public void ClearPotionEffects()
    {
        RemovePotionEffect((PotionEffects.Effects)~0);
    }
    public bool HasPotionEffect(PotionEffects.Effects effects)
    {
        return currentEffects.HasFlag(effects);
    }

    protected abstract void OnPotionEffectsAdded(PotionEffects.Effects effects);

    protected abstract void OnPotionEffectsRemoved(PotionEffects.Effects effects);

}
