using System;
using UnityEngine;

public abstract class PotionTargetAbstract : MonoBehaviour
{
    public ObjectRoot objectRoot;
    
    public PotionEffects.Effects currentEffects;
    public Action<ObjectRoot> OnPotionEffectsChangedEvent;

    public virtual void Start()
    {
        if (!objectRoot) { objectRoot = GetComponent<ObjectRoot>(); }
    }
    public PotionEffects.Effects GetPotionEffects()
    {
        return currentEffects;
    }
    public bool HasPotionEffect(PotionEffects.Effects effects)
    {
        return currentEffects.HasFlag(effects);
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
        PotionEffects.Effects newEffects = effects &~ currentEffects;
        currentEffects |= effects;

        if (newEffects != PotionEffects.Effects.None)
        {
            OnPotionEffectsAdded(newEffects);
            OnPotionEffectsChangedEvent?.Invoke(objectRoot);
        }
    }

    public void RemovePotionEffect(PotionEffects.Effects effects)
    {
        PotionEffects.Effects removedEffects = currentEffects & effects;
        currentEffects &= ~effects;

        if (removedEffects != PotionEffects.Effects.None)
        {
            OnPotionEffectsRemoved(removedEffects);
            OnPotionEffectsChangedEvent?.Invoke(objectRoot);
        }
    }

    public void SetPotionEffects(PotionEffects.Effects effects)
    {
        RemovePotionEffect(~effects);
        AddPotionEffect(effects);
    }

    public void ClearPotionEffects()
    {
        RemovePotionEffect((PotionEffects.Effects)~0);
    }  

    protected abstract void OnPotionEffectsAdded(PotionEffects.Effects effects);

    protected abstract void OnPotionEffectsRemoved(PotionEffects.Effects effects);

}