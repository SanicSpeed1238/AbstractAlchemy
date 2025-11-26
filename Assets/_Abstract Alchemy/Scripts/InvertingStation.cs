using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InvertingStation : MonoBehaviour
{
    [NonSerialized]
    public ObjectRoot currentBottle;

    private bool inverted;
    private float invertChargeTime;
    private const float timeUntilCharged = 1f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FixedUpdate()
    {
        if (currentBottle && !inverted)
        {
            invertChargeTime += Time.fixedDeltaTime * (1f / timeUntilCharged);
            if (invertChargeTime >= 1f)
            {
                invertChargeTime %= 1f;
                InvertPotion();
                inverted = true;
            }
        }
    }

    public void InvertPotion()
    {
        if (currentBottle)
        {
            PotionObject potion = currentBottle.GetComponentInChildren<PotionObject>();
            if (potion)
            {
                List<PotionEffects> effects = potion.currentEffects.GetScriptableObjects();
                PotionEffects.Effects newEffects = potion.currentEffects;
                foreach (var item in effects)
                {
                    newEffects &= ~item.currentEffect;
                    newEffects |= item.conflictingEffect;
                }
                Debug.Log(newEffects);
                potion.UpdateCurrentPotion(newEffects);
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!currentBottle)
        {
            ObjectRoot root = collider.GetComponentInParent<ObjectRoot>();
            if (root)
            {
                if (root.smashableIdentifier == "Bottle")
                {
                    currentBottle = root;
                }
            }
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (currentBottle)
        {
            ObjectRoot root = collider.GetComponentInParent<ObjectRoot>();
            if (root && root == currentBottle)
            {
                currentBottle = null;
                inverted = false;
                invertChargeTime = 0f;
            }
        }
    }
}
