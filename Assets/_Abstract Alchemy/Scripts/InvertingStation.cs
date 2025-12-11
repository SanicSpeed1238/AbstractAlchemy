using System;
using System.Collections.Generic;
using UnityEngine;

public class InvertingStation : MonoBehaviour
{
    [NonSerialized]
    public ObjectRoot currentBottleRoot;
    [NonSerialized]
    public PotionObject currentBottlePotion;

    public ParticleSystem chargingParticle;
    public AudioSource chargingSound;
    public ParticleSystem finishParticle;
    public AudioSource finishSound;

    private bool inverting;
    private bool inverted;
    private float invertChargeTime;
    private const float timeUntilCharged = 3f;

    public void FixedUpdate()
    {
        if (currentBottleRoot && currentBottlePotion && (!currentBottleRoot.XRGrabInteractable || !currentBottleRoot.XRGrabInteractable.isSelected) 
            && currentBottlePotion.currentEffects != PotionEffects.Effects.None && !inverted)
        {
            if (!inverting)
            {
                chargingParticle.Play();
                chargingSound.Play();
                inverting = true;
            }
            invertChargeTime += Time.fixedDeltaTime * (1f / timeUntilCharged);
            if (invertChargeTime >= 1f)
            {
                invertChargeTime %= 1f;
                InvertPotion();
                inverted = true;
            }
        }
        else
        {
            inverting = false;
            chargingParticle.Stop();
        }
    }

    public void InvertPotion()
    {
        if (currentBottleRoot && currentBottlePotion)
        {
            finishParticle.Play();
            finishSound.Play();
            List<PotionEffects> effects = currentBottlePotion.currentEffects.GetScriptableObjects();
            PotionEffects.Effects newEffects = currentBottlePotion.currentEffects;
            foreach (var item in effects)
            {
                newEffects &= ~item.currentEffect;
                newEffects |= item.conflictingEffect;
            }
            currentBottlePotion.UpdateCurrentPotion(newEffects);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!currentBottleRoot)
        {
            ObjectRoot root = collider.GetComponentInParent<ObjectRoot>();
            if (root)
            {
                if (root.smashableIdentifier == "Bottle")
                {
                    PotionObject potion = root.GetComponentInChildren<PotionObject>();
                    if (potion)
                    {
                        currentBottleRoot = root;
                        currentBottlePotion = potion;
                    }
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (currentBottleRoot)
        {
            ObjectRoot root = collider.GetComponentInParent<ObjectRoot>();
            if (root && root == currentBottleRoot)
            {
                currentBottleRoot = null;
                currentBottlePotion = null;
                inverted = false;
                invertChargeTime = 0f;
            }
        }
    }
}