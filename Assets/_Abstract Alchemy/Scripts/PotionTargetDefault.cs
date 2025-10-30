// Used on Generic Objects

public class PotionTargetDefault : PotionTargetAbstract
{
    protected override void OnPotionEffectsAdded(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= 0.5f;
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale *= 2f;
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            // Light Effect
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            // Heavy Effect
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            // Cold Effect
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            // Hot Effect
        }
    }
    protected override void OnPotionEffectsRemoved(PotionEffects.Effects effects)
    {
        if (effects.HasFlag(PotionEffects.Effects.Shrink))
        {
            transform.localScale *= 2f;
        }
        if (effects.HasFlag(PotionEffects.Effects.Grow))
        {
            transform.localScale *= 0.5f;
        }

        if (effects.HasFlag(PotionEffects.Effects.Light))
        {
            // Light Effect
        }
        if (effects.HasFlag(PotionEffects.Effects.Heavy))
        {
            // Heavy Effect
        }

        if (effects.HasFlag(PotionEffects.Effects.Cold))
        {
            // Cold Effect
        }
        if (effects.HasFlag(PotionEffects.Effects.Hot))
        {
            // Hot Effect
        }
    }
}