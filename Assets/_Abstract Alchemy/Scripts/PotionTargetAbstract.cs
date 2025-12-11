using System;
using UnityEngine;
using static PotionEffects;

public abstract class PotionTargetAbstract : MonoBehaviour
{
    [Header("Basic Properties")]
    public ObjectRoot objectRoot;
    public GameObject currentVFX;
    [Range(0.01f, 2f)]
    public float scaleVFX = 1f; 

    public PotionEffects.Effects currentEffects;
    public Action<ObjectRoot> OnPotionEffectsChangedEvent;

    public virtual void Start()
    {
        if (!objectRoot) { objectRoot = GetComponent<ObjectRoot>(); }

        CreateVFXObject();
    }
    private void CreateVFXObject()
    {
        if (!currentVFX)
        {
            currentVFX = new GameObject("VFX");
            currentVFX.transform.parent = objectRoot.transform;
            currentVFX.transform.localScale *= scaleVFX;
        }
    }
    public PotionEffects.Effects GetPotionEffects()
    {
        return currentEffects;
    }
    public bool HasPotionEffect(PotionEffects.Effects effects)
    {
        return currentEffects.HasFlag(effects);
    }

    public void AddPotionEffect(PotionEffects.Effects effects)
    {
        PotionEffects.Effects effectsToRemove = PotionEffects.Effects.None;
        foreach (var item in effects.GetScriptableObjects())
        {
            if (HasPotionEffect(item.conflictingEffect))
            {
                effectsToRemove |= item.conflictingEffect;
            }

        }
        if (effectsToRemove != PotionEffects.Effects.None) { RemovePotionEffect(effectsToRemove); }

        PotionEffects.Effects newEffects = effects &~ currentEffects;
        currentEffects |= effects;

        if (newEffects != PotionEffects.Effects.None)
        {
            OnPotionEffectsAdded(newEffects);
            OnPotionEffectsChangedEvent?.Invoke(objectRoot);
        }

        // VFX Stuff
        CreateVFXObject();
        foreach (PotionEffects.Effects singleEffect in Enum.GetValues(typeof(PotionEffects.Effects)))
        {
            if (singleEffect == PotionEffects.Effects.None)
                continue;

            if ((newEffects & singleEffect) != 0)
            {
                bool vfxExists = false;
                for (int i = 0; i < currentVFX.transform.childCount; i++)
                {
                    var reference = currentVFX.transform.GetChild(i).GetComponent<SimpleReference>();
                    if (reference != null && reference.potionEffect == singleEffect)
                    {
                        vfxExists = true;
                        break;
                    }
                }

                if (!vfxExists)
                {
                    var vfxPrefab = singleEffect.GetScriptableObject().effectFX;
                    if (vfxPrefab != null)
                    {
                        GameObject newVFX = Instantiate(vfxPrefab, objectRoot.transform.position, Quaternion.identity, currentVFX.transform);
                        newVFX.transform.localPosition = Vector3.zero;
                    }
                }
            }
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

        // VFX Stuff
        if (currentVFX)
        {
            for (int vfx = currentVFX.transform.childCount - 1; vfx >= 0; vfx--)
            {
                Transform vfxObject = currentVFX.transform.GetChild(vfx);
                if (vfxObject.TryGetComponent<SimpleReference>(out var currentVFXObject))
                {
                    if (currentVFXObject.potionEffect == effects)
                        Destroy(vfxObject.gameObject);
                }
            }
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

        if (currentVFX)
        {
            for (int i = currentVFX.transform.childCount - 1; i >= 0; i--)
            {
                Destroy(currentVFX.transform.GetChild(i).gameObject);
            }
        }
    }  

    protected abstract void OnPotionEffectsAdded(PotionEffects.Effects effects);

    protected abstract void OnPotionEffectsRemoved(PotionEffects.Effects effects);

}