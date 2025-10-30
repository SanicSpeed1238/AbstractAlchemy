using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RecipeUtil
{
    public static GameObject ConvertObjectToOtherObject(ObjectRoot input, GameObject outputPrefab)
    {
        PotionEffects.Effects potionEffects = PotionEffects.Effects.None;
        if (input.potionTarget)
        {
            potionEffects = input.potionTarget.GetPotionEffects();
        }
        GameObject converted = GameObject.Instantiate(outputPrefab, input.transform.position, input.transform.rotation);
        if (outputPrefab.TryGetComponent<PotionTargetAbstract>(out PotionTargetAbstract potionTarget) && potionEffects != PotionEffects.Effects.None)
        {
            potionTarget.AddPotionEffect(potionEffects);
        }
        return converted;
    }
}
