using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionTargetDefault : PotionTargetAbstract
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    protected override void OnPotionEffectsAdded(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= 0.5f;
        }
    }
    protected override void OnPotionEffectsRemoved(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= 2f;
        }
    }
}
