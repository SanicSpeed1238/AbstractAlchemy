using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class MixingStation : PotionTargetAbstract
{
    public bool hasEffect { get { return currentEffects != PotionEffects.Effects.None; } }
    
    protected override void OnPotionEffectsAdded(PotionEffects.Effects effects)
    {
        objectRoot.renderer.gameObject.SetActive(true);
        objectRoot.renderer.material.UpdateMaterialWithPotionEffect(currentEffects);
    }
    protected override void OnPotionEffectsRemoved(PotionEffects.Effects effects)
    {
        if (!hasEffect)
        {
            objectRoot.renderer.gameObject.SetActive(false);
        }
        else
        {
            objectRoot.renderer.material.UpdateMaterialWithPotionEffect(currentEffects);
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        ObjectRoot root = collider.gameObject.GetComponentInParent<ObjectRoot>();
        if (root)
        {
            if (root.smashableIdentifier == "Bottle")
            {
                Transform potion = root.transform.Find("Potion Component");
                if (potion && potion.TryGetComponent(out PotionObject potionObject))
                {
                    potionObject.UpdateCurrentPotion(currentEffects);
                }
            }
        }
    }
}